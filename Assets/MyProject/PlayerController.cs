using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private float speed = 30f;
    
    void Update()
    {
        var dir = movement.action.ReadValue<Vector2>();
        transform.position += new Vector3(dir.x, 0f, dir.y) * speed * Time.deltaTime;
    }
}
