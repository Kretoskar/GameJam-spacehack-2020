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
        GameObject obj = Instantiate(_uiPrefab, _itemsParent);
        _itemsParent.GetComponent<InventoryItemPlacer>().PlaceItem(obj);
        
        Destroy(gameObject);
    }
}
