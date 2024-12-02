using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketEjector : MonoBehaviour
{
    public XRInteractionManager manager;
    public XRSocketInteractor socket;
    public IXRSelectInteractable interactable;

    public Transform detach;

    public void Eject()
    {
        if (interactable != null)
        {
            socket.socketActive = false;
            Debug.Log("Detaching from code");
            manager.SelectExit(socket, interactable);
            interactable.transform.position = detach.position;
            interactable = null;

            StartCoroutine(enableAfter());
            //Destroy(interactable.transform.gameObject);
            //socket.socketActive = true;
        }
    }

    private IEnumerator enableAfter()
    {
        yield return new WaitForFixedUpdate();
        //yield return new WaitForFixedUpdate();
        //yield return new WaitForFixedUpdate();
        
        socket.socketActive = true;
    }
    

    // private void OnEnable()
    // {
    //     socket.registered+= SocketOnregistered;
    //     socket.unregistered += SocketOnunregistered;
    // }
    //
    // private void OnDisable()
    // {
    //     socket.registered-= SocketOnregistered;
    //     socket.unregistered -= SocketOnunregistered;
    // }

    public void SocketOnregistered(SelectEnterEventArgs obj)
    {
        Debug.Log("Socket register");
        interactable = obj.interactableObject;
    }
    
    public void SocketOnunregistered(SelectExitEventArgs obj)
    {
        Debug.Log("Socket unregister");
        //interactable = null;
        //interactable = null;
    }
}
