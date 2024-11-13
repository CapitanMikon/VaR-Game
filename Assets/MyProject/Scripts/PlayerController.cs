using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform legs;

    [SerializeField] private bool grounded = true;

    private void Start()
    {
        //rb.maxLinearVelocity = maxSpeed;
    }

    private void OnEnable()
    {
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
        var dir = movement.action.ReadValue<Vector2>();
        var newPos = new Vector3(dir.x, 0f, dir.y) * speed * Time.fixedDeltaTime;

        if (dir != Vector2.zero && grounded)
        {
            Vector3 velocity = newPos * speed * Time.fixedDeltaTime;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }

        if (Physics.Raycast(legs.transform.position, Vector3.down, out var hit, 0.2f))
        {
            Debug.Log(hit.collider.gameObject);
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void OnMoveStart(InputAction.CallbackContext ctx)
    {
        
    }

    private void OnMoveEnd(InputAction.CallbackContext ctx)
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(legs.transform.position, Vector3.down * 0.2f);
    }
}
