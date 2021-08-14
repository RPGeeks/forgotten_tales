using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nametag : NetworkBehaviour
{
    public TextMeshProUGUI playerNameText;
    public GameObject nametag;

    private Material playerMaterialClone;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName;
    }

    public override void OnStartLocalPlayer()
    {
        string name = PlayerPrefs.GetString("NameSelected", "player");
        CmdSetupPlayer(name);
        nametag.SetActive(false);
    }

    [Command]
    public void CmdSetupPlayer(string _name)
    {
        playerName = _name;
    }

    void Update()
    {
        nametag.transform.LookAt(Camera.main.transform);
    }
}
