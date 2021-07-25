using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
	public bool isSceneChanger;
	public int IndexOfSecene;
	public bool IsPageOpenerBool;
	public GameObject gm;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex) {
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1) {
				animator.SetBool ("pressed", true);
				StartMainMenuy();
				IsPageOpener();
			}
			else if (animator.GetBool ("pressed")) {
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}
		else {
			animator.SetBool ("selected", false);
		}
    }
	public void StartMainMenuy()
	{
		if (isSceneChanger == true) {
			SceneManager.LoadScene(IndexOfSecene);
		} 

	}
	public void IsPageOpener()
	{
		if (IsPageOpenerBool == true) { 
			gm.SetActive(true);
		}
	}
}
