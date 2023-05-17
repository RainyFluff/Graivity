using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public ParticleSystem particles;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.MainModule psMain = particles.main;
        ParticleSystem.ShapeModule psShape = particles.shape;
        ParticleSystem.ForceOverLifetimeModule psForceLifetime = particles.forceOverLifetime;
        psForceLifetime.x = speed;
        psForceLifetime.y = speed;
        

    }
}
