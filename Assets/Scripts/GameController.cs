using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	GameObject player;
	GameObject asteroid;

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
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update ()
	{
		// This will be null if there are no asteroids left
		asteroid = GameObject.FindGameObjectWithTag("Asteroid");
		// If the game has just started
		if (player == null && Time.time < 2.0f)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		if (player != null && asteroid != null)
		{
			GUIText.text = (int)(Time.time) + " seconds";
		}
	}
}
