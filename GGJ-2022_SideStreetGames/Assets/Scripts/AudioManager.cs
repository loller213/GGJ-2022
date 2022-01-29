using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip backGroundMusic;

    public static AudioClip playerDied;
    public static AudioClip currentClip;
    public static AudioSource audioSource;
    
    void Start()
    {
        playerDied = Resources.Load<AudioClip>("GameOver");
    }

    public void SetAudioManager()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            currentClip = backGroundMusic;
            Debug.Log("Audio clip switched");
        }
        else
            return;
    }

    public static void PlaySound(string soundType)
    {
        switch (soundType)
        {
            case "Player Died":
                audioSource.loop = false;
                audioSource.clip = playerDied;
                audioSource.Play();
                audioSource = null;
                break;

            case "Pause game":
                audioSource.volume = 0.2f;
                break;

            case "Resume game":
                audioSource.volume = 0.5f;
                break;

            case "Play level music":
                Debug.Log("Music Playing");
                audioSource.clip = currentClip;
                audioSource.loop = true;
                audioSource.Play();
                break;

            default:
                return;
        }
    }
}
