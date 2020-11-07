using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteriesManager : MonoBehaviour
{
    [SerializeField]
    private int _maxWater = 500;

    [SerializeField] private Material _noEmissionMaterial;

    [SerializeField] private List<GameObject> _batteries;
    
    private WaterManager _waterManager;

    private int _currentBatteriesCount = 5;

    private int _droppedAmount;
    
    private void Start()
    {
        _waterManager = FindObjectOfType<WaterManager>();
        _waterManager.CurrentAmountChanged += UpdateBatteries;
    }

    private void UpdateBatteries(int amount)
    {
        //co ja robie kurwa
        //chuj z tym szybko to napisalem to bedzie
        if (amount < 400)
            _batteries[0].GetComponent<MeshRenderer>().material = _noEmissionMaterial;
            if (amount < 300)
                _batteries[1].GetComponent<MeshRenderer>().material = _noEmissionMaterial;
                if(amount < 200)
                    _batteries[2].GetComponent<MeshRenderer>().material = _noEmissionMaterial;
                    if(amount < 100)
                        _batteries[3].GetComponent<MeshRenderer>().material = _noEmissionMaterial;
                       if(amount < 50)
                            _batteries[4].GetComponent<MeshRenderer>().material = _noEmissionMaterial;
            
    }
}
