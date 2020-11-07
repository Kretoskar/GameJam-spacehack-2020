using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInteraction : Interaction
{
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _shrinkTime = 3;
    
    protected override void Interact()
    {
        base.Interact();
        
        _particle.SetActive(true);

        StartCoroutine(ShrinkCorouutine());
    }

    private IEnumerator ShrinkCorouutine()
    {
        float t = 0;

        Vector3 startScale = transform.localScale;
        
        while (t < 1)
        {
            t += Time.deltaTime / _shrinkTime;
            transform.localScale = startScale * Mathf.Lerp(1, 0, t);
            yield return null;
        }
    }
}
