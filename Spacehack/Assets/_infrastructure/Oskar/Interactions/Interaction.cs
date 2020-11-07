using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private int _itemID;
    [SerializeField] private CheckHasItem _hasItem;
    
    private bool _playerInRange;
    private bool _interacted;
    
    private void Update()
    {
        if(_interacted) return;

        if(_playerInRange)
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_hasItem.UseItemIfHave(_itemID))
                {
                    _interacted = true;
                    Interact();
                }
            }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = false;
    }

    protected virtual void Interact()
    {
        print("Interacting");
    }
}
