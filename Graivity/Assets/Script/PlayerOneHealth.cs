using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerOneHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            TakeDamage(20);
        }



        if (currentHealth == 0)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeathZone")
        {
            TakeDamage(20);
        }
    }
    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;

        healthBar.SetHealth(currentHealth);
    }
}
