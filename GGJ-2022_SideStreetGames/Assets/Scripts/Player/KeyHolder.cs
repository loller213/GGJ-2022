using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;
    GameObject bagObj;

    private void Start()
    {
        bagObj = GameObject.FindGameObjectWithTag("Bag");
    }

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }

    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added Key " + keyType);
        keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
        Destroy(GetKeyObject(keyType));
    }

    public GameObject GetKeyObject(Key.KeyType keyType)
    {
        foreach (Transform inventoryPlace in bagObj.transform)
        {
            GameObject key = inventoryPlace.transform.GetChild(0).GetChild(0).gameObject;

            if (key.CompareTag(keyType + "Key"))
            {
                return key;
            }
        }
        return null;
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Key key = collision.GetComponent<Key>();
        if (key != null)
        {
            AudioManager.PlaySound("Pickup Item");
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        KeyDoor keyDoor = collision.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                Debug.Log("Contains Key!");
                RemoveKey(keyDoor.GetKeyType());
                keyDoor.Interact();
            }
            else
            {
                keyDoor.OpenInteractionIcon();
            }
        }

        if (collision.CompareTag("LevelGoal"))
        {
            FindObjectOfType<GameControl>().PlayerWon();
        }
    }
}
