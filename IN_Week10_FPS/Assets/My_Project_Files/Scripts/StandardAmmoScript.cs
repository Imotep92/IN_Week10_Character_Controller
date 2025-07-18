using UnityEngine;

public class StandardAmmoScript : MonoBehaviour
{

    public static StandardAmmoScript _standardAmmoScript;

    public int _maxStandardAmmo = 10;

    public int _standardAmmo;


    void Awake()
    {
        _standardAmmoScript = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
