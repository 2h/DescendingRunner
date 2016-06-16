using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void goBackToMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
