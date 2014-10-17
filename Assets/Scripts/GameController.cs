using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	// Hard coded values to implement screen wrapping effect
	public static float rightEdge = 6.370152f;
	public static float leftEdge = -6.370152f;
	public static float topEdge = 4.997135f;
	public static float bottomEdge = -4.997135f;

	// Use this for initialization
	void Start ()
	{
		GUIText GUIText = GameObject.Find ("GUI Text").GetComponent<GUIText> ();
		GUIText.text = "Screen width = " + Screen.width + ", Screen height = " + Screen.height;
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
