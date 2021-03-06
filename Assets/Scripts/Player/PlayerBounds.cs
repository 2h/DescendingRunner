﻿using UnityEngine;
using System.Collections;

public class PlayerBounds : MonoBehaviour {

	private float minX;
	private float maxX;


	// Use this for initialization
	void Start () {
		setMinAndMaxX ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If the  player hits the bounds, move him back within the game window
		if (transform.position.x < minX) 
		{
			Vector3 temp = transform.position;
			temp.x = minX;
			transform.position = temp;
		}

		if (transform.position.x > maxX) 
		{
			Vector3 temp = transform.position;
			temp.x = maxX;
			transform.position = temp;
		}	
	}

	void setMinAndMaxX()
	{
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height,0));

		//copied from CloudSpawner
		maxX = bounds.x;
		minX = -bounds.x;
	}
}
