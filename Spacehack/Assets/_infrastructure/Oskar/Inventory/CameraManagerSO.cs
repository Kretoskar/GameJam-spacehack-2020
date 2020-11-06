using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Single instances/Camera manager")]
public class CameraManagerSO : ScriptableObject
{
    [SerializeField]
    [Range(0.1f, 100)]
    private float mouseSpeed = 5;
    [SerializeField]
    [Range(0.1f, 100)]
    private float controllerSpeed = 5;
    [SerializeField]
    [Range(0.1f, 100)]
    private float followSpeed = 10;
    [SerializeField]
    [Range(0.1f, 100)]
    private float turnSmoothing = 0.1f;
    [SerializeField]
    [Range(0.1f, 100)]
    private float minAngle = -35;
    [SerializeField]
    [Range(0.1f, 100)]
    private float maxAngle = 35;

    public float MouseSpeed { get => mouseSpeed; set => mouseSpeed = value; }
    public float ControllerSpeed { get => controllerSpeed; set => controllerSpeed = value; }
    public float FollowSpeed { get => followSpeed; set => followSpeed = value; }
    public float TurnSmoothing { get => turnSmoothing; set => turnSmoothing = value; }
    public float MinAngle { get => minAngle; set => minAngle = value; }
    public float MaxAngle { get => maxAngle; set => maxAngle = value; }
}