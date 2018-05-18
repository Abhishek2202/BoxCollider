using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

	public GUISkin skin;
	void OnGUI()
	{
		GUI.skin = skin;
		GUI.Label (new Rect (10, 10, 400, 75), "Go Home");

		if (PlayerPrefs.GetInt ("Level Completed") > 0)
		{
			if (GUI.Button (new Rect (10, 100, 100, 45), "Continue"))
			{
				SceneManager.LoadScene (PlayerPrefs.GetInt ("Level Completed"));
			}
		}

		if(GUI.Button (new Rect(10,155,100,45),"New Game"))
		{
			PlayerPrefs.SetInt ("Level Completed", 0);
			SceneManager.LoadScene (0); 
		}
		if(GUI.Button (new Rect(10,210,100,45),"Quit"))
		{
			Application.Quit ();
		}
	}
}