using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

	[SerializeField]
	//played when touching a coin or when life effected.
	private AudioClip coinClip, lifeClip;

	private CameraScript cameraScript;

	private bool countScoreBool
	;

	//Used in scoring. Reducing the players Y position when falling, will increment the players score.
	private Vector3 previousPosition;

	//Variables below will be accessed outside of this script. Using static type avoids the requirement for GetComponent
	public static int scoreCount;
	public static int lifeCount;
	public static int coinCount;

	void Awake()
	{
		cameraScript = Camera.main.GetComponent<CameraScript> ();
	}

	// Use this for initialization
	void Start () {
		previousPosition = transform.position;
		countScoreBool = true;
	}
	
	// Update is called once per frame
	void Update () {
		CountScore ();
	}

	void CountScore()
	{
		//short for countScore == true)
		if (countScoreBool) 
		{
			if (transform.position.y < previousPosition.y) 
			{
				scoreCount++;
			}
			previousPosition = transform.position;
		}
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Coin") 
		{
			coinCount++;
			scoreCount+=200;

			AudioSource.PlayClipAtPoint (coinClip, transform.position);
			target.gameObject.SetActive (false);
		}
			
		if (target.tag == "Life") 
		{
			lifeCount++;
			scoreCount += 300;

			AudioSource.PlayClipAtPoint (lifeClip, transform.position);
			target.gameObject.SetActive (false);
		}

		if (target.tag == "Bounds") 
		{
			//referencing CameraScript
			cameraScript.moveCamera = false;
			countScoreBool = false;

			lifeCount--;
			//With player death, player is placed at an arbitary point away from the camera
			transform.position = new Vector3 (500, 500, 0);
		}

		if (target.tag == "Deadly") 
		{
			//referencing CameraScript
			cameraScript.moveCamera = false;
			countScoreBool = false;

			lifeCount--;
			//With player death, player is placed at an arbitary point away from the camera
			transform.position = new Vector3 (500, 500, 0);
		}

	}
}
