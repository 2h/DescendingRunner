using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	//executed when object enabled
	void OnEnable()
	{
		//Invoke is inherited from MonoBehaviour
		//Calls a function after given time (function, time)
		Invoke ("DestroyCollectable()", 6f);
	}
	//executed when object disabled
	void OnDisable()
	{
		
	}

	void DestroyCollectable()
	{
		gameObject.SetActive (false);
	}
		
}
