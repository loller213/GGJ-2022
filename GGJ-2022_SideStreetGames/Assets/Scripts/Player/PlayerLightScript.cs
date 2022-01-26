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



    private void Awake()
    {
        playerLightGO.SetActive(false);
    }

    void Start()
    {
        batteryPercent = 100;
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

        if (batteryPercent < 100 && batteryPercent > 70)
        {
            playerLight.intensity = 0.9f;
        }
        else if (batteryPercent < 70 && batteryPercent > 30)
        {
            playerLight.intensity = 0.7f;
        }
        else if (batteryPercent < 30 && batteryPercent > 10)
        {
            playerLight.intensity = Mathf.PingPong(0.2f, 0.7f);
        }else if (batteryPercent < 10)
        {
            playerLight.intensity = 0.0f;
        }

        if (getIsOn == true)
        {
            playerLightGO.SetActive(true);
        }else if (getIsOn == false)
        {
            playerLightGO.SetActive(false);
        }

    }
}
