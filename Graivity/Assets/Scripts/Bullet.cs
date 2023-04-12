using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody rb;
    GameObject enemy;
    GameObject player;
    GameObject target;

    PlayerOneHealth POH;
   
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;
        POH = player.GetComponent<PlayerOneHealth>();
    }

  
    private void OnCollisionEnter(Collision collision)
    {
  
        if (collision.transform.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            POH.TakeDamage(20);
        }
        else
        {
            target = collision.gameObject;
            if (target.GetComponent<hp>())
            {
                target.GetComponent<hp>().health -= 20;
            }
            Destroy(this.gameObject);
            target = null;
        }

    }


}
