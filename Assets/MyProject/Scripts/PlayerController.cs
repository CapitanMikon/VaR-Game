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
    private Vector3 slopeNormal;

    private void Start()
    {
        //rb.maxLinearVelocity = maxSpeed;
    }

    private void OnEnable()
    {
        movement.action.performed += OnMoveStart;
        movement.action.canceled += OnMoveEnd;
        jump.action.performed += OnJump;
    }
    
    private void OnDisable()
    {
        movement.action.performed -= OnMoveStart;
        movement.action.canceled -= OnMoveEnd;
        jump.action.performed -= OnJump;
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
    }
    // ignore slopes just jump :)
    private bool OnSlope()
    {
        if (Physics.Raycast(legs.position, Vector3.down, out var slopeHit, 0.25f))
        {
            if (slopeNormal == Vector3.zero)
            {
                Debug.Log("reset y velocity");
                var current = rb.velocity;
                current.y = 0;
                rb.velocity = current;
            }
            slopeNormal = slopeHit.normal;
            return slopeHit.normal != Vector3.up;
        }
        else
        {
            slopeNormal = Vector3.zero;
        }

        return false;
    }

    private void HandleMovement()
    {
        input.Normalize();
        var inputDir = new Vector3(input.x, 0f, input.y);
        
        //float left = Keyboard.current.leftArrowKey.ReadValue();
        //float right = Keyboard.current.rightArrowKey.ReadValue();
        //var inputDir = Vector3.right * right + Vector3.left * left;
        //input = inputDir;
        
        var dir = OnSlope() ? Vector3.ProjectOnPlane(inputDir, slopeNormal).normalized : inputDir;
        //Debug.DrawRay(transform.position, dir, Color.magenta);
        var targetDir = dir * (moveSpeed * Time.fixedDeltaTime);
        Debug.DrawRay(transform.position, targetDir * 20f, Color.blue);
        
        rb.MovePosition(transform.position + targetDir);

        rb.useGravity = !OnSlope();
        //Debug.Log(OnSlope());
    }

    private void CheckGrounded()
    {
        if (Physics.Raycast(legs.transform.position, Vector3.down, out var hit, 0.2f))
        {
            //Debug.Log(hit.collider.gameObject);
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        // Debug.Log(hit.collider.gameObject);
        //grounded = Physics.Raycast(legs.transform.position, Vector3.down, out var hit, 0.2f);
    }

    private void OnMoveStart(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>();
    }

    private void OnMoveEnd(InputAction.CallbackContext ctx)
    {
        input = Vector2.zero;
    }
    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (grounded)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(legs.transform.position + new Vector3(input.x/4, 0f, input.y/4), Vector3.down * 0.2f);
    }
}
