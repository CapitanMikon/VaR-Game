using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public int maxHealth = 3;
    
    private int currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        transform.position = spawnPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DeathArea>(out var deathArea))
        {
            TryRespawn();
        }
    }

    private void TryRespawn()
    {
        currentHealth--;
        transform.position = spawnPoint.position;
        
        Debug.LogWarning($"Player Died remaining hearts = {currentHealth}!");
        if (currentHealth <= 0)
        {
            Debug.LogWarning("GameOver for pc player!");
            PcPlayerOver();
        }

    }

    private void PcPlayerOver()
    {
        gameObject.SetActive(false);
    }
}
