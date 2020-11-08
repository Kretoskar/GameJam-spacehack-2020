using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private WaterParticleProperties wpp;
    private AudioSource audioSource;
    [SerializeField] AudioClip[] waterDrops;
    private AudioClip randomWaterDrop;

    private void Start()
    {
        wpp = GetComponent<WaterParticleProperties>();
        audioSource = GetComponent<AudioSource>();
        chooseRandomWaterDrop();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain") audioSource.Play();
    }

    void chooseRandomWaterDrop()
    {
        randomWaterDrop = waterDrops[Random.Range(0, waterDrops.Length)];
        audioSource.clip = randomWaterDrop;
    }
        
}
