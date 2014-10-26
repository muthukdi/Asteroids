using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	GameObject playerObject;
	GameObject asteroidObject;

	// Hard coded values to implement screen wrapping effect
	public static float rightEdge = 6.370152f;
	public static float leftEdge = -6.370152f;
	public static float topEdge = 4.997135f;
	public static float bottomEdge = -4.997135f;

	// Message label
	private GUIText GUIText;

	// Use this for initialization
	void Start ()
	{
		GUIText = GameObject.Find("GUI Text").GetComponent<GUIText> ();
		//GUIText.text = "Screen width = " + Screen.width + ", Screen height = " + Screen.height;
		playerObject = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update ()
	{
		// This will be null if there are no asteroids left
		asteroidObject = GameObject.FindGameObjectWithTag("Asteroid");
		// If the game has just started
		if (playerObject == null && Time.time < 2.0f)
		{
			playerObject = GameObject.FindGameObjectWithTag("Player");
		}
		// If there are still asteroids and a player in the game
		if (playerObject != null && asteroidObject != null)
		{
			// Heads up display
			GUIText.text = "Time Elapsed: " + (int)(Time.time) +
						   " seconds,   Hit Count = " + Torpedo.hitCount + 
						   ",   Launched = " + Torpedo.launchCount +
						   ",   Miss Count = " + Torpedo.missCount;
		}
	}
}
