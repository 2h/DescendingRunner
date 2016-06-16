using UnityEngine;
using System.Collections;

public class BGScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		//Resize the background images to the width of the camera view in game.
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Vector3 tempScale = transform.localScale;

		float width = sr.sprite.bounds.size.x;

		//Camera height
		float worldHeight = Camera.main.orthographicSize * 2f;
		float worldWidth = worldHeight / Screen.height * Screen.width;

		tempScale.x = worldWidth / width;

		transform.localScale = tempScale;

	}


}
