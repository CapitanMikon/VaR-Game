using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int ducksToDeliver = 4;

    private int deliveredDucks = 0;
    private void OnEnable()
    {
        DeliverDuckArea.DuckDelivered += DeliverDuckAreaOnDuckDelivered;
        PlayerHealth.PlayerNoLives += VRPlayerWon;
    }

    private void OnDisable()
    {
        DeliverDuckArea.DuckDelivered -= DeliverDuckAreaOnDuckDelivered;
        PlayerHealth.PlayerNoLives -= VRPlayerWon;

    }

    private void DeliverDuckAreaOnDuckDelivered()
    {
        deliveredDucks++;

        if (ducksToDeliver == deliveredDucks)
        {
            PcPlayerWon();
        }
    }

    private void PcPlayerWon()
    {
        Debug.Log("PC player WON!");
    }
    
    private void VRPlayerWon()
    {
        Debug.Log("VR player WON!");
    }
}
