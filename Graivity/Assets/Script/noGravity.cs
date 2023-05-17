using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noGravity : MonoBehaviour
{
    Rigidbody rb;
    public bool inAir;
    public ParticleSystem fallingParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NoGravity"))
        {
            GetComponent<Movement>().enabled = false;
            epicFloat();
            inAir = true;
            ParticleSystem.MainModule fallingMain = fallingParticleSystem.main;
            ParticleSystem.ForceOverLifetimeModule fallingForceLifetime = fallingParticleSystem.forceOverLifetime;
            fallingForceLifetime.x = -rb.velocity.x;
            fallingForceLifetime.y = -rb.velocity.y;

        }
    }
    private void OnTriggerExit(Collider other)
    { 
        if(other.CompareTag("NoGravity"))
        {
            GetComponent<Movement>().enabled = true;
            inAir = false;
        }
    }


    void epicFloat()
    {
        rb.useGravity = false;
        rb.drag = 0;
    }
}
