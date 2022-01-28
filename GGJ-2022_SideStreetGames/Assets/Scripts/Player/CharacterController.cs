using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    //[SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private float speed;
    [SerializeField] private float aimSpeed;
    private Rigidbody2D rb;
    private Vector2 boxSize = new Vector2(0.1f, 1f);
    private float currentSpeedX, currentSpeedY;

    public static bool isSafe;
    public static bool isOn;
    public static bool isAlive;

    public Animator animator;
    //private Vector2 aimDir;

    void Start()
    {
        isOn = false;
        rb = GetComponent<Rigidbody2D>();
        isAlive = true;
    }

    void Update()
    {


        LookAtMouse();
        Movement();

        FlashlightOnOff();

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }

    private void LookAtMouse()
    {
        /*
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = mousePos - new Vector2(transform.position.x, transform.position.y);
        //transform.right = Vector2.Lerp(mousePos, new Vector2(transform.position.x, transform.position.y), Time.deltaTime);
        */

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, aimSpeed * Time.deltaTime);

    }

    private void Movement()
    {

        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = input.normalized * speed;

        currentSpeedX = rb.velocity.x;
        currentSpeedY = rb.velocity.y;

        animator.SetFloat("Speed", Mathf.Abs(currentSpeedX + currentSpeedY));

    }

    public void OpenInteractableIcon()                          //This Function Should be on Game Control but I'll just fix it on the process of setting up the interactable objects
    {
        Debug.Log("Currently Interacting");
        
    }
                                                            
    public void CloseInteractableIcon()                        //This Function Should be on Game Control but I'll just fix it on the process of setting up the interactable objects
    {

    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    private void FlashlightOnOff()
    {

        if (Input.GetKeyDown(KeyCode.E) && isOn == false)
        {
            isOn = true;
        }else if (Input.GetKeyDown(KeyCode.E) && isOn == true)
        {
            isOn = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Safe Area"))
        {
            isSafe = true;
        }
        else
        {
            isSafe = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Safe Area"))
        {
            isSafe = false;
        }

    }


}
