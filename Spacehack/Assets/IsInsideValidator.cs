using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInsideValidator : MonoBehaviour
{
    WaterParticleProperties wpp;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Water")
        {
            wpp = other.GetComponent<WaterParticleProperties>();
            wpp.IsInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Water")
        {
            wpp = other.GetComponent<WaterParticleProperties>();
            wpp.IsInside = false;
        }
    }
}
