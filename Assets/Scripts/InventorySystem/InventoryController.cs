using RPGeeks.Inventories;
using RPGeeks.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Button exitButton;
    [SerializeField] private ItemDropHandler dropHandler;

    [Header("Inventory parameters")]
    [SerializeField] private Inventory inventoryData;
    private int _inventorySize;

    private GameObject[] slots;

    void Start()
    {
        inventory = inventory != null ? inventory : transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
        slotPrefab = slotPrefab != null ? slotPrefab : Resources.Load<GameObject>("Prefabs/UI/Inventory/Slot");
        exitButton = exitButton != null ? exitButton : transform.Find("ExitButton").GetComponent<Button>();
        dropHandler ??= FindObjectOfType<ItemDropHandler>();

        // TODO load inventory from NetworkClient.localPlayer;
        inventoryData ??= Resources.Load<Inventory>("Prefabs/Data/Inventory");
        _inventorySize = inventoryData.Size;
        slots = new GameObject[_inventorySize];

        slotPrefab.GetComponentInChildren<InventoryItemDragHandler>().DropHandler = dropHandler;
        for (int i = 0; i < _inventorySize; i++)
        {
            slots[i] = Instantiate(slotPrefab, inventory.transform);
        }

        dropHandler.Init(inventory: inventoryData);
        exitButton.onClick.AddListener(QuitInventory);
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
