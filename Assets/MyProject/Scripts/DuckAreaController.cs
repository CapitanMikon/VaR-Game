using System.Collections.Generic;
using UnityEngine;

public class DuckAreaController : MonoBehaviour
{
    [SerializeField] private List<GameObject> ducks = new();

    private List<Vector3> startingTransforms = new();

    //private int currentPickedDuck = -1;
    private int nextDuckIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < ducks.Count; i++)
        {
            startingTransforms.Add(ducks[i].transform.position);
        }

        nextDuckIndex = ducks.Count - 1;
    }

    public GameObject PickUpDuck(Transform parent, Transform carryTransform)
    {
        if (nextDuckIndex < 0)
        {
            return null;
        }

        var duck = ducks[nextDuckIndex--];
        duck.transform.SetParent(parent);
        duck.transform.position = carryTransform.position;
        return duck;
    }

    public void ReturnDuck(GameObject duck)
    {
        duck.transform.SetParent(null);
        nextDuckIndex++;
        duck.transform.SetParent(transform);
        duck.transform.position = startingTransforms[nextDuckIndex];
        duck.transform.rotation = Quaternion.LookRotation(Vector3.left);
    }
}
