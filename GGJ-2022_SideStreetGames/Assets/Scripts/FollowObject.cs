using UnityEngine;

public class FollowObject : MonoBehaviour
{

    [Header("Tweaks")]
    [SerializeField] private Transform follow;
    [SerializeField] private Vector3 offset;

    [Header("Logic")]
    private Camera cam;

    void Start()
    {
        //Assign Main Camera
        cam = Camera.main;            
    }


    void Update()
    {
        //Follow assigned object
         Vector3 pos = cam.WorldToScreenPoint(follow.position + offset);

        //Reset Object Position in cam
        if (transform.position != pos)
            transform.position = pos;                  
    }
}
