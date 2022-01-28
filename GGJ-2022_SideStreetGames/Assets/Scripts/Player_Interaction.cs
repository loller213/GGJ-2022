using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Player_Interaction : MonoBehaviour
{
    CircleCollider2D teeth;
    AIPath aiPath;
    float defaultMaxSpeed;
    [SerializeField] float freezeDuration;
    // Start is called before the first frame update
    void Start()
    {
        teeth = GetComponent<CircleCollider2D>();
        aiPath = GetComponent<AIPath>();
        defaultMaxSpeed = aiPath.maxSpeed;
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

        if (other.tag == "Light")
        {
            aiPath.maxSpeed = 0f;
            Invoke("unFreezeEnemy", freezeDuration);
        }

    }

    void unFreezeEnemy()
    {
        aiPath.maxSpeed = defaultMaxSpeed;
    }
}
