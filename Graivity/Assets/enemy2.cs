using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    public GameObject weapon;
    public GameObject weapon2;
    public SpriteRenderer sprite;
    public SpriteRenderer sprite2;
    public GameObject findPlayer;
    GameObject enemy;
    bool yes;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.GetComponent<Enemy>().foundPlayer == false)
        {
            sprite.enabled = false;
            weapon2.SetActive(true);
        }
        else
        {
            sprite.enabled = true;
            weapon2.SetActive(false);
        }
        Debug.Log(findPlayer.transform.eulerAngles.z);
        
        if(findPlayer.transform.eulerAngles.z > 270 || findPlayer.transform.eulerAngles.z < 90)
        {
            sprite.flipY = false;
        }
        else
        {
            sprite.flipY = true;
        }

        if(enemy.GetComponent<Enemy>().direction < 0)
        {
            sprite2.flipX = true;
        }
        else
        {
            sprite2.flipX = false;
        }
    }
}
