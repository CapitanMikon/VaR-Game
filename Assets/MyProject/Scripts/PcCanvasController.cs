using System;
using TMPro;
using UnityEngine;

public class PcCanvasController : MonoBehaviour
{
    private const String pcPlayerWinText = "You Won!"; 
    private const String pcPlayerLostText = "You Lost!";

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject textGO;

    private void Awake()
    {
        textGO.SetActive(false);
        text.text = "";
    }

    private void OnEnable()
    {
        GameController.GameFinished += GameControllerOnGameFinished;
    }

    private void OnDisable()
    {
        GameController.GameFinished -= GameControllerOnGameFinished;
    }
    
    private void GameControllerOnGameFinished(int playerId)
    {
        textGO.SetActive(true);
        text.text = playerId == 0 ? pcPlayerWinText : pcPlayerLostText;
        

    }
}
