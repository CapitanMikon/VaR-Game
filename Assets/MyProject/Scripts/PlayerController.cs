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
    [SerializeField] private float groundCheckDistance = 0.05f;
    
    [SerializeField] private Rigidbody rb;
    [FormerlySerializedAs("legs")] [SerializeField] private Transform feetTransform;

    [FormerlySerializedAs("grounded")] [SerializeField] private bool isGrounded = true;

    [SerializeField] private Vector2 input;

    private Vector3 slopeNormal;
    private float turnValue;
    
    [SerializeField] private GameObject playerModel;

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

    void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();

        if (input.magnitude >= 0.1)
        {
            float angle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
            var smooth = Mathf.SmoothDampAngle(playerModel.transform.localEulerAngles.y, angle, ref turnValue, 0.1f);
            playerModel.transform.localRotation = Quaternion.Euler(0, smooth, 0);
            
        }
    }
    
    private bool OnSlope()
    {
        if (Physics.Raycast(feetTransform.position, Vector3.down, out var slopeHit, groundCheckDistance))
        {
            if (slopeNormal == Vector3.zero)
            {
                //Debug.Log("reset y velocity");
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

        var dir = OnSlope() ? Vector3.ProjectOnPlane(inputDir, slopeNormal).normalized : inputDir;
        
        var targetDir = dir * (moveSpeed * Time.fixedDeltaTime);
        Debug.DrawRay(transform.position, targetDir * 20f, Color.blue); // TODO: remove
        
        rb.MovePosition(transform.position + targetDir);

        rb.useGravity = !OnSlope();
    }

    private void CheckGrounded()
    {
        if (Physics.Raycast(feetTransform.transform.position, Vector3.down, out var hit, 0.05f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
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
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos() // TODO: remove
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(feetTransform.transform.position + new Vector3(input.x/4, 0f, input.y/4), Vector3.down * 0.05f);
    }
}
