using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	private bool disabledOnce;
	public bool DisabledOnce { get => disabledOnce; set => disabledOnce = value; }

	void PlaySound(AudioClip whichSound)
	{
		if (!disabledOnce && whichSound && menuButtonController.AudioSourced)
		{
			menuButtonController.AudioSourced.PlayOneShot(whichSound);
		}
		else
		{
			disabledOnce = false;
		}
	}
}
