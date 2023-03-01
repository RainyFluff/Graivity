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
    bool foundPlayer = false;
    float timer = Mathf.Infinity;
    RaycastHit objectHit;
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

        findPlayer.transform.LookAt(player.transform.position);

        
        if(Physics.Raycast(findPlayer.transform.position, findPlayer.transform.forward, out objectHit, 10) && objectHit.transform.gameObject == player)
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
                Debug.Log("shoot");
                timer = Mathf.Infinity;
            }
        }
        

    }
}
