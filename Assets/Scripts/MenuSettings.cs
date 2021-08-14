using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitSettings()
    {
        SceneManager.LoadScene("Menu");
    }
}
