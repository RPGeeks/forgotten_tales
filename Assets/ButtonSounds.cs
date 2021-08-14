using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundsScript : MonoBehaviour
{
	public AudioSource clickSoundFX;
	public AudioClip hoverSound;
	public AudioClip clickSound;

	public void HoverSound()
	{
		clickSoundFX.PlayOneShot(hoverSound);
	}

	public void ClickSound()
	{
		clickSoundFX.PlayOneShot(clickSound);
	}
}
