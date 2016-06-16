//2h
//13/06/2016

using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	//private but visible in the inspector
	[SerializeField]
	private GameObject[] clouds;

	[SerializeField]
	private GameObject[] collectables;

	private GameObject player;

	private float distanceBetweenCloudsY = 3f;

	//restraining left and right side within screen bounds
	private float minX, maxX;

	//when CloudSpawner reaches this position and hits the trigger, we need to spawn new clouds
	private float lastCloudPositionY;

	//to control the x-position of clouds and randomise their location
	private float controlX;

	// wse this for initialization
	void Awake () {
		controlX = 0;
		setMinAndMaxX();
		createClouds ();
		player = GameObject.Find ("Player");

		//deactivate at beginning as they are active from the build
		for (int i = 0; i < collectables.Length; i++) 
		{
			collectables [i].SetActive (false);
		}
	}

	void Start()
	{
		positionThePlayer ();
	}

	//used to position clouds within screens x plane with small edge overlap
	void setMinAndMaxX()
	{
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height,0));

		//to postion clouds x value. .5 allows for some screen edge overlap of the clouds
		maxX = bounds.x - 0.5f;
		minX = -bounds.x + 0.5f;
	}

	//randomise gameobject positions
	void shuffle(GameObject[] arrayToShuffle)
	{
		for (int i = 0; i < arrayToShuffle.Length; i++) 
		{
			GameObject temp = arrayToShuffle [i];
			int random = Random.Range (i, arrayToShuffle.Length);
			arrayToShuffle [i] = arrayToShuffle [random];
			arrayToShuffle[random] = temp;
		}
	}

	void createClouds()
	{
		shuffle (clouds);

		float positionY = 0f;

		for (int i = 0; i < clouds.Length;i++)
		{
			Vector3 temp = clouds [i].transform.position;

			temp.y = positionY;

			if (controlX == 0) {
				temp.x = Random.Range (0.0f, maxX);
				controlX = 1;
			} else if (controlX == 1) {
				temp.x = Random.Range (0.0f, minX);
				controlX = 2;
			} else if (controlX == 2) {
				temp.x = Random.Range (1.0f, maxX);
				controlX = 3;
			}else if (controlX == 3) {
				temp.x = Random.Range (-1.0f, minX);
				controlX = 0;
			}

			lastCloudPositionY = positionY;

			clouds [i].transform.position = temp;

			positionY -= distanceBetweenCloudsY;

		}
	}

	void positionThePlayer()
	{
		GameObject[] darkClouds = GameObject.FindGameObjectsWithTag ("Deadly");
		GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag ("Cloud");

		for (int i = 0; i < darkClouds.Length; i++) 
		{
			//If a dark cloud is postioned first in the array, it will appear first in the game and instantly 
			//terminate the player. To avoid this we test to see if a dark cloud is in first position.
			//If this is the case, we change the player's position
			if (darkClouds [i].transform.position.y == 0) 
			{
				Vector3 t = darkClouds [i].transform.position;
				darkClouds [i].transform.position = new Vector3 (cloudsInGame[0].transform.position.x, 
																 cloudsInGame[0].transform.position.y,
																 cloudsInGame[0].transform.position.z);

				//reposition the dark cloud
				cloudsInGame[0].transform.position = t;
			}
		}

		Vector3 temp = cloudsInGame [0].transform.position;
		for(int i = 1; i< cloudsInGame.Length; i++)
		{
			if (temp.y < cloudsInGame [i].transform.position.y) 
			{
				temp = cloudsInGame[i].transform.position;
			}
		}

		//elevate the player aboive the cloud my modifying start position
		temp.y += 1.0f;

		player.transform.position = temp;

	}

	void OnTriggerEnter2D(Collider2D target)
	{
		//checking for last cloud in Array
		//If lst cloud encountered, create more clouds
		if (target.tag == "Cloud" || target.tag == "Deadly") 
		{
			//lastCloudPositionY saved in the create clouds method above
			if (target.transform.position.y == lastCloudPositionY) 
			{
				//regenerate array
				shuffle (clouds);
				shuffle (collectables);

				Vector3 temp = target.transform.position;

				for (int i = 0; i < clouds.Length; i++) {

					//Is the GameObject active in the scene? In this case, using '!' to signify is not active
					if (!clouds [i].activeInHierarchy) 
					{
						//same as previous. 
						//used to postion cluds on X axis.
						if (controlX == 0) {
							temp.x = Random.Range (0.0f, maxX);
							controlX = 1;
						} else if (controlX == 1) {
							temp.x = Random.Range (0.0f, minX);
							controlX = 2;
						} else if (controlX == 2) {
							temp.x = Random.Range (1.0f, maxX);
							controlX = 3;
						} else if (controlX == 3) {
							temp.x = Random.Range (-1.0f, minX);
							controlX = 0;
						}

						//set the distance again
						temp.y -= distanceBetweenCloudsY;

						lastCloudPositionY = temp.y;

						clouds [i].transform.position = temp;
						clouds [i].SetActive (true);

						//create a random index
						int random = Random.Range(0, collectables.Length);

						//if it's a dark/deadly cloud
						if (clouds [i].tag != "Deadly") 
						{
							if (!collectables [random].activeInHierarchy) 
							{
								//position the collectable above the cloud
								Vector3 temp2 = clouds [i].transform.position;
								temp2.y += 0.7f;

								if (collectables [random].tag == "Life") 
								{
									//life limited to 2
									if (PlayerScore.lifeCount < 2) 
									{
										collectables[random].transform.position = temp2;
										collectables [random].SetActive (true);
									}
								} else 
								{
									//if not life but a coin
									collectables[random].transform.position = temp2;
									collectables [random].SetActive (true);
								}
							}
						}
					}
				}

			}
		}
	}
}
