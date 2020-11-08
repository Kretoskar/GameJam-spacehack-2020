using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public Transform FishMovePoint;
    public FishMovementManager fmManager;
    public Transform Bajoro;

    private float circleRadius;
    private float speed;
    void Start()
    {
        FishMovePoint.parent = Bajoro;
        circleRadius = fmManager.CircleRadius;
        speed = fmManager.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, FishMovePoint.position, step);

        transform.LookAt(FishMovePoint);
    }
}
