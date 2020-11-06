using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightCalculator : MonoBehaviour
{
    [SerializeField] private PlayerRagdoll _playerRagdoll;
    [SerializeField] private float _turnSpeed = 1;
    
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
        
        StopAllCoroutines();
        StartCoroutine(ChangeWeightCoroutine(count, weight));
    }

    private IEnumerator ChangeWeightCoroutine(int count, float weight)
    {
        float desiredWeight = 1 - (weight / count);
        float currentWeight = _playerRagdoll.Weight;

        float lerpT = 0;
        
        while (lerpT < 1)
        {
            _playerRagdoll.Weight = Mathf.Lerp(currentWeight, desiredWeight, lerpT);
            lerpT += Time.deltaTime;
            yield return null;
        }
        
        _playerRagdoll.Weight =  1 - (weight / count);
    }
}
