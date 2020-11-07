using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FootstepsAudio _audio;
    
    [SerializeField]
    private PlayerMovementSO so;

    //cached from so
    private float moveSpeed = 100;
    private float turnSpeed = 5;
    private float rotateSpeed = 10;

    private float horizontal;
    private float vertical;
    private bool canMove;

    private CameraManager cameraManager;
    private CharacterController characterController;
    
    [SerializeField]
    private Animator torsoAnimator;

    [SerializeField] private ParticleSystem _particleSystem;
    
    private Vector3 moveDir;
    private float moveAmount;
    private float jumpForce;
    private float gravity;

    public bool CanMove { get => canMove; set => canMove = value; }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
      //  torsoAnimator = GetComponentInChildren<Animator>();
        GetValuesFromSO();
        canMove = true;
    }

    private void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void GetValuesFromSO()
    {
        gravity = so.Gravity;
        jumpForce = so.JumpForce;
        moveSpeed = so.MoveSpeed;
        turnSpeed = so.TurnSpeed;
        rotateSpeed = so.RotateSpeed;
    }

    private void Update()
    {
        if (!CanMove)
        {
            UpdateAnimator(0);
            return;
        }

        //Get input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //Update animator
        UpdateAnimator(moveAmount);

        //Rotate
        //transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
        Move(vertical);
        Rotate();
    }

    private void Rotate()
    {
        Vector3 targetDirection = moveDir;
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion tr = Quaternion.LookRotation(targetDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * moveAmount * rotateSpeed);
        transform.rotation = targetRotation;
    }

    private void Move(float vertical)
    {
        if (characterController.isGrounded)
        {
            torsoAnimator.SetBool("IsGrounded", true);
            Vector3 v = vertical * cameraManager.transform.forward;
            Vector3 h = horizontal * cameraManager.transform.right;
            moveDir = (v + h).normalized;

            moveAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            moveAmount = Mathf.Clamp01(moveAmount);

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpForce;
                _particleSystem.Play();
            }
        }
        else
        {
            torsoAnimator.SetBool("IsGrounded", false);
        }
        moveDir.y -= gravity * Time.deltaTime;

        if(moveDir.magnitude > 1 && characterController.isGrounded)
            _audio.StartPlaying();
        else
            _audio.StopPlaying();
        
        UpdateAnimator(moveDir.magnitude);
        characterController.Move(moveDir * moveSpeed  * Time.deltaTime);
    }

    private void UpdateAnimator(float moveSpeed)
    {
        torsoAnimator.SetFloat("Speed", moveSpeed);
    }
}