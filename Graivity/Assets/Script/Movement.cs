using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("General")]
    public float rbForce = 10;
    Rigidbody rb;
    public float rbJumpForce = 20;
    public GameObject feet;
    LayerMask groundMask;
    public float stopTime = 0.2f;
    bool isGrounded;
    float jumpsLeft;
    public float dashForce = 10f;

    [Header("CoolDowns")]
    public float dashCooldown = 5;
    float dashTimer;

    [Header("Lerp")]
    public float lerpDuration = 0.8f;
    float timeElapsed;
    float endValue = 0;
    float valueToLerp;
   
   

    [Header("WallRunning")]
    public float wallRunDistance = 0.5f;
    bool isWallRight;
    bool isWallLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        isWallRight = false;
        isWallLeft = false;
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
        if (isGrounded)
        {
            jumpsLeft = 1;
        }
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
        //dash right
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && dashTimer <= Time.time)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            rb.AddForce(transform.right * dashForce, ForceMode.Impulse);
            dashTimer = Time.time + dashCooldown;
        }
        //dash left
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && dashTimer <= Time.time)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            rb.AddForce(-transform.right * dashForce, ForceMode.Impulse);
            dashTimer = Time.time + dashCooldown;
        }
        //jump
        Vector3 rbDrag = new Vector3(0, rb.drag, 0);
        Vector3 jumpCalc = transform.up * rbJumpForce + rbDrag;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && (isGrounded || jumpsLeft >= 1) && !(isWallLeft || isWallRight))
        {
            rb.AddForce(jumpCalc, ForceMode.Impulse);
            jumpsLeft--;
        }
        //slamdown
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl)) && !isGrounded)
        {
            rb.AddForce(-transform.up * rbJumpForce, ForceMode.Acceleration);
        }
        //friction
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && isGrounded)
        {
            lerpDeacceleration();     
        }
        else
        {
            timeElapsed = 0;
        }
    }

    void wallRunning()
    {
        //wallcheck
        if ((isWallRight || isWallLeft) && !isGrounded)
        {
            Debug.Log("WallFound");
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, -1);
        }
        //wallcheckloss
        else if (!isWallRight || !isWallLeft)
        {
            Debug.Log("WallNotFound");
            rb.useGravity = true;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y);
        }
        //wall jump left
        if (isWallRight && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            rb.AddForce((-transform.right * rbJumpForce) + (transform.up * rbJumpForce), ForceMode.Impulse);
        }
        //wall jump right
        if (isWallLeft && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            rb.AddForce((transform.right * rbJumpForce) + (transform.up * rbJumpForce), ForceMode.Impulse);
        }
    }
    void lerpDeacceleration()
    {
        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(rb.velocity.x, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            rb.velocity = new Vector3(valueToLerp, rb.velocity.y, rb.velocity.z);
        }
    }
}
