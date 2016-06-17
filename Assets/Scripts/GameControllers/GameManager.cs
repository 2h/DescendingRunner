//2h
//17/06/2016
//14:38
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//A singleton pattern
	public static GameManager instance;

	//don't show in the inspector. 
	//no longer adjustable in Inspector Panel.
	[HideInInspector]
	public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

	[HideInInspector]
	public int score, coinScore, lifescore;

	// Use this for initialization
	void Awake () {
		MakeSingleton ();
	}

	//eash time a scene is loaded this will be used
	void OnLevelWasLoaded()
	{
		if (Application.loadedLevelName == "Jack001") 
		{
			if (gameRestartedAfterPlayerDied) {
				GamePlayController.instance.SetScore (score);
				GamePlayController.instance.SetCoinSCore (coinScore);
				GamePlayController.instance.SetLifeScore (lifescore);

				PlayerScore.scoreCount = score;
				PlayerScore.coinCount = coinScore;
				PlayerScore.lifeCount = lifescore;
			} else if (gameStartedFromMainMenu) 
			{
				PlayerScore.scoreCount = 0;
				PlayerScore.coinCount = 0;
				PlayerScore.lifeCount = 2;

				GamePlayController.instance.SetScore (0);
				GamePlayController.instance.SetCoinSCore (0);
				GamePlayController.instance.SetLifeScore (2);
			}
		}
	}

	//A singleton object will exist across multiple scenes.
	void MakeSingleton()
	{
		//if there is an existing game manager, don't create a new one. Destroy the one that this scipt is attached too
		//to avoid duplication
		if (instance != null) 
		{
			Destroy (gameObject);
		} else 
		{
			//if there is no existing game manager, create one and keep it
			instance = this;
			DontDestroyOnLoad (gameObject);
		} 
	}

	public void CheckGameStatus(int score, int coinScore, int lifeScore)
	{
		if (lifescore < 0) {
			gameStartedFromMainMenu = false;
			gameRestartedAfterPlayerDied = false;

			//gameplay controller will be called here to reload level after death
			GamePlayController.instance.GameOverShowPanel(score, coinScore);

		} else {
			//if we still have lives left
			//Score from this instance is equal to the score passed to this method
			this.score = score;
			this.coinScore = coinScore;
			this.lifescore = lifeScore;

			gameStartedFromMainMenu = false;
			gameRestartedAfterPlayerDied = true;

			GamePlayController.instance.PlayerDiedRestartTheGame ();
		}
	}
}
