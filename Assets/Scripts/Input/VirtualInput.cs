using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualInput : MonoBehaviour
{

    public static bool IsGameInput
    {
        get {
            if (IsChatOpened) { return false; }

            // .. additional conditions ( inventory, menus, etc. )

            return true;
        }
    }

    public static bool IsChatOpened
    {
        get
        {
            return ChatManager.IsOpened;
        }
    }

    void Update()
    {
        if (IsGameInput)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ChatManager.instance.inputField.Select();
        }
    }
}