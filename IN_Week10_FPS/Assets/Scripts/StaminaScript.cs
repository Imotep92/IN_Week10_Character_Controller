using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StaminaScript : MonoBehaviour
{
    FPSControllerScript _fpsControllerScript;

    public Image staminaBar;
    public float stamina, maxStamina;
    public float runCost;

    //Called before start()
    void Awake()
    {
        //Access to FPSControllerScript 
        _fpsControllerScript = GetComponent<FPSControllerScript>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StaminaCost()
    {
        stamina -= runCost * Time.deltaTime;
        if (stamina < 0)
        {
            stamina = 0;
        }
        staminaBar.fillAmount = stamina / maxStamina;
    }
}
