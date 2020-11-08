using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] int numberOfFish;
    [SerializeField] GameObject fishPrefab;
    [SerializeField] GameObject movePointSpawnerPrefab;
    [SerializeField] Transform bajoro;
    
    private void Start()
    {
        for(int i = 0; i<numberOfFish; i++)
        {
            GameObject fish = Instantiate(fishPrefab);
            //fish.transform.SetParent()
        }

        Instantiate(movePointSpawnerPrefab);
    }
}
