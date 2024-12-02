using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeveloperControlls : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            Debug.Log("Reloading Scene!");
            SceneManager.LoadScene(0);
        }
    }
}
