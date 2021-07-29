using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;

    public InputField inputField;
    public Text chatText;

    public event Action<string> OnInputMessage;

    static UnityEngine.EventSystems.EventSystem eventSystemComponent;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        eventSystemComponent = UnityEngine.EventSystems.EventSystem.current;
    }

    public void OnMessageEditEnd()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }
        if (string.IsNullOrWhiteSpace(inputField.text)) { return; }
        OnInputMessage?.Invoke(inputField.text);
        inputField.text = string.Empty;

        eventSystemComponent.SetSelectedGameObject(null);
    }

    public void AddMessage(string message)
    {
        // TODO : potential optimization would be to store all messages in a queue
        // so you will always have a maximum number of in-memory messages
        // ( by popping out the old ones )
        chatText.text += message;
    }

    public void ClearChat()
    {
        chatText.text = string.Empty;
    }


    static bool lastFrameIsOpened = false;

    public static bool IsOpened
    {
        get {
            if (instance != null)
            {
                bool condition = eventSystemComponent.currentSelectedGameObject == instance.inputField.gameObject;
                bool output = condition || lastFrameIsOpened;
                lastFrameIsOpened = condition;
                return output;
            }
            else
            {
                return false;
            }
        }
    }
}
