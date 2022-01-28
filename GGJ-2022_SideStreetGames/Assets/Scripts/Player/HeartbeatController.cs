using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeartbeatController : MonoBehaviour
{
    [SerializeField] private AudioClip heartbeatClip;
    [SerializeField] private int furthestDetectionRange;
    [SerializeField] private int nearestDetectionRange;

    private AudioSource audioSource;
    private float nearestEnemyDistance;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = heartbeatClip;
        audioSource.loop = true;
        audioSource.volume = 0;
        audioSource.playOnAwake = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        nearestEnemyDistance = furthestDetectionRange;
        float detectionRange = furthestDetectionRange - nearestDetectionRange;

        Vector2 object2DPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(object2DPosition, furthestDetectionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Enemy")
            {
                Vector2 enemyPosition = hitCollider.gameObject.transform.position;
                float enemyDistance = Vector2.Distance(enemyPosition, object2DPosition);
                nearestEnemyDistance = enemyDistance < nearestEnemyDistance ? enemyDistance:nearestEnemyDistance;


                Debug.Log("Nearest enemy distance: " + nearestEnemyDistance);
                if (nearestEnemyDistance <= nearestDetectionRange)
                {
                    audioSource.volume = 1;
                }
                else
                {
                    audioSource.volume = 1 - ((nearestEnemyDistance - nearestDetectionRange) / (furthestDetectionRange - nearestDetectionRange));
                }

                audioSource.pitch = audioSource.volume * 3;
                audioSource.panStereo = ((enemyPosition.x-object2DPosition.x) / furthestDetectionRange);
            }
        }

        //Fail safe when no enemy is detected, check if it reached outermost player detection range
        //And mute the volume
        if (nearestEnemyDistance == furthestDetectionRange)
        {
            audioSource.volume = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, furthestDetectionRange);

        Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(transform.position, nearestDetectionRange);
    }
}
