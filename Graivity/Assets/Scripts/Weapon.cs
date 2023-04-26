using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    float timer;
    public float TimePerShot = 0.5f;

    private Camera mainCam;
    private Vector3 mousePos;

    GameObject player;
    Rigidbody rb;
    public float gravityForce = 5;

   void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        timer = Time.time;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time - timer > TimePerShot)
        {
            Shoot();
            timer = Time.time;
        }

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
       

    }

    void Shoot()
    {
        Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
        if(player.GetComponent<noGravity>().inAir == true)
        {
            rb.AddForce(-transform.right * gravityForce, ForceMode.Impulse);
        }
    }
}
