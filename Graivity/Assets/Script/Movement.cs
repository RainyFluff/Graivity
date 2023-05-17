using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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

    [Header("Slamdown")]
    public float timeForSlam = 0.6f;
    public float slamdownSpeed = 100;
    float slamTimer = Mathf.Infinity;
    

    [Header("WallRunning")]
    public float wallRunDistance = 0.5f;
    bool isWallRight;
    bool isWallLeft;
    bool canWallRide;

    [Header("PrettyJuice")]
    public ParticleSystem jumpParticleSystem;
    public ParticleSystem fallingParticleSystem;

    float originalDashForce;
    RaycastHit hit;
    float distance1;
    float distance2;
    float distance3;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        isWallRight = false;
        isWallLeft = false;
        groundMask = LayerMask.GetMask("Ground");

        originalDashForce = dashForce;
        distance1 = dashForce;
        distance2 = dashForce;
        distance3 = dashForce;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        isGrounded = Physics.CheckSphere(feet.transform.position, 0.05f, groundMask);
        isWallRight = Physics.Raycast(transform.position, transform.right, wallRunDistance);
        isWallLeft = Physics.Raycast(transform.position, -transform.right, wallRunDistance);
        wallRunning();
        particleForces();
        if (isGrounded)
        {
            jumpsLeft = 1;
        }
    }

    void move()
    {
        //left
        if (Input.GetKey(KeyCode.A) && isGrounded)
        {
            rb.velocity = new Vector3(-rbForce, rb.velocity.y, rb.velocity.z);
        }
        if (Input.GetKey(KeyCode.A) && !isGrounded)
        {
            rb.AddForce(-rbForce * 0.1f,0,0, ForceMode.Acceleration);
        }
        //right
        if (Input.GetKey(KeyCode.D) && isGrounded)
        {
            rb.velocity = new Vector3(rbForce, rb.velocity.y, rb.velocity.z);
        }
        if (Input.GetKey(KeyCode.D) && !isGrounded)
        {
            rb.AddForce(rbForce * 0.1f, 0, 0, ForceMode.Acceleration);
        }
        //dash right
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && dashTimer <= Time.time && isGrounded)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z), transform.right, out hit, originalDashForce))
            {
                distance1 = Mathf.Abs(transform.position.x - hit.transform.position.x) - hit.transform.localScale.x / 2;
            }

            if (Physics.Raycast(transform.position, transform.right, out hit, originalDashForce))
            {
                distance2 = Mathf.Abs(transform.position.x - hit.transform.position.x) - hit.transform.localScale.x / 2;
            }

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z), transform.right, out hit, originalDashForce))
            {
                distance3 = Mathf.Abs(transform.position.x - hit.transform.position.x) - hit.transform.localScale.x / 2;
            }

            if (distance1 < distance2)
            {
                if (distance1 < distance3)
                {
                    dashForce = distance1;
                }
            }
            else if (distance2 < distance3)
            {
                dashForce = distance2;
            }
            else
            {
                dashForce = distance3;
            }

            transform.position = new Vector3(transform.position.x + dashForce - 0.51f, transform.position.y, transform.position.z);

            dashForce = originalDashForce;

            dashTimer = Time.time + dashCooldown;
            /*transform.position = new Vector3(transform.position.x + dashForce, transform.position.y, transform.position.z);
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            rb.AddForce(transform.right * dashForce, ForceMode.Impulse);
            dashTimer = Time.time + dashCooldown; */
        }
        //dash left
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && dashTimer <= Time.time && isGrounded)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z), -transform.right, out hit, originalDashForce))
            {
                distance1 = Mathf.Abs(transform.position.x - hit.transform.position.x) - hit.transform.localScale.x / 2;
            }

            if (Physics.Raycast(transform.position, -transform.right, out hit, originalDashForce))
            {
                distance2 = Mathf.Abs(transform.position.x - hit.transform.position.x) - hit.transform.localScale.x / 2;
            }

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z), -transform.right, out hit, originalDashForce))
            {
                distance3 = Mathf.Abs(transform.position.x - hit.transform.position.x) - hit.transform.localScale.x / 2;
            }

            if (distance1 < distance2)
            {
                if (distance1 < distance3)
                {
                    dashForce = distance1;
                }
            }
            else if (distance2 < distance3)
            {
                dashForce = distance2;
            }
            else
            {
                dashForce = distance3;
            }

            transform.position = new Vector3(transform.position.x - (dashForce - 0.51f), transform.position.y, transform.position.z);

            dashForce = originalDashForce;

            dashTimer = Time.time + dashCooldown;
            //transform.position = new Vector3(transform.position.x - dashForce, transform.position.y, transform.position.z);
            //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            //rb.AddForce(-transform.right * dashForce, ForceMode.Impulse);
            //dashTimer = Time.time + dashCooldown;
        }
        //jump
        Vector3 rbDrag = new Vector3(0, rb.drag, 0);
        Vector3 jumpCalc = transform.up * rbJumpForce + rbDrag;
        if ((Input.GetKeyDown(KeyCode.Space)) && (isGrounded || jumpsLeft >= 1) && !(isWallLeft || isWallRight))
        {
            rb.AddForce(jumpCalc, ForceMode.Impulse);
            jumpsLeft--;
            jumpParticleSystem.transform.position = new Vector3(transform.position.x, transform.position.y-0.6f, transform.position.z);
            jumpParticleSystem.Play();
        }
        //slamdown
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl)) && !isGrounded)
        {
            if(slamTimer == Mathf.Infinity)
            {
                slamTimer = Time.time;
            }
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
        //slam
        if (Time.time - slamTimer < timeForSlam && Time.time - slamTimer > 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.drag = 0;
        }
        if (Time.time - slamTimer > timeForSlam && !isGrounded)
        {
            rb.velocity = new Vector3(0, (Time.time - slamTimer) * -slamdownSpeed, 0);
        }
        if (isGrounded)
        {
            slamTimer = Mathf.Infinity;
        }

        if(slamTimer != Mathf.Infinity)
        {
            canWallRide = false;
        }
        else
        {
            canWallRide = true;
        }
    }

    void wallRunning()
    {
        if (canWallRide)
        {
            //wallcheck
            if ((isWallRight || isWallLeft) && !isGrounded)
            {
                rb.useGravity = false;
                rb.velocity = new Vector3(rb.velocity.x, -1);
            }
            //wallcheckloss
            else if (!isWallRight || !isWallLeft)
            {
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
    }
    void particleForces()
    {
        ParticleSystem.MainModule fallingMain = fallingParticleSystem.main;
        ParticleSystem.ForceOverLifetimeModule fallingForceLifetime = fallingParticleSystem.forceOverLifetime;
        fallingForceLifetime.x = -rb.velocity.x;
        fallingForceLifetime.y = -rb.velocity.y;
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
