using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> health = new List<GameObject>();
    private int currentHealthStatus = 0;
    private void OnEnable()
    {
        currentHealthStatus = health.Count-1;
        PlayerHealth.PlayerDied += PlayerHealthOnPlayerDied;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDied -= PlayerHealthOnPlayerDied;
    }

    private void PlayerHealthOnPlayerDied()
    {
        if (currentHealthStatus < 0)
        {
            return;
        }
        
        health[currentHealthStatus--].SetActive(false);
    }
}
