using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float rbForce = 10;
    public float rbJumpForce = 20;
    bool isGrounded;
    public GameObject feet;
    LayerMask groundMask;
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
        isGrounded = Physics.CheckSphere(feet.transform.position, 0.2f, groundMask);
        Debug.Log(isGrounded);
    }

    void move()
    {
        //left
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * rbForce, ForceMode.Acceleration);
        }
        //right
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * rbForce, ForceMode.Acceleration);
        }
        //jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            
            rb.AddForce(transform.up * rbJumpForce, ForceMode.Impulse);
        }

    }
}
