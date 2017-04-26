using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
	public Transform pauseScreen;
	public GameObject penguinScreen;
	// Use this for initialization

	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Pause ();
			enablePauseScreen();
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
	}

	public void enablePauseScreen() {
		pauseScreen.gameObject.SetActive(true);
		penguinScreen.SetActive(false);
	}
}