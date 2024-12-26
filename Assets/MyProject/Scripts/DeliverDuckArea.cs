using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliverDuckArea : MonoBehaviour
{
    [SerializeField] private List<Transform> positions = new();

    private int currentPos = 0;

    public static event Action DuckDelivered;
    
    public void DeliverDuck(GameObject duck)
    {
        duck.transform.SetParent(transform);
        duck.transform.position = positions[currentPos++].position;
        Debug.Log("Duck delivered!");
        DuckDelivered?.Invoke();
    }
}
