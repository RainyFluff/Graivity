using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    Rigidbody rb;
    public float rbForce = 10;
    float originalForce;
    public float rbJumpForce = 20;
    bool isGrounded;
    public GameObject feet;
    LayerMask groundMask;
    public float stopTime = 0.2f;

    float timeElapsed;
    public float lerpDuration = 0.2f;
    float endValue = 0;
    float valueToLerp;

    float timer = Mathf.Infinity;
    float dashCooldown = -5;
    float timeRunning = Mathf.Infinity;
    float runningBonus = 10;
    int jumpsleft = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        groundMask = LayerMask.GetMask("Ground");
        originalForce = rbForce;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        isGrounded = Physics.CheckSphere(feet.transform.position, 0.1f, groundMask);
    }

    void move()
    {
        //left
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-rbForce, rb.velocity.y, rb.velocity.z);
            if(timeRunning == Mathf.Infinity || Input.GetKey(KeyCode.D))
            {
                timeRunning = Time.time;
            }
        }
       

        //right
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(rbForce, rb.velocity.y, rb.velocity.z);
            if (timeRunning == Mathf.Infinity)
            {
                timeRunning = Time.time;
            }
        }
        else
        {
            timeRunning = Mathf.Infinity;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - dashCooldown > 5)
        {
            if(timer == Mathf.Infinity)
            {
                timer = Time.time;
                dashCooldown = Time.time;
            }
        }

        if (isGrounded)
        {
            jumpsleft = 1;
        }
        //jump
        Vector3 rbDrag = new Vector3(0, rb.drag, 0);
        Vector3 jumpCalc = transform.up * rbJumpForce + rbDrag;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && (isGrounded || jumpsleft >= 1))
        {
            rb.AddForce(jumpCalc, ForceMode.Impulse);
            jumpsleft--;
        }
        //slamdown
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl)) && !isGrounded)
        {
            rb.AddForce(-transform.up * rbJumpForce, ForceMode.Impulse);
        }
        //friction
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && isGrounded)
        {
            lerpMovement();
        }
        else
        {
            timeElapsed = 0;
        }


        if(timeRunning == Mathf.Infinity)
        {
            rbForce = originalForce;
        }

        else if(Time.time - timeRunning > 1)
        {
            if(Time.time - timeRunning < 4)
            {
                rbForce = originalForce + ((Time.time - (timeRunning + 1)) * 3.3f);
            }
            else
            {
                rbForce = originalForce + runningBonus;
            }
        }

        else if(Time.time - timer > 0.1f && Time.time - timer < 1)
        {
            rbForce = (originalForce * 3 - ((Time.time - timer) * 20));
        }

        else if(Time.time - timer < 0 || Time.time - timer >0.6f)
        {
            timer = Mathf.Infinity;
            rbForce = originalForce;  
        }

        

        
    }
    void lerpMovement()
    {
        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(rb.velocity.x, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            rb.velocity = new Vector3(valueToLerp, rb.velocity.y, rb.velocity.z);
        }
    }
}
