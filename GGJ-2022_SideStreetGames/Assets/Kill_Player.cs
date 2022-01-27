using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Player : MonoBehaviour
{
    GameObject Player;
    Collider teeth;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        teeth.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collide");
        if(collision.gameObject == Player)
        {
            Debug.Log("Found Player");
        }
    }
}
