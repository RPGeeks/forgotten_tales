using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChatSync : NetworkBehaviour
{
    private static event Action<string> OnMessage;

    private string userName = "player";

    // Called when the a client is connected to the server
    public override void OnStartAuthority()
    {
        OnMessage += HandleNewMessage;
        ChatManager.instance.OnInputMessage += Send;

        CmdSendNickname(PlayerPrefs.GetString("NameSelected", "player"));
    }

    // Called when a client has exited the server
    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
        ChatManager.instance.OnInputMessage -= Send;
    }

    // When a new message is added, update the Scroll View's Text to include the new message
    private void HandleNewMessage(string message)
    {
        ChatManager.instance.AddMessage(message);
    }

    // When a client hits the enter button, send the message in the InputField
    [Client]
    public void Send(string message)
    {
        CmdSendMessage(message);
    }

    [Command]
    private void CmdSendNickname(string nickname)
    {
        userName = nickname;
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        // Validate message
        // ({connectionToClient.connectionId}) -- add this for connection ID
        RpcHandleMessage($"[<color=green>{userName}</color>]:  {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }

}