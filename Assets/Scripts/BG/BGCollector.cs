using UnityEngine;
using System.Collections;

public class BGCollector : MonoBehaviour {


	//Deactivate backgrounds as we pass them
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Background") 
		{
			target.gameObject.SetActive(false);
		}
	}

}
