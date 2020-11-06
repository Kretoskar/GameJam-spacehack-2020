using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightCalculator : MonoBehaviour
{
    [SerializeField] private PlayerRagdoll _playerRagdoll;

    public void CalculateWeight()
    {
        float weight = 0;
        int count = 0;
        
        foreach (Transform child in transform)
        {
            var item = child.GetComponent<InventoryItem>();
            if(item == null) continue;

            count += item.Effectiveness;
            weight += item.Weight * item.Effectiveness;
        }

        _playerRagdoll.Weight =  1 - (weight / count);
    }
}
