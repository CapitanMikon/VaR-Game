using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 offset;

    private void OnEnable()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
