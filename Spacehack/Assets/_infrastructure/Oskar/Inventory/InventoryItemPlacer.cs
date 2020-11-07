using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItemPlacer : MonoBehaviour
{
    [SerializeField] private float _xClamp = 120;
    [SerializeField] private float _yClamp = 120;

    private List<ItemRoom> _itemRooms;

    private void Awake()
    {
        _itemRooms = new List<ItemRoom>();
    }
    
    public void PlaceItem(GameObject item)
    {
        Vector2 randomPosition = new Vector2(Random.Range(-_xClamp, _xClamp), Random.Range(-_yClamp, _yClamp));

        for (int i = 0; i < 50; i++)
        {
            if(_itemRooms.Any(itemRoom => itemRoom.IsRoomAvailable(randomPosition) == false))
                randomPosition = new Vector2(Random.Range(-_xClamp, _xClamp), Random.Range(-_yClamp, _yClamp));
            else
                break;
        }

        item.transform.localPosition = randomPosition;
        _itemRooms.Add(new ItemRoom(randomPosition));

        GetComponent<WeightCalculator>().CalculateWeight();
    }
}

public class ItemRoom
{
    public float _placeX = 40;
    public float _placeY = 40;
    public Vector2 position;

    public ItemRoom(Vector2 position)
    {
        this.position = position;
    }

    public bool IsRoomAvailable(Vector2 desiredPosition)
    {
        if (desiredPosition.x > position.x - _placeX && desiredPosition.x < position.x + _placeX)
            return false;
        if (desiredPosition.y > position.y - _placeY && desiredPosition.y < position.y + _placeY)
            return false;
        return true;
    }
}
