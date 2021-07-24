using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //private bool inventoryEnabled;

    [Header("References")]
    [SerializeField] private GameObject inventory; 
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Button exitButton;


    [Header("Inventory parameters")]
    [SerializeField] private int inventorySize;

    private GameObject[] slots;

    private void Awake()
    {
        slots = new GameObject[inventorySize];
    }

    void Start()
    {
        inventory = inventory != null ? inventory : transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
        slotPrefab = slotPrefab != null ? slotPrefab : Resources.Load<GameObject>("Prefabs/UI/Inventory/Slot");
        exitButton = exitButton != null ? exitButton : transform.Find("ExitButton").GetComponent<Button>();

        for (int i = 0; i < inventorySize; i++)
        {
            slots[i] = Instantiate(slotPrefab, inventory.transform);
        }

        exitButton.onClick.AddListener(QuitInventory);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameObject.SetActive(!inventory.activeSelf);
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
