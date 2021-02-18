using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DougEntrance : MonoBehaviour
{

    private Animator anim;
    public GameObject sprites;

    public Transform[] smokeParticleLocations;
    public GameObject smokeParticleEffect;
    public Transform[] fireforksParticleLocations;
    public GameObject fireworkParticleEffect;

    public GameObject DougShowTheme;
    public GameObject CrowdCheering;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 7f);
    }

    public void ReleaseSmokeParticles()
    {
        for (int i = 0; i < smokeParticleLocations.Length; i++)
        {
            Instantiate(smokeParticleEffect, smokeParticleLocations[i].position, Quaternion.Euler(-90, 0, 0));
        }
    }

    public void ReleaseFireworksParticles()
    {
        for (int i = 0; i < fireforksParticleLocations.Length; i++)
        {
            Instantiate(fireworkParticleEffect, fireforksParticleLocations[i].position, Quaternion.Euler(-90, 0, 0));
        }
    }

    public void StartDougShowTheme()
    {
        Instantiate(DougShowTheme, transform.position, transform.rotation);
    }

    public void StartCrowdCheering()
    {
        Instantiate(CrowdCheering, transform.position, transform.rotation);
    }
}
