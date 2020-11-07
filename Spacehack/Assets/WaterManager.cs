using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public int Amount;

    private int _currentAmount;
    public int CurrentAmount
    {
        get => _currentAmount;
        set
        {
            _currentAmount = value;
            CurrentAmountChanged?.Invoke(_currentAmount);
        }
    }

    public int PerRow;
    public float Radius;
    public GameObject Prefab;

    [SerializeField] private WaterGenerator waterGenerator = default;
    [SerializeField] GameObject waterCollider = default;

    public Action<int> CurrentAmountChanged;
    void Start()
    {
        CurrentAmount = Amount;
    }
}
