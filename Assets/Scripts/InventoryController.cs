using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class InventoryController :  NetworkBehaviour
{
    //private bool inventoryEnabled;

    [Header("References")]
    [SerializeField] private GameObject inventory; 
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Button exitButton;


    [Header("Inventory parameters")]
    [SerializeField] private int inventorySize;
    private SyncList<GameObject> slots = new SyncList<GameObject>();
   
    
    private void Awake()
    {
      //  slots = new SyncList<GameObject>();
    }

    void Start()
    {
        inventory = inventory != null ? inventory : transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
        slotPrefab = slotPrefab != null ? slotPrefab : Resources.Load<GameObject>("Prefabs/UI/Inventory/Slot");
        exitButton = exitButton != null ? exitButton : transform.Find("ExitButton").GetComponent<Button>();

        for (int i = 0; i < inventorySize; i++)
        {
            slots.Add(Instantiate(slotPrefab, inventory.transform));
        }

        exitButton.onClick.AddListener(QuitInventory);
    }

    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.I))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void QuitInventory()
    {
        gameObject.SetActive(false);
        Debug.Log("quit");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("QuitInventory"))
        {
            Debug.Log("You used the button");
            QuitInventory();
        }
    }
}
