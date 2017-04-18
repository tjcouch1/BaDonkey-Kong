using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonNavigation : MonoBehaviour {
	public int index=0;
	public int totallevels=2;
	public float yOffset=1f;
	// Use this for initialization

	// Update is called once per frame

	public void LoadScence (int level)
	{

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if(index<=totallevels-1)
			{
				index++;
				Vector2 position=transform.position;
				position.y-= yOffset;
				transform.position=position;
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(index>0)
			{
				index--;
				Vector2 position=transform.position;
				position.y+= yOffset;
				transform.position=position;
			}
		}
		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(index==0){
				SceneManager.LoadScene(level);
			}
			else 
				SceneManager.LoadScene(level);

		}
	}
}
