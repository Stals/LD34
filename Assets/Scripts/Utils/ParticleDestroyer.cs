using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
class ParticleDestroyer : MonoBehaviour 
{
    void Start () {
        Destroy(gameObject, GetComponent<ParticleSystem>().duration);
    }
}

