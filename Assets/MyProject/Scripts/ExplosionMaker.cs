using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExplosionMaker : MonoBehaviour
{
    public float explosionForce = 10f;
    public float explosionRadius;
    private Collider[] colliders = new Collider[10];
    public LayerMask layerMask;
    public Camera cam;
    
    private Vector3 click;
    
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                click = hit.point;
                int numColliders = Physics.OverlapSphereNonAlloc(hit.point, explosionRadius, colliders, layerMask);
                Debug.Log(numColliders);

                if (numColliders > 0)
                {
                    for (int i = 0; i < numColliders; i++)
                    {
                        if (colliders[i].attachedRigidbody)
                        {
                            colliders[i].attachedRigidbody.AddForce(Vector3.forward * explosionForce, ForceMode.Impulse);
                            Debug.Log(colliders[i].gameObject);
                        }
                    }
                }
            }
            else
            {
                click = Vector3.zero;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(click, explosionRadius);
    }
#endif
}
