using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Target : MonoBehaviour
{
    [SerializeField] private float max;
    [SerializeField] private float min;
    [SerializeField] private float speed;

    [SerializeField] private int maxHits = 3;
    [SerializeField] private MeshRenderer meshRenderer;

    private Color[] damageColor = new[] { new Color(1, 0, 0),  new Color(1f, 1f, 0f), new Color(1f, 1f, 0.98f)};
    private int currentHits = 0;

    private void OnEnable()
    {
        currentHits = maxHits;
        UpdateColors();
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DestroyObject>(out var obj))
        {
            Debug.Log($"Target {other.gameObject.name} hit!");
            currentHits--;
            UpdateColors();

            if (currentHits <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdateColors()
    {
        switch (currentHits)
        {
            case 0:
                meshRenderer.material.color = damageColor[2];
                break;
            case 1:
                meshRenderer.material.color = damageColor[1];
                break;
            case 3:
                meshRenderer.material.color = damageColor[0];
                break;
        }
    }
}
