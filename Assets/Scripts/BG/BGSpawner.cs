using UnityEngine;
using System.Collections;

public class BGSpawner : MonoBehaviour {

	private GameObject[] backgrounds;
	private float lastYPosition;

	// Use this for initialization
	void Start () {
		GetBackgroundsAndSetLastY ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetBackgroundsAndSetLastY()
	{
		//fill array with Background gameObjects
		backgrounds = GameObject.FindGameObjectsWithTag ("Background");

		lastYPosition = backgrounds [0].transform.position.y;

		for (int i = 1; i < backgrounds.Length; i++) 
		{
			if (lastYPosition > backgrounds [i].transform.position.y) 
			{
				lastYPosition = backgrounds [i].transform.position.y;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Background") 	
		{
			if (target.transform.position.y == lastYPosition) 
			{
				Vector3 temp = target.transform.position;
				float height = ((BoxCollider2D)target).size.y;

				for (int i = 0; i < backgrounds.Length; i++) 
				{
					if (!backgrounds [i].activeInHierarchy) 	
					{
						temp.y -= height;

						lastYPosition = temp.y;

						backgrounds [i].transform.position = temp;

						backgrounds [i].SetActive (true);
					}
				}

			}
		}
	}
}
