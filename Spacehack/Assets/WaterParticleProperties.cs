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
    public int everyNthSplashes;
    public int splashNumber; //Debug
    public bool isSplashing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        splashNumber = Random.Range(1, everyNthSplashes);
        if (splashNumber == 1) isSplashing = true;
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
