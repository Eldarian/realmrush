using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSound : MonoBehaviour
{

    AudioSource laserShot;

    // Start is called before the first frame update
    void Start()
    {
        laserShot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var bulletsEmitter = GetComponent<ParticleSystem>().emission;
        //bulletsEmitter.
    }
}
