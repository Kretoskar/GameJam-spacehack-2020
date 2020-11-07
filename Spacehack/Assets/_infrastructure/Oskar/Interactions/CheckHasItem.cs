using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHasItem : MonoBehaviour
{
    public bool UseItemIfHave(int itemID)
    {
        foreach (Transform child in transform)
        {
            var comp = child.GetComponent<InventoryItem>();
            
            if(comp == null) continue;

            if (comp.Id == itemID)
            {
                Destroy(comp.gameObject);

                StartCoroutine(DelayedCalculateWeight());
                
                return true;
            }
        }

        return false;
    }

    private IEnumerator DelayedCalculateWeight()
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<WeightCalculator>().CalculateWeight();
    }
}
