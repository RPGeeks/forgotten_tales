using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField input;
    void Start()
    {
        if (PlayerPrefs.GetString("name") != "")
        {
            input.text = PlayerPrefs.GetString("name");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (input.text != PlayerPrefs.GetString("name"))
        {
            PlayerPrefs.SetString("name", input.text);
        }
    }
}
