using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _yOffset = 1;
    
     [SerializeField]
    private Transform target;
    [SerializeField]
    private CameraManagerSO cameraManagerSO;

    //cacged from SO
    private float mouseSpeed = 5;
    private float controllerSpeed = 5;
    private float followSpeed = 10;
    private float turnSmoothing = 0.1f;
    private float minAngle = -35;
    private float maxAngle = 35;

    private float smoothX;
    private float smoothY;
    private float smoothXVel;
    private float smoothYVel;
    private float lookAngle;
    private float tiltAngle;

    private Transform pivot;
    private Transform cam;

    public static CameraManager singleton;

    private void Awake()
    {
        singleton = this;
        GetValuesFromSO();
    }

    private void GetValuesFromSO()
    {
        mouseSpeed = cameraManagerSO.MouseSpeed;
        controllerSpeed = cameraManagerSO.ControllerSpeed;
        followSpeed = cameraManagerSO.FollowSpeed;
        turnSmoothing = cameraManagerSO.TurnSmoothing;
        minAngle = cameraManagerSO.MinAngle;
        maxAngle = cameraManagerSO.MaxAngle;
    }

    private void Start()
    {
        cam = Camera.main.transform;
        pivot = cam.parent;
    }

    private void Update()
    {
        //Get input
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        float targetSpeed = mouseSpeed;
        

        FollowTarget();
        RotateCamera(v, h, targetSpeed);
    }

    private void RotateCamera(float v, float h, float targetSpeed)
    {
        if(turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXVel, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYVel, turnSmoothing);
        } else
        {
            smoothX = h;
            smoothY = v;
        }

        lookAngle += smoothX * targetSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

        //change to += if not opposite
        tiltAngle -= smoothY * targetSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        transform.position = targetPosition;
    }
}
