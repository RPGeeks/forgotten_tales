using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO remove this class and use VirtualInput
public class MenuInputManager : MonoBehaviour
{
    private static MenuInputManager _instance;
    public static MenuInputManager Instance { get => _instance; }

    [SerializeField] private KeyCode inventoryKey = KeyCode.I;
    
    public delegate void OnKeyPress();
    public static event OnKeyPress onInventoryEvent;

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

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            onInventoryEvent?.Invoke();
        }
    }
}
