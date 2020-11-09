using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointSpawner : MonoBehaviour
{
    public GameObject[] fishMovePoints;
    public FishMovementManager fmManager;

    private float timeToRearrange = 0f;
    private float secondsToRearrangeMovepoints;

    private void Start()
    {
        fishMovePoints = GameObject.FindGameObjectsWithTag("FishMovePoint");
        secondsToRearrangeMovepoints = fmManager.SecondsToRearrangeMovepoints;
        StartCoroutine(MovePointsCoroutine());
    }

    private IEnumerator MovePointsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsToRearrangeMovepoints);
            RearrangeMovePoints();
        }
    }
    
    void RearrangeMovePoints()
    {
        foreach (var movePoint in fishMovePoints)
        {
            var randomPosition = Random.insideUnitCircle * fmManager.CircleRadius;
            var newPosition = new Vector3(randomPosition.x, 0f, randomPosition.y) + transform.position;
            movePoint.transform.position = newPosition;
        }
    }
}
