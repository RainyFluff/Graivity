using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    float timeElapsed;
    float lerpDuration = 3;
    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    float startTime;
    void Update()
    {
       if (Input.GetKey(KeyCode.K))
       {
            lerp();
       }
       

    }

    void lerp()
    {
        lerpDuration = startTime + 1;
        startTime = Time.time;
        if (startTime < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, Time.time / lerpDuration);
            
        }
        Debug.Log(valueToLerp);
    }
}
