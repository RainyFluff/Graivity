using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody rb;
    GameObject enemy;
   
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;
    }

  
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.transform.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }


}
