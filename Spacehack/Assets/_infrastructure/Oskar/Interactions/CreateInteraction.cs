using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInteraction : Interaction
{
    [SerializeField] private GameObject _particle;
    [SerializeField] private GameObject _createObject;
    [SerializeField] private float _sizeUpTime;
    
    protected override void Interact()
    {
        base.Interact();
        
        _createObject.SetActive(true);
        
        if(_particle != null)
            _particle.SetActive(true);
        
        StartCoroutine(SizeUp());
    }

    private IEnumerator SizeUp()
    {
        float t = 0;

        Vector3 startScale = _createObject.transform.localScale;
        
        while (t < 1)
        {
            t += Time.deltaTime / _sizeUpTime;
            _createObject.transform.localScale = startScale * Mathf.Lerp(0, 1, t);
            yield return null;
        }
    }
}
