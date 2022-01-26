using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Interactable
{

    [SerializeField] private Key.KeyType keyType;
    [SerializeField] private GameObject icon;
    [SerializeField] private float timer;
    private bool isActive;
   

    private void Awake()
    {
        icon.SetActive(false);
    }

    private void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                CloseInteractionIcon();
                timer = 3f;
            }
        }
    }

    public override void Interact()
    {
        gameObject.SetActive(false);
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenInteractionIcon()
    {
        icon.SetActive(true);
        isActive = true;
    }

    public void CloseInteractionIcon()
    {
        icon.SetActive(false);
    }
}
