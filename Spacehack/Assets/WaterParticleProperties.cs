using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleProperties : MonoBehaviour
{
    public bool IsInside = true;
    Rigidbody rb;
    public float forceAmt;
    public float randomForceAmt;
    float randomX;
    float randomZ;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        randomX = Random.Range(-randomForceAmt, randomForceAmt);
        randomZ = Random.Range(-randomForceAmt, randomForceAmt);
    }

    private void FixedUpdate()
    {
        dragDown();
        dragRandomly();
    }

    void dragDown() => rb.velocity += new Vector3(0f, -forceAmt * Time.deltaTime, 0f);
    void dragRandomly() => rb.velocity += new Vector3(randomX * Time.deltaTime, 0f, randomZ*Time.deltaTime);
}
