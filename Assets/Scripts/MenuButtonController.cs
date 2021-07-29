using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{

	// Use this for initialization
	private int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	private AudioSource audioSource;

	public int Indexed { get => index; set => index = value; }
	public AudioSource AudioSourced { get => audioSource; set => audioSource = value; }

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetAxis("Vertical") != 0)
		{
			if (!keyDown)
			{
				if (Input.GetAxis("Vertical") < 0)
				{
					if (index < maxIndex)
					{
						index++;
					}
					else
					{
						index = 0;
					}
				}
				else if (Input.GetAxis("Vertical") > 0)
				{
					if (index > 0)
					{
						index--;
					}
					else
					{
						index = maxIndex;
					}
				}
				keyDown = true;
			}
		}
		else
		{
			keyDown = false;
		}
	}

}
