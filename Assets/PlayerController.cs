using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 playerInput;    
    [SerializeField] CharacterController controller;   
    [SerializeField] private float playerSpeed = 3f;  
    [SerializeField] private float runSpeed = 6f;

    [SerializeField] private float playerRotation = 40f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;

    private bool isRunning = false;

  
    private Animator animator;
    private bool isWalking = false;
    private bool isRunningAnimation = false;

    private void OnMove(InputValue value)
    {
        
        playerInput = value.Get<Vector2>();

        
        isWalking = playerInput.magnitude > 0;
        isRunningAnimation = isRunning && isWalking;
        animator.SetBool("Walking", isWalking);
        animator.SetBool("Running", isRunningAnimation);
    }

    private void OnRun(InputValue value)
    {
        isRunning = value.isPressed;
    }

    private void PlayerMovement()
    {

        float speed = isRunning ? runSpeed : playerSpeed;

        controller.Move(transform.forward * playerInput.y * speed * Time.deltaTime);
        transform.Rotate(transform.up, playerRotation * playerInput.x * Time.deltaTime);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        
    }
    private void OnShoot(InputValue value)
    {
        animator.SetTrigger("Shooting");       
        
    }
}