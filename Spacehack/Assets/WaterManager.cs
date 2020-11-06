using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public int Amount;
    public int CurrentAmount;
    public int PerRow;
    public float Radius;
    public GameObject Prefab;

    [SerializeField] private WaterGenerator waterGenerator = default;
    [SerializeField] GameObject waterCollider = default;
    void Start()
    {
        CurrentAmount = Amount;
    }
}
