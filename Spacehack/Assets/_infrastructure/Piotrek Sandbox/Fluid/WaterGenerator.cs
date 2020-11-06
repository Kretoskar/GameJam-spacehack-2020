using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    private Particle[] particles;
    private ParticleCollider[] colliders;
    [SerializeField] WaterManager waterManager = default;
    private int amount;
    private int perRow;
    private float radius;
    private GameObject prefab;

    private void Start()
    {
        amount = waterManager.Amount;
        perRow = waterManager.PerRow;
        radius = waterManager.Radius;
        prefab = waterManager.Prefab;
        Initialize();
    }
    private void Initialize()
    {
        particles = new Particle[amount];

        for (int i = 0; i < amount; i++)
        {
            float x = (i % perRow) + Random.Range(-0.1f, 0.1f);
            float y = (2 * perRow) + ((i / perRow) / perRow) * 1.1f;
            float z = ((i / perRow) % perRow) + Random.Range(-0.1f, 0.1f);

            GameObject currentGo = Instantiate(prefab);
            Particle currentParticle = currentGo.AddComponent<Particle>();
            particles[i] = currentParticle;

            currentGo.transform.localScale = Vector3.one * radius;
            currentGo.transform.position = new Vector3(x, y, z);

            currentParticle.Go = currentGo;
            currentParticle.Position = currentGo.transform.position;
        }
    }
}

