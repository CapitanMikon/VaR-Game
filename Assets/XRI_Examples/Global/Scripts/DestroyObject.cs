using System;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Destroys GameObject after a few seconds.
    /// </summary>
    public class DestroyObject : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Time before destroying in seconds.")]
        float m_Lifetime = 5f;

        public Rigidbody rb;

        void Start()
        {
            Destroy(gameObject, m_Lifetime);
        }

        private void OnCollisionEnter(Collision other)
        {
            rb.useGravity = true;
        }
    }
}
