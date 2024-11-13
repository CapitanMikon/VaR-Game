using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference jump;
    [FormerlySerializedAs("speed")] [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform legs;

    [SerializeField] private bool grounded = true;

    [SerializeField] private Vector2 input;

    private void Start()
    {
        //rb.maxLinearVelocity = maxSpeed;
    }

    private void OnEnable()
    {
        movement.action.performed += OnMoveStart;
        movement.action.canceled += OnMoveStart;
    }
    
    private void OnDisable()
    {
        movement.action.performed -= OnMoveStart;
        movement.action.canceled -= OnMoveStart;
    }

    private void Update()
    {
        if (grounded && jump.action.WasPerformedThisFrame())
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("click");
            rb.AddForce(Vector3.forward * 5f, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
    }

    private void HandleMovement()
    {
        /*if (input == Vector2.zero)
        {
            return;
        }*/
        input.Normalize();
        
        var currentVelocity = rb.velocity;
        
        var targetVelocity = new Vector3(input.x,0f, input.y) * moveSpeed;
        // targetVelocity.x += currentVelocity.x;
        // targetVelocity.z += currentVelocity.z;
        // Vector3.ClampMagnitude(targetVelocity, maxSpeed);
        // targetVelocity.y = currentVelocity.y;

        var changeVelocity = targetVelocity - currentVelocity;
        changeVelocity = new Vector3(changeVelocity.x, 0f, changeVelocity.z);

        Vector3.ClampMagnitude(changeVelocity, maxSpeed);
        
        rb.AddForce(changeVelocity, ForceMode.VelocityChange);

        //rb.velocity = targetVelocity;

        //currentVelocity = rb.velocity;
        //Vector3.ClampMagnitude(currentVelocity, maxSpeed);
        //currentVelocity.y = rb.velocity.y;
    }

    private void CheckGrounded()
    {
        // if (Physics.Raycast(legs.transform.position, Vector3.down, out var hit, 0.2f))
        // {
        //     //Debug.Log(hit.collider.gameObject);
        //     grounded = true;
        // }
        // else
        // {
        //     grounded = false;
        // }
        // Debug.Log(hit.collider.gameObject);
        grounded = Physics.Raycast(legs.transform.position, Vector3.down, out var hit, 0.2f);
    }

    private void OnMoveStart(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>();
    }

    private void OnMoveEnd(InputAction.CallbackContext ctx)
    {
        input = Vector2.zero;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(legs.transform.position, Vector3.down * 0.2f);
    // }
}
