using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject Prefab;
    public float Radius;
    public float Mass;
    public float RestDensity;
    public float Viscosity;
    public float Drag;

    public bool WallsUp;
    public int Amount;
    public int PerRow;
    public List<GameObject> Walls = new List<GameObject>();

    private float smoothingRadius = 1.0f;
    private Vector3 gravity = new Vector3(0.0f, -9.81f, 0.0f);
    private float gravityMultiplicator = 2000.0f;
    private float gas = 2000.0f;
    private float deltaTime = 0.0008f;
    private float damping = -0.5f;

    private Particle[] particles;
    private ParticleCollider[] colliders;
    private bool clearing;


    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        CalculateForces();
        ParticleMovement();
        CalculateCollisions();
        
    }
    private void Initialize()
    {
        particles = new Particle[Amount];

        for (int i = 0; i < Amount; i++)
        {
            float x = (i % PerRow) + UnityEngine.Random.Range(-0.1f, 0.1f);
            float y = (2 * Radius) + (float)((i / PerRow) / PerRow) * 1.1f;
            float z = ((i / PerRow) % PerRow) + UnityEngine.Random.Range(-0.1f, 0.1f);

            GameObject currentGo = Instantiate(Prefab);
            Particle currentParticle = currentGo.AddComponent<Particle>();
            particles[i] = currentParticle;

            currentGo.transform.localScale = Vector3.one * Radius;
            currentGo.transform.position = new Vector3(x, y, z);

            currentParticle.Go = currentGo;
            currentParticle.Position = currentGo.transform.position;
        }
    }

    private void CalculateForces()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            if(clearing)
            {
                return;
            }

            for(int j = 0; j < particles.Length; j++)
            {
                Vector3 direction = particles[j].Position - particles[i].Position;
                float distance = direction.magnitude;

                particles[i].Density = ParticleDensity(particles[i], distance);
                particles[i].Pressure = gas * (particles[i].Density - RestDensity);
            }
        }
    }

    private float ParticleDensity(Particle _currentParticle, float _distance)
    {
        if(_distance < smoothingRadius)
        {
            return _currentParticle.Density += Mass * (315.0f / (64.0f * Mathf.PI * Mathf.Pow(smoothingRadius, 9.0f))) *
                Mathf.Pow(smoothingRadius - _distance, 3.0f);
        }
        return _currentParticle.Density;
    }

    private void ParticleMovement()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            if (clearing)
            {
                return;
            }

            Vector3 forcePressure = Vector3.zero;
            Vector3 forceViscosity = Vector3.zero;

            for(int j = 0; j < particles.Length; j++)
            {
                if (i == j) continue;

                Vector3 direction = particles[j].Position - particles[i].Position;
                float distance = direction.magnitude;

                forcePressure += ParticlePressure(particles[i], particles[j], direction, distance);
                forceViscosity += ParticleViscosity(particles[i], particles[j], distance);
            }

            Vector3 forceGravity = gravity * particles[i].Density * gravityMultiplicator;

            particles[i].CombinedForce = forcePressure + forceViscosity + forceGravity;
            particles[i].Velocity += deltaTime * (particles[i].CombinedForce) / particles[i].Density;
            particles[i].Position += deltaTime * (particles[i].Velocity);
            particles[i].Go.transform.position = particles[i].Position;
        }
    }

    private Vector3 ParticlePressure(Particle _currentParticle, Particle _nextParticle, Vector3 _direction, float _distance)
    {
        if(_distance < smoothingRadius)
        {
            return -1 * (_direction.normalized) * Mass * (_currentParticle.Pressure + _nextParticle.Pressure) / (2.0f * _nextParticle.Density) *
                (-45.0f / (Mathf.Pow(smoothingRadius, 6.0f))) * Mathf.Pow(smoothingRadius - _distance, 2.0f);
        }
        return Vector3.zero;
    }

    private Vector3 ParticleViscosity(Particle _currentParticle, Particle _nextParticle, float _distance)
    {
        if(_distance < smoothingRadius)
        {
            return Viscosity * Mass * (_nextParticle.Velocity - _currentParticle.Velocity) / _nextParticle.Density * (45.0f / (Mathf.PI *
                Mathf.Pow(smoothingRadius, 6.0f))) *(smoothingRadius - _distance);
        }

        return Vector3.zero;
    }

    private void CalculateCollisions()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            for (int j = 0; j < colliders.Length; j++)
            {
                if(clearing || colliders.Length == 0)
                {
                    return;
                }

                Vector3 penetrationNormal;
                Vector3 penetrationPosition;
                float penetrationLength;
                if(Collision(colliders[j], particles[i].Position, Radius, out penetrationNormal, out penetrationPosition, out penetrationLength))
                {
                    particles[i].Velocity = DampenVelocity(colliders[j], particles[i].Velocity, penetrationNormal, 1.0f - Drag);
                    particles[i].Position = penetrationPosition - penetrationNormal * Mathf.Abs(penetrationLength);
                }
            }
        }
    }

    private static bool Collision(ParticleCollider collider, Vector3 position, float radius, out Vector3 penetrationNormal, out Vector3 penetrationPosition,
        out float penetrationLength)
    {
        Vector3 colliderProjection = collider.Position - position;

        penetrationNormal = Vector3.Cross(collider.Right, collider.Up);
        penetrationLength = Mathf.Abs(Vector3.Dot(colliderProjection, penetrationNormal)) - (radius / 2.0f);
        penetrationPosition = collider.Position - colliderProjection;

        return penetrationLength < 0.0f
            && Mathf.Abs(Vector3.Dot(colliderProjection, collider.Right)) < collider.Scale.x
            && Mathf.Abs(Vector3.Dot(colliderProjection, collider.Up)) < collider.Scale.y;
    }

    private Vector3 DampenVelocity(ParticleCollider collider, Vector3 velocity, Vector3 penetrationNormal, float drag)
    {
        Vector3 newVelocity = Vector3.Dot(velocity, penetrationNormal) * penetrationNormal * damping + Vector3.Dot(velocity, collider.Right) *
            collider.Right * drag + Vector3.Dot(velocity, collider.Up) * collider.Up * drag;

        return Vector3.Dot(newVelocity, Vector3.forward) * Vector3.forward + Vector3.Dot(newVelocity, Vector3.right) * Vector3.right
            + Vector3.Dot(newVelocity, Vector3.up) * Vector3.up;
    }
}
