using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSystem : MonoBehaviour
{
    [SerializeField] ParticleSystem explodeParticles;
    [SerializeField] ParticleSystem hitParticles;

    [SerializeField] int hitsToKill = 5;
    int currentHealth;

    private void Awake()
    {
        currentHealth = hitsToKill;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        HandleHit();
        //print(gameObject.name + " hit!");
        hitParticles.Play();
    }

    private void HandleHit()
    {
        currentHealth--; //TODO various towers hit power

        if (currentHealth < 1)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        var explode = Instantiate(explodeParticles, transform.position, Quaternion.identity, transform.parent);
        var exp = explode.main;
        exp.duration = 0.3f;
        explode.Play();
        Destroy(explode.gameObject, 1f);
        Destroy(gameObject);
    }
}
