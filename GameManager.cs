using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
	//count
	public int currentScore;
	public int highscore;
	public int tokenCount;
	public int totalTokenCount;
	public int currentLevel=0;
	public int unlockedLevel;

	//timer variables
	public Rect timerRect;
	public Color warningColorTimer;
	public Color defaultColorTimer;
	public float startTime;
	private string currentTime;

	//GUI SET
	public GUISkin skin;

	//References
	public GameObject tokenParent;

	private bool completed=false;
	private bool showWinScreen=false;
	public int winScreenWidth,winScreenHeight;

	private void Update()
	{

		if (!completed) 
		{
			startTime -= Time.deltaTime;
			currentTime = string.Format ("{0:0.0}", startTime);
		
		
			if (startTime <= 0) 
			{
				startTime = 0;
				//Destroy(gameObject);
				//Application.LoadLevel ("Main Menu");
				SceneManager.LoadScene ("Main Menu");
			
			}
		
		}
	}

	public void start()
	{
		totalTokenCount = tokenParent.transform.childCount;
		if (PlayerPrefs.GetInt ("Level Completed") > 0) {
			currentLevel = PlayerPrefs.GetInt ("Level Completed");
		} else
		{
			currentLevel = 1;
		}
	}

	public void CompleteLevel()
	{		
		//LoadNextLevel ();
		//currentLevel += 1;
		//SceneManager.LoadScene (currentLevel);
		//Application.LoadLevel (currentLevel);
		showWinScreen=true;
		completed = true;
	}

	public void AddToken()
	{
		tokenCount += 1;
	}
		
	public void LoadNextLevel()
	{
		Time.timeScale = 1f;
		if (currentLevel < 4) {
			currentLevel++;
			print (currentLevel);
			SaveGame ();
			SceneManager.LoadScene (currentLevel);
		}
		else {
			print ("You Win!");
			PlayerPrefs.SetInt ("Level Completed", 0);
		
		}

	}
	void SaveGame()
	{
			PlayerPrefs.SetInt ("Level Completed", currentLevel);
		PlayerPrefs.SetInt ("Level " + currentLevel.ToString() + "score",currentScore);
	}

	void OnGUI()
	{	
		GUI.skin = skin;
		if (startTime < 5f)
		{
			skin.GetStyle ("Timer").normal.textColor = warningColorTimer;		
		} 
		else
		{
			skin.GetStyle ("Timer").normal.textColor = defaultColorTimer;
		}
		GUI.Label (timerRect, currentTime);
		GUI.Label (new Rect (45, 100, 200, 200),tokenCount.ToString () + "/" + totalTokenCount.ToString ());

		if (showWinScreen) 
		{
			Rect winScreenRect = new Rect (Screen.width/2-(Screen.width*.5f/2),Screen.height/2-(Screen.height*.5f/2),Screen.width*.5f,Screen.height*.5f);
			GUI.Box (winScreenRect,"Yeah");

			int gameTime = (int)startTime;
			currentScore = tokenCount * gameTime;
			if(GUI.Button(new Rect(winScreenRect.x + winScreenRect.width-170,winScreenRect.y + winScreenRect.height-60,150,40),"Continue"))
			{
				LoadNextLevel();
			}
			if(GUI.Button(new Rect(winScreenRect.x + 20,winScreenRect.y + winScreenRect.height-60,100,40),"Quit"))
			{
				SceneManager.LoadScene ("Main Menu");
				Time.timeScale = 1f;

			}

			GUI.Label (new Rect (winScreenRect.x + 20, winScreenRect.y + 40, 300, 50), currentScore.ToString () + "Score");
			GUI.Label (new Rect (winScreenRect.x + 20, winScreenRect.y + 90, 300, 50), "Completed Level" + currentLevel);
					
		}
	
	}
}
