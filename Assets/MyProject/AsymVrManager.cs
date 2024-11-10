using UnityEngine;
using UnityEngine.XR;

public class AsymVrManager : MonoBehaviour
{
    void Start()
    {
        XRSettings.gameViewRenderMode = GameViewRenderMode.None; 
    }
}
