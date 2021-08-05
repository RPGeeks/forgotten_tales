using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;

    public TMP_InputField inputField;
    public TMP_Text chatText;

    //bool currentlyFading = false;

    //public float timeToHideChat = 5f;

    public event Action<string> OnInputMessage;

    static UnityEngine.EventSystems.EventSystem eventSystemComponent;

    Queue<string> m_chatMessages = new Queue<string>();
    public int maxNumChatMessages = 10;

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
        if (m_chatMessages.Count >= this.maxNumChatMessages)
        {
            m_chatMessages.Dequeue();
        }

        m_chatMessages.Enqueue(message);
        
        this.UpdateUI();
    }

    void UpdateUI()
    {
        string[] chatMessages = m_chatMessages.ToArray();

        string textStr = "";

        for (int i = 0; i < chatMessages.Length; i++)
        {
            textStr += chatMessages[i];
        }

        chatText.text = textStr;

        //UnFade();
        //SetFadeTimeout();
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

    /*IEnumerator FadeChat(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                if (!currentlyFading)
                    yield break;

                // set color with i as alpha
                chatText.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                if (!currentlyFading)
                    yield break;

                // set color with i as alpha
                chatText.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    void StartFade()
    {
        if (currentlyFading == false)
        {
            currentlyFading = true;
            StartCoroutine(FadeChat(true));
        }
    }

    public void UnFade()
    {
        currentlyFading = false;
        chatText.color = new Color(1, 1, 1, 1);
    }

    void SetFadeTimeout()
    {
        if (!this.IsInvoking(nameof(StartFade)))
            this.Invoke(nameof(StartFade), this.timeToHideChat);
    }*/
}
