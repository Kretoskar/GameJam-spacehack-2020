using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private float _gravity = 8;
    
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    


    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = Vector3.zero;
        
        if (_characterController.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        }

        moveDir.y = -_gravity;
        moveDir *= Time.deltaTime * _speed;
        _characterController.Move(moveDir);
    }
}
