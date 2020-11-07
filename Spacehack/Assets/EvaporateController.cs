using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaporateController : MonoBehaviour
{
    [SerializeField] float evaporationTime = default;
    [SerializeField] float evaporationDecrease = default;

    private ParticleSystem evaporationParticleSystem;
    private float timeLeft;

    float r, g, b, a;
    ParticleSystem.MainModule main;


    private void Start()
    {
        evaporationParticleSystem = GetComponent<ParticleSystem>();
        timeLeft = evaporationTime;
        
        
        main = evaporationParticleSystem.main;
        r = main.startColor.color.r;
        g = main.startColor.color.g;
        b = main.startColor.color.b;
        a = main.startColor.color.a;

        StartCoroutine("DeleteParticlesCorutine");
    }
    IEnumerator DeleteParticlesCorutine()
    {
        while(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            a -= Time.deltaTime;
            main.startColor = new Color(r, g, b, a);
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }
}
