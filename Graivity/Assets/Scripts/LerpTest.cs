using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    float timeElapsed;
    float lerpDuration = 0.2f;
    float endValue = 0;
    float valueToLerp;
    public Rigidbody rb;
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            lerpMovement();
        }
        else 
        {
            timeElapsed = 0;
        }
    }
    void lerpMovement()
    {
        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(rb.velocity.x, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            Debug.Log(valueToLerp);
        }

        
        

    }
}
