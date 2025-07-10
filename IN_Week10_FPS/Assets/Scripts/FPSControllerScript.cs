using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSControllerScript : MonoBehaviour
{
    #region character movement variables
    //This gives us the ability to manipulate the FPS Camera
    private Camera playerCamera;

    //This is how fast the player moves
    [SerializeField] float moveSpeed = 6f;

    //This is how fast the player moevs when they are runnin, relative to the moveSpeed;
    [SerializeField] float runMultiplier = 1.5f;

    //This is how high the player jumps
    [SerializeField] float jumpForce = 7f;

    //This is the gravity being applied to the player when they are not on the ground 
    [SerializeField] float gravity = 9.8f;

    //This is how sensitive the camera movement is based on the mouse input
    public float mouseSensitivity = 2f;

    //This is how high or low the player can look
    [SerializeField] float lookXLimit = 45f;

    //This stores the X rotation of the camera
    float rotationX = 0;

    //This represents the direction the player is moving in any given point
    Vector3 moveDirection;

    //Gives us access to the player's character controller
    CharacterController characterController;
    #endregion


    #region Stamina bar variables
    public Image staminaBar;
    public float stamina, maxStamina;
    public float runCost;
    public bool running;

    public float chargeRate;
    private Coroutine recharge;
    #endregion

    //Called before start
    void Awake()
    {

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity);

        //Rotating the player camera in the X axis
        rotationX += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        //Checks if the player is on the ground
        if (characterController.isGrounded)
        {
            //Getting the player inputs
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //Preserving the Y velocity of the player
            float movementDirectionY = moveDirection.y;

            //combining the player's local directions with player inputs
            moveDirection = (horizontalInput * transform.right) + (verticalInput * transform.forward);

            #region Jumping
            //Jumping mechanic
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
            #endregion

            #region Run
            //increases moveSpeed to the running speed
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveSpeed *= runMultiplier;
                running = true;
            }

            //Resets moveSpeed
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed /= runMultiplier;
                running = false;
            }

            #region stamina

            //Stamina drains when player is running
            if (running/* && is moving */)
            {
                stamina -= runCost * Time.deltaTime;
                if (stamina < 0)
                {
                    stamina = 0;
                    moveSpeed /= runMultiplier;
                    running = false;
                }
                staminaBar.fillAmount = stamina / maxStamina;

                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }

            #endregion stamina

            #endregion
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        //Moves the character based on inputs
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    //RechargeStamina coroutine
    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(5f);

        while (stamina < maxStamina)
        {
            stamina += chargeRate / 5f;
            if (stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.2f);
        }
    }
}
/*EXAMPLE*/