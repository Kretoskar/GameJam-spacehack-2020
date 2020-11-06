using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    [SerializeField] private List<Transform> _spineComponents;
     private List<float> _initialXPositions;
    
    [SerializeField] private float _rotationMultiplier = 1.1f;
    [SerializeField] private float _firstSpinePositionMultiplier = .2f;
    [SerializeField] private float _positionMultiplier = 1.1f;
    [SerializeField] private float _maxRotation = 20;
    
    [SerializeField, Range(0,1)]  private float _weight = .5f;

    private void Start()
    {
        _initialXPositions = new List<float>();
        foreach (var spineComponent in _spineComponents)
        {
            _initialXPositions.Add(spineComponent.position.x);
        }
    }
    
    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            CalculateCurrentZRotation());

        float currentRotationMultiplier = _rotationMultiplier;
        float currentPositionMultiplier = _firstSpinePositionMultiplier;

        int iterator = 0;
        
        foreach (var spine in _spineComponents)
        {
            spine.eulerAngles = new Vector3(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                CalculateCurrentZRotation() * currentRotationMultiplier);
            
            
            spine.transform.position = new Vector3(_initialXPositions[iterator] + currentPositionMultiplier * CalculateCurrentXPosition(),
            spine.transform.position.y, 
            spine.transform.position.z);
            
            currentPositionMultiplier *= 1 + _positionMultiplier;
            currentRotationMultiplier *= _rotationMultiplier;

            iterator++;
        }
    }

    private float CalculateCurrentXPosition()
    {
        return Mathf.Lerp(1, -1, _weight);
    }
    
    private float CalculateCurrentZRotation()
    {
        return Mathf.Lerp(-_maxRotation, _maxRotation, _weight);
    }
}
