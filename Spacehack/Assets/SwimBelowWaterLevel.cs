using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBelowWaterLevel : MonoBehaviour
{

    [SerializeField] GameObject fish;
    public GameObject fishFollowPoint;
    public float FollowParticlesSpeed;
    public GameObject Aquarium;
    Transform target;
    float step;
    public float speed = 10f;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        fish.transform.position = Vector3.MoveTowards(fish.transform.position, fishFollowPoint.transform.position, step);
    }
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            if(hit.transform.tag == "Water")
            {
                if(hit.transform.GetComponent<WaterParticleProperties>().IsInside)
                {
                    target = hit.transform;
                    fishFollowPoint.transform.position = hit.transform.position;
                }
            }
                
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }


    }
}
