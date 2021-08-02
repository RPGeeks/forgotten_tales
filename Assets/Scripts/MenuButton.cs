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
	public int indexOfSecene;
	public bool isPageOpenerBool;
	public GameObject gm;

	// Update is called once per frame
	void Update()
	{
		if (menuButtonController.Indexed == thisIndex)
		{
			animator.SetBool("selected", true);
			if (Input.GetAxis("Submit") == 1)
			{
				animator.SetBool("pressed", true);
				StartMainMenuy();
				PageOpener();
			}
			else if (animator.GetBool("pressed"))
			{
				animator.SetBool("pressed", false);
				animatorFunctions.DisabledOnce = true;
			}
		}
		else
		{
			animator.SetBool("selected", false);
		}
	}
	public void StartMainMenuy()
	{
		if (isSceneChanger == true)
		{
			SceneManager.LoadScene(indexOfSecene);
		}
	}
	public void PageOpener()
	{
		if (isPageOpenerBool == true)
		{
			gm.SetActive(true);
		}
	}
}
