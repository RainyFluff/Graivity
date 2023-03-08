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
    public float stopTime = 0.2f;
    float elapsedTime;
    float timer = Mathf.Infinity;
    
   
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
        
    }

    void move()
    {
        //left
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-rbForce, rb.velocity.y, rb.velocity.z);
            
        }
        //right
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(rbForce, rb.velocity.y, rb.velocity.z);
            

        }
        //jump
        Vector3 rbDrag = new Vector3(0, rb.drag, 0);
        Vector3 jumpCalc = transform.up * rbJumpForce + rbDrag;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.AddForce(jumpCalc, ForceMode.Impulse);
        }
        //slamdown
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl)) && !isGrounded)
        {
            rb.AddForce(-transform.up * rbJumpForce, ForceMode.Impulse);
        }
        //gravity
        /*if (!isGrounded)
        {
            rb.AddForce(0, -9.82f, 0, ForceMode.Acceleration);
        }
        */
        //friction
        /*if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && isGrounded)
        {
            
            
                float rbVelocityX = rb.velocity.x;
                float rbVelocityXFriction = Mathf.Lerp(rbVelocityX, 0, elapsedTime / stopTime);
                stopTime += Time.deltaTime;

               
                
                //rb.velocity = new Vector3(rbVelocityXFriction, rb.velocity.y, rb.velocity.z);
              
        }
        */
        //NICKES

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && isGrounded)
        {
            
            
            
            
            
            if(timer == Mathf.Infinity)
            {
                timer = Time.time;
            }

            if(rb.velocity.x > 0.3f)
            {
                rb.velocity = new Vector3(rbForce - ((Time.time - timer) * 30), rb.velocity.y, rb.velocity.z);
            }
            else if(rb.velocity.x < -0.3f)
            {
                rb.velocity = new Vector3(-rbForce + ((Time.time - timer) * 30), rb.velocity.y, rb.velocity.z);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            }
            
        }
        else
        {
            timer = Mathf.Infinity;
        }

        
        
       
        

    }
}
