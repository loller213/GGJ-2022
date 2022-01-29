using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTimer : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private string sceneName;
    private float counter;

    private void Start()
    {
        counter = timer;
    }

    private void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
