using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Interactable
{

    [SerializeField] private Key.KeyType keyType;
    [SerializeField] private GameObject icon;
    [SerializeField] private float timer;
    private bool isActive;
    private Animator doorAnimator;
    private BoxCollider2D boxColl;
   

    private void Awake()
    {
        icon.SetActive(false);
    }

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
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
        doorAnimator.SetBool("isOpen", true);
        AudioManager.PlaySound("Open Door");
        boxColl.enabled = false;

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
