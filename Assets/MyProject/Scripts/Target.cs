using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float max;
    [SerializeField] private float min;
    [SerializeField] private float speed;
    
    void Update()
    {
        transform.position += Vector3.right * (speed * Time.deltaTime);

        if (transform.position.x > max)
        {
            transform.position = new Vector3(min, transform.position.y, transform.position.z);
        }else if(transform.position.x < min)
        {
            transform.position = new Vector3(max, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Target {name} hit!");
    }
}
