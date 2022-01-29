using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTile : MonoBehaviour
{
    [SerializeField] private float timer;
    private float counter = 30f;

    private void Start()
    {
        counter = timer;
    }

    private void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

}
