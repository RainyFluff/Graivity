using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noGravity : MonoBehaviour
{
    Rigidbody rb;
    public bool inAir;
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
