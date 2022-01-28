using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class PlayerLightScript : MonoBehaviour
{

    [SerializeField] private bool getIsSafe;
    [SerializeField] private bool getIsOn;

    [SerializeField] private Light2D playerLight;
    [SerializeField] private GameObject playerLightGO;
    [SerializeField] private float batteryPercent;

    [SerializeField] private float maxLight;
    [SerializeField] private float minLight;

    [SerializeField] private float MaxBattery = 100f;

    //[SerializeField] private float currentBattery = 100f;
    [SerializeField] private float batteryRegenAmt;
    [SerializeField] private float batteryConsumeAmt;

    [SerializeField] private TextMeshProUGUI batteryText;

    [SerializeField] private GameObject visionCone;

    private void Awake()
    {
        playerLightGO.SetActive(false);
    }

    void Start()
    {
        visionCone.SetActive(false);
        batteryPercent = 100f;
        batteryText.text = Mathf.RoundToInt(batteryPercent) + "";
    }

    // Update is called once per frame
    void Update()
    {

        batteryText.text = Mathf.RoundToInt(batteryPercent) + "";

        getIsSafe = CharacterController.isSafe;
        getIsOn = CharacterController.isOn;

        //ConsumeBattery
        if (getIsOn == true && getIsSafe == false)
        {
            batteryConsume();
        }

        //RegenBattery
        if (getIsOn == false && getIsSafe == false)
        {
            batteryRegen();
        }


        //FlashlightIntensity
        if (batteryPercent <= 100 && batteryPercent >= 75)
        {
            visionCone.SetActive(true);

            //playerLight.intensity = 0.9f;
            StartCoroutine(LightFlicker());
            minLight = 0.9f;
            maxLight = 0.9f;

        }
        else if (batteryPercent <= 75 && batteryPercent >= 50)
        {

            minLight = 0.7f;
            maxLight = 0.7f;

        }
        else if (batteryPercent <= 50 && batteryPercent >= 25)
        {

            minLight = 0.2f;
            maxLight = 0.5f;
            
        }
        else if (batteryPercent <= 25 && batteryPercent >= 5)
        {

            visionCone.SetActive(false);
            minLight = 0.0f;
            maxLight = 0.3f;

        }
        else if (batteryPercent <= 5)
        {

            minLight = 0.0f;
            maxLight = 0.0f;

            //getIsOn = false;

        }

        //Vision Cone Collider is inactive when flashlight is off.
        //FlashLightOn/Off
        if (getIsOn == true)
        {
            playerLightGO.SetActive(true);
            visionCone.SetActive(true);
        }else if (getIsOn == false)
        {
            playerLightGO.SetActive(false);
            visionCone.SetActive(false);
        }

    }

    public void batteryRegen()
    {
        batteryPercent += batteryRegenAmt * Time.deltaTime;

        batteryPercent = Mathf.Clamp(batteryPercent, 0f, MaxBattery);

    }

    public void batteryConsume()
    {
        batteryPercent -= batteryConsumeAmt * Time.deltaTime;

        batteryPercent = Mathf.Clamp(batteryPercent, 0f, MaxBattery);

    }

    IEnumerator LightFlicker()
    {

        yield return new WaitForSeconds(1.0f);
        playerLight.intensity = Random.Range(minLight, maxLight);
        //yield return new WaitForSeconds(3.0f);
        StartCoroutine(LightFlicker());

    }

}

/*
public class Battery
{

    public const float MaxBattery = 100;

    public static float currentBattery = 100f;
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

*/