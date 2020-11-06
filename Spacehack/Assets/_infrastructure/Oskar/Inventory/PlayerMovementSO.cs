using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Single instances/Player movement")]
public class PlayerMovementSO : ScriptableObject
{
    [SerializeField]
    [Range(0.1f, 1000)]
    private float moveSpeed = 100;
    
    [SerializeField]
    [Range(0.1f, 1000)]
    private float turnSpeed = 5;

    [SerializeField]
    [Range(0.1f,100)]
    private float rotateSpeed = 10;

    [SerializeField]
    [Range(0.1f, 100)]
    private float gravity = 10;

    [SerializeField]
    [Range(0.1f, 100)]
    private float jumpForce = 3;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float TurnSpeed { get => turnSpeed; set => turnSpeed = value; }
    public float RotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }
    public float Gravity { get => gravity; set => gravity = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
}
