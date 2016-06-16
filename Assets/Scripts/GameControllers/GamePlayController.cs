//2h
//16/06/2016
//22:29
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {

	//create an variable instance of the class
	//can be accessed outside of the script due to being static
	//is quicker to use as it does not require the use of GetComponent....and the creation of an object in another class
	//can then access methods and variables
	//instance not set to '=' anything (relevant below 'MakeInstance'
	public static GamePlayController instance;

	[SerializeField]
	private Text scoreText, coinText, lifeText;

	[SerializeField]
	private GameObject pausePanel;

	// Use this for initialization
	void Awake () 
	{	
		MakeInstance ();
	}
	
	void MakeInstance()
	{
		//if no object is assigned to 'instance'
		if (instance == null) 
		{
			instance = this;
		}
	}

	public void SetScore(int score)
	{
		scoreText.text = "x" + score;
	}

	public void SetCoinSCore(int coinScore)
	{
		coinText.text = "x" + coinScore;
	}

	public void SetLifeScore(int lifeScore)
	{
		lifeText.text = "x" + lifeScore;
	}


	public void PauseTheGame()
	{
		//pauses game
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
	}

	public void ResumeGame()
	{
		//reactivates game
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}

	public void QuitGame()
	{
		Time.timeScale = 1f;
		Application.LoadLevel ("MainMenu");
	}


}
