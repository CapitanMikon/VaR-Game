using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public int maxHealth = 3;
    public static event Action PlayerDied;
    public static event Action PlayerNoLives;
    
    private int currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
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
        PlayerDied?.Invoke();
        
        Debug.LogWarning($"Player Died remaining hearts = {currentHealth}!");
        if (currentHealth <= 0)
        {
            Debug.LogWarning("GameOver for pc player!");
            PcPlayerOver();
            PlayerNoLives?.Invoke();
        }
        /*else
        {
            PlayerDied?.Invoke();
        }*/
        
        transform.position = spawnPoint.position;
        

    }

    private void PcPlayerOver()
    {
        gameObject.SetActive(false);
    }
}
