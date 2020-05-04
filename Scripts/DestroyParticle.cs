using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        var lifetime = particleSystem.startLifetime + particleSystem.duration;
        Destroy(gameObject, lifetime);
    }

}
