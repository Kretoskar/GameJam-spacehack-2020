using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _xClamp = 120;
    [SerializeField] private float _yClamp = 120;

    private WeightCalculator _weightCalculator;
    
    private Vector3 _positionBeforeDrag;
    private Transform previousParent;

    public float Weight;
    public int  Effectiveness;

    private void Start()
    {
        _weightCalculator = transform.parent.GetComponent<WeightCalculator>();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 lastFramePosition = transform.position;

        transform.position = Input.mousePosition;

        float newX = transform.localPosition.x > _xClamp || transform.localPosition.x < -_xClamp
            ? lastFramePosition.x
            : transform.position.x;

        float newY = transform.localPosition.y > _yClamp || transform.localPosition.y < -_yClamp
            ? lastFramePosition.y
            : transform.position.y;
        
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Weight = transform.localPosition.x / _xClamp / 2 + .5f;
        Effectiveness = (int) ((transform.localPosition.y / _yClamp / 2 + 0.5f) * 10) ;
        
        _weightCalculator.CalculateWeight();
    }
}
