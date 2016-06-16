using UnityEngine;
using System.Collections;

public class HighScoreController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void goBackToMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
