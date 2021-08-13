using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private CrosshairController crosshairController;
    [SerializeField] private CharacterController characterController;

    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public static bool IsInventoryOpened { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        inventoryController = inventoryController != null 
            ? inventoryController 
            : FindObjectOfType<InventoryManager>();
        inventoryController.gameObject.SetActive(false);

        cameraController = cameraController != null
            ? cameraController
            : FindObjectOfType<CameraController>();

        crosshairController = crosshairController != null
            ? crosshairController
            : FindObjectOfType<CrosshairController>();

        // TODO wait for localPlayer to be spawned
        //characterController = NetworkClient.localPlayer.GetComponent<CharacterController>();

    }

    public void ToggleInventory()
    {
        bool isInventoryActive = inventoryController.gameObject.activeSelf;
        IsInventoryOpened = !isInventoryActive;

        cameraController.enabled = isInventoryActive;
        crosshairController.enabled = isInventoryActive;
        //characterController.enabled = isInventoryActive;
        
        inventoryController.gameObject.SetActive(!isInventoryActive);
        Cursor.visible = !isInventoryActive;
    }
}
