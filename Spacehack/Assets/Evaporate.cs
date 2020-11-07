using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaporate : MonoBehaviour
{
    public float evaporateParticleRadius;
    public float decreaseSizeRate;
    public GameObject waterParticlePrefab;
    public GameObject particleSystemPrefab;

    private float x, y, z;

    private void Start()
    {
        x = evaporateParticleRadius;
        y = evaporateParticleRadius;
        z = evaporateParticleRadius;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain")
        {
            GameObject particles = Instantiate(particleSystemPrefab);
            particles.transform.position = transform.position;
            StartCoroutine("graduallyDecreaseSize");
        }
    }

    IEnumerator graduallyDecreaseSize()
    {
        while (gameObject.transform.localScale.y > 0)    //    while the object is larger than desired
        {
            gameObject.transform.localScale -= Vector3.one * decreaseSizeRate*Time.deltaTime;
            yield return null;    //    wait a frame
        }
        Destroy(gameObject);
        yield break;
    }

    private void OnDestroy()
    {
        Debug.Log("Zniknąłem");
    }
}
