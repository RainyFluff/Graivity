using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    float direction = 1;
    float lastDirection = 1;
    public float speed = 5;
    GameObject player;
    public GameObject findPlayer;
    public GameObject bulletSpawn;
    public GameObject weapon;
    bool foundPlayer = false;
    float timer = Mathf.Infinity;
    RaycastHit objectHit;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(direction);
        if(!Physics.Raycast(new Vector3(transform.position.x + direction/2, transform.position.y, transform.position.z), -transform.up, 1))
        {
            direction *= -1;
        }
        Vector3 difference = player.transform.position - findPlayer.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        findPlayer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //findPlayer.transform.LookAt(player.transform.position);

        
        if(Physics.Raycast(findPlayer.transform.position, findPlayer.transform.right, out objectHit, 10) && objectHit.transform.gameObject.tag == "Player")
        {
            //lastDirection = direction;
            //direction = 0;
            foundPlayer = true;
        }

        else
        {
            foundPlayer = false;
            //direction = lastDirection;
            timer = Mathf.Infinity;
            transform.Translate(transform.right * 5 * direction * Time.deltaTime);
        }


        if (foundPlayer)
        {
            if(timer == Mathf.Infinity)
            {
                timer = Time.time;
            }

            if(Time.time - timer > 2)
            {
                shoot();
                timer = Mathf.Infinity;
            }
            weapon.transform.position = bulletSpawn.transform.position;
            weapon.transform.rotation = bulletSpawn.transform.rotation;
        }

        else
        {
            if(direction > 0)
            {
                weapon.transform.position = new Vector3(transform.position.x + 1, transform.position.y + 0.5f, transform.position.z);
                weapon.transform.rotation = transform.rotation;
            }
            else
            {
                weapon.transform.position = new Vector3(transform.position.x - 1, transform.position.y + 0.5f, transform.position.z);
                weapon.transform.rotation = transform.rotation;
            }
        }
        

    }

    void shoot()
    {
        Instantiate(bullet, bulletSpawn.transform.position, findPlayer.transform.rotation);
    }
}
