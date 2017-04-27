using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
	public Transform pauseScreen;
	public GameObject penguinScreen;
	public GameObject HelpScreen;
	// Use this for initialization


	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Pause ();
			enablePauseScreen();
		}
		if (Input.GetKeyDown (KeyCode.H))
		{
			Pause ();
			enableHelpScreen();
		}
	}

	public void Pause()
	{
	
		if (canvas.gameObject.activeInHierarchy == false)
		{
			canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
		} 
		else 
		{
			canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
	
		}
	}

	public void enablePenguinScreen() {
		pauseScreen.gameObject.SetActive(false);
		penguinScreen.SetActive(true);
		HelpScreen.gameObject.SetActive(false);
	}

	public void enablePauseScreen() {
		pauseScreen.gameObject.SetActive(true);
		penguinScreen.SetActive(false);
		HelpScreen.gameObject.SetActive(false);
	}

	public void enableHelpScreen() {
		HelpScreen.gameObject.SetActive(true);
		pauseScreen.gameObject.SetActive(false);
		penguinScreen.SetActive(false);
	}
}