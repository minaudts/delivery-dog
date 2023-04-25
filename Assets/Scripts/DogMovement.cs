using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DogMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;
    private InputActionMap playerActionMap;
    private InputAction movement;
    private NavMeshAgent agent;
    [SerializeField]
    [Range(0,0.99f)]
    private float smoothing = 0.25f;
    [SerializeField]
    private float targetLerpSpeed = 1;
    private Vector3 targetDirection;
    private float lerpTime = 0;
    private Vector3 lastDirection;
    private Vector3 movementVector;
    //public float moveSpeed = 5f;
    // For diagonal movement
    //private Rigidbody _rigidbody;
    //private Vector2 movementInput;
    private Animator animator;
    // To wake up for collision detection
    private Rigidbody _rigidbody;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.sleepThreshold = 0;
        playerActionMap = inputActions.FindActionMap("Player");
        movement = playerActionMap.FindAction("Move");
        movement.started += HandleMovementAction;
        movement.canceled += HandleMovementAction;
        movement.performed += HandleMovementAction;
        movement.Enable();
        playerActionMap.Enable();
        inputActions.Enable();
        //_rigidbody = GetComponent<Rigidbody>();
    }

    private void HandleMovementAction(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        // weird because of rotation of world and camera
        movementVector = new Vector3(input.y, 0, -input.x);

    }

    void Update()
    {
        // Movement
        //_rigidbody.velocity = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed;
        movementVector.Normalize();
        if(movementVector != lastDirection)
        {
            lerpTime = 0;
        }
        lastDirection = movementVector;
        targetDirection = Vector3.Lerp(targetDirection, movementVector, Mathf.Clamp01(lerpTime * targetLerpSpeed * (1 - smoothing)));
        agent.Move(targetDirection * agent.speed * Time.deltaTime);
        Vector3 lookDirection = movementVector;
        if(lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Mathf.Clamp01(lerpTime * targetLerpSpeed * (1 - smoothing)));
        }
        lerpTime += Time.deltaTime;
        animator.SetBool("isWalking", IsMoving());
    }

    /*private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }*/
    public bool IsMoving()
    {
        return !movementVector.Equals(Vector3.zero);
        //return !_rigidbody.velocity.Equals(Vector3.zero);
    }
}
