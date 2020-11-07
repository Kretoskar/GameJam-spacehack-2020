using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject _uiPrefab;

    private Transform _itemsParent;

    private void Start()
    {
        _itemsParent = FindObjectOfType<WeightCalculator>().transform;
    }
    
    public void AddToInventory()
    {
        Instantiate(_uiPrefab, _itemsParent);
        Destroy(gameObject);
    }
}
