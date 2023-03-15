using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float rbForce = 10;
    public float rbJumpForce = 20;
    bool isGrounded;
    bool isWallRight;
    bool isWallLeft;
    public GameObject feet;
    LayerMask groundMask;
    public float stopTime = 0.2f;
    public float wallRunDistance = 0.5f;

    float timeElapsed;
    public float lerpDuration = 0.8f;
    float endValue = 0;
    float valueToLerp;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        isWallRight = false;
        groundMask = LayerMask.GetMask("Ground");
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        isGrounded = Physics.CheckSphere(feet.transform.position, 0.2f, groundMask);
        isWallRight = Physics.Raycast(transform.position, transform.right, wallRunDistance);
        isWallLeft = Physics.Raycast(transform.position, -transform.right, wallRunDistance);
        wallRunning();
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
        //friction
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && isGrounded)
        {
            lerpMovement();     
        }
        else
        {
            timeElapsed = 0;
        }
    }

    void wallRunning()
    {
        if ((isWallRight || isWallLeft) && !isGrounded)
        {
            Debug.Log("WallFound");
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, 0);
        }
        else if (!isWallRight || !isWallLeft)
        {
            Debug.Log("WallNotFound");
            rb.useGravity = true;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y);
        }
        //wall jump left
        if (isWallRight && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            jumpLeft();
            
        }
        //wall jump right
        if (isWallLeft && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            rb.AddForce((transform.right * rbJumpForce) + (transform.up * rbJumpForce), ForceMode.Impulse);
        }

        void jumpLeft()
        {
            rb.AddForce((-transform.right * rbJumpForce) + (transform.up * rbJumpForce), ForceMode.Impulse);
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
