using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadonClick : MonoBehaviour {
	public void LoadScence (int level)
	{
		SceneManager.LoadScene(level);
	}
}