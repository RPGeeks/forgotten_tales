using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioScript: MonoBehaviour
{
	// Variable to check if the sound is already played
	private static AudioScript audio_source;

	// Check if we skipped the menu scene, so we don`t destroy the object more times
	bool is_skipped = false;

	private void Start()
	{
		audio_source = GetComponent<AudioScript>();
	}

	private void Update()
	{
		if (is_skipped == false)
		{
			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "CharacterSelection")
			{
				Destroy(gameObject);
				is_skipped = true;
			}
		}
	}
	// On change scene
	void Awake()
	{
		if (audio_source != null)
		{
			Destroy(gameObject);
		} 
		else
		{ 
			DontDestroyOnLoad(transform.gameObject);
		}
	}
}