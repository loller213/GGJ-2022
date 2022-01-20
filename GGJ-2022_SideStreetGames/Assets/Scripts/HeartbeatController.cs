using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeartbeatController : MonoBehaviour
{
    [SerializeField] private AudioClip heartbeatClip;
    [SerializeField] private int detectionRange;

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
        nearestEnemyDistance = detectionRange;

        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, detectionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Enemy")
            {
                float enemyDistance = Vector3.Distance(hitCollider.gameObject.transform.position, gameObject.transform.position);
                nearestEnemyDistance = enemyDistance < nearestEnemyDistance ? enemyDistance:nearestEnemyDistance;

                audioSource.volume = 1 -(nearestEnemyDistance / detectionRange);
                audioSource.pitch = audioSource.volume * 3;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
