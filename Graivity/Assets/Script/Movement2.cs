using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    Rigidbody rb;
    public float rbForce = 10;
    public float rbJumpForce = 20;
    bool isGrounded;
    public GameObject feet;
    LayerMask groundMask;
    float timer = Mathf.Infinity;
    float direction = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if (Input.GetKey(KeyCode.A))
        {
            if (timer == Mathf.Infinity)
            {
                timer = Time.time;
            }
            direction = -1;
            Moving();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (timer == Mathf.Infinity)
            {
                timer = Time.time;
            }
            direction = 1;
            Moving();
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
        //jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {

            rb.AddForce(transform.up * rbJumpForce, ForceMode.Impulse);
        }
        isGrounded = Physics.CheckSphere(feet.transform.position, 0.2f, groundMask);
        Debug.Log(isGrounded);
    }

    void move()
    {
        
    }
    void Moving()
    {
        if(rb.velocity.x < 5 && rb.velocity.x > -5)
        {
            rb.velocity = new Vector3(1 + ((5 * Time.time - timer)) * direction, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(5 * direction, rb.velocity.y, rb.velocity.z);
        }
       
    }
}
