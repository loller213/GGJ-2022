using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerLightScript : MonoBehaviour
{

    [SerializeField] private bool getIsSafe;
    [SerializeField] private bool getIsOn;

    [SerializeField] private Light2D playerLight;
    [SerializeField] private GameObject playerLightGO;
    [SerializeField] private float batteryPercent;

    [SerializeField] private float maxLight;
    [SerializeField] private float minLight;


    private void Awake()
    {
        playerLightGO.SetActive(false);
    }

    void Start()
    {
        batteryPercent = Battery.currentBattery;
    }

    // Update is called once per frame
    void Update()
    {


        getIsSafe = CharacterController.isSafe;
        getIsOn = CharacterController.isOn;

        if (Input.GetKeyDown(KeyCode.L))
        {
            batteryPercent -= 10;
            //Debug.Log(batteryPercent);
        }

        //FlashlightIntensity
        if (batteryPercent <= 100 && batteryPercent >= 75)
        {
            playerLight.intensity = 0.9f;
        }
        else if (batteryPercent <= 75 && batteryPercent >= 50)
        {
            playerLight.intensity = 0.7f;
        }
        else if (batteryPercent <= 50 && batteryPercent >= 25)
        {

            minLight = 0.2f;
            maxLight = 0.5f;

            StartCoroutine(LightFlicker());
            
        }
        else if (batteryPercent <= 25 && batteryPercent >= 10)
        {
            minLight = 0.0f;
            maxLight = 0.3f;

            StartCoroutine(LightFlicker());
        }
        else if (batteryPercent <= 10)
        {

            minLight = 0.0f;
            maxLight = 0.0f;

            //StopCoroutine(LightFlicker());
            //playerLight.intensity = 0.0f;
        }

        //FlashLightOn/Off
        if (getIsOn == true)
        {
            playerLightGO.SetActive(true);
        }else if (getIsOn == false)
        {
            playerLightGO.SetActive(false);
        }

    }

    IEnumerator LightFlicker()
    {

        yield return new WaitForSeconds(1.0f);
        playerLight.intensity = Random.Range(minLight, maxLight);
        //yield return new WaitForSeconds(3.0f);
        StartCoroutine(LightFlicker());

    }

}

public class Battery
{

    public const float MaxBattery = 100;
    //public const float MinBattery = 0;

    public static float currentBattery;
    private float batteryRegenAmt;
    private float batteryConsumeAmt;

    public Battery()
    {
        currentBattery = 100f;

        batteryRegenAmt = 10f;
        batteryConsumeAmt = 5f;
    }

    public void batteryRegen()
    {
        currentBattery += batteryRegenAmt * Time.deltaTime;

        currentBattery = Mathf.Clamp(currentBattery, 0f, MaxBattery);

    }

    public void batteryConsume()
    {
        currentBattery -= batteryConsumeAmt * Time.deltaTime;

        currentBattery = Mathf.Clamp(currentBattery, 0f, MaxBattery);

    }

}