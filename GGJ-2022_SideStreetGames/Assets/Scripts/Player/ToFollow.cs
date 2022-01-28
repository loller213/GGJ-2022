using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToFollow : MonoBehaviour
{
    [SerializeField] private GameObject follow;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        transform.position = follow.transform.position + offset;
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = mousePos - new Vector2(transform.position.x, transform.position.y);
        Vector3 aimDir = transform.right;

    }
}
