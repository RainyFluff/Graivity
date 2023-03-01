using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float rbForce = 10;
    public float rbJumpForce = 20;
    public float maxSpeed = 10;
    bool isGrounded;
    public GameObject feet;
    LayerMask groundMask;
    Vector3 speedCap;
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
        speedCap = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
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
        //slamdown
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl)) && !isGrounded)
        {
            rb.AddForce(-transform.up * rbJumpForce, ForceMode.VelocityChange);
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && isGrounded)
        {
            rb.drag = 10;
        }
        else
        {
            rb.drag = 0;
        }


    }
}
