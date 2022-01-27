using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Player : MonoBehaviour
{
    CircleCollider2D teeth;
    // Start is called before the first frame update
    void Start()
    {
        teeth = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
