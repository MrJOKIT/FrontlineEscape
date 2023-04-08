using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    
    [SerializeField]  private float speed = 5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    private CharacterController _controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool sprinting;
    

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = _controller.isGrounded;
        
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        
        _controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        _controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public void StartSprint()
    {
        sprinting = !sprinting;
        if (isGrounded)
        {
            speed = sprintSpeed;
        }
        
    }

    public void CancelSprint()
    {
        sprinting = !sprinting;
        speed = walkSpeed;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
            speed = walkSpeed;
        }
    }
}
