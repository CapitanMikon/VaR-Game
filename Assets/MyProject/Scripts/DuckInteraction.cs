using UnityEngine;
using UnityEngine.InputSystem;

public class DuckInteraction : MonoBehaviour
{
    private bool inDuckArea = false;
    private DuckAreaController _duckAreaControllerRef;
    private GameObject pickedDuck = null;
    
    [SerializeField] private Transform duckPickUpTransform;
    [SerializeField] private Transform duckPlayerModel;
    [SerializeField] private InputActionReference interact;

    [SerializeField] private GameObject hintHud;

    private void OnEnable()
    {
        interact.action.performed += OnInteract;
        PlayerHealth.PlayerDied += OnDeath;
    }
    
    private void OnDisable()
    {
        interact.action.performed -= OnInteract;
        PlayerHealth.PlayerDied -= OnDeath;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DuckAreaController>(out var duckAreaController))
        {
            inDuckArea = true;
            _duckAreaControllerRef = duckAreaController;
            Debug.Log("Entered area. Press Interaction key to pick up duck!");
            if (pickedDuck == null)
            {
                ShowHint(true);
            }
        }
        
        if (other.TryGetComponent<DeliverDuckArea>(out var deliverDuckArea))
        {
            if (pickedDuck == null)
            {
                return;
            }
            
            deliverDuckArea.DeliverDuck(pickedDuck);
            
            _duckAreaControllerRef = null;
            pickedDuck = null;
            interact.action.Enable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<DuckAreaController>(out var duckAreaController))
        {
            inDuckArea = false;
            Debug.Log("Left area. Press Interaction key to pick up duck!");
            //_duckAreaControllerRef = null;
            ShowHint(false);
        }
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        Debug.Log("Interact key pressed");
        if (!inDuckArea || _duckAreaControllerRef == null)
        {
            return;
        }
        
        pickedDuck = _duckAreaControllerRef.PickUpDuck(duckPlayerModel.transform, duckPickUpTransform);
        if (pickedDuck == null)
        {
            Debug.LogWarning("Trying to pick up null duck!");
            return;
        }
        
        interact.action.Disable();
        ShowHint(false);
    }

    private void OnDeath()
    {
        if (pickedDuck == null || _duckAreaControllerRef == null)
        {
            return;
        }
        
        _duckAreaControllerRef.ReturnDuck(pickedDuck);
        
        _duckAreaControllerRef = null;
        pickedDuck = null;
        interact.action.Enable();
    }

    private void ShowHint(bool show)
    {
        hintHud.SetActive(show);
    }
}
