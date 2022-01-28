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

        Vector2 object2DPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(object2DPosition, furthestDetectionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Enemy")
            {
                Debug.Log("ENEMY FOUND");
                Vector2 enemyPosition = hitCollider.gameObject.transform.position;
                float enemyDistance = Vector2.Distance(enemyPosition, object2DPosition);
                Debug.Log("enemy distance: " + enemyDistance);
                nearestEnemyDistance = enemyDistance < nearestEnemyDistance ? enemyDistance:nearestEnemyDistance;

                audioSource.volume = 1 -(nearestEnemyDistance / furthestDetectionRange);
                audioSource.pitch = audioSource.volume * 3;
                audioSource.panStereo = ((enemyPosition.x-object2DPosition.x) / furthestDetectionRange);
                Debug.Log("stereo:" + audioSource.panStereo);
            }
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
