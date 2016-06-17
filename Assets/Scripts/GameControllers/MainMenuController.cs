using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void startGame()
	{
		GameManager.instance.gameStartedFromMainMenu = true;
		Application.LoadLevel ("Jack001");
	}

	public void highScoreMenu()
	{
		Application.LoadLevel ("HighScore");
	}

	public void optionsMenu()
	{
		Application.LoadLevel ("OptionsMenu");
	}

	public void quitGame()
	{
		Application.Quit ();
	}

	public void musicButton()
	{
		
	}
}
