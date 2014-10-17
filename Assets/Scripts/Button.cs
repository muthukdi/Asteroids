using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	GameObject player;
	private Starship starship;
	public string action;
	private Color color;
	private bool buttonPressed;

	// Use this for initialization
	void Start ()
	{
		// Don't need buttons if we are running it on the Desktop.
		// Make sure to uncomment this for Unity Remote debugging!
		if (SystemInfo.deviceType == DeviceType.Desktop)
		{
			Destroy(gameObject);
		}
		player = GameObject.FindGameObjectWithTag("Player");
		starship = player.GetComponent<Starship>();
		// Make the button translucent
		color = renderer.material.color;
		renderer.material.color = new Color(color.r, color.g, color.b, 0.5f);
		buttonPressed = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the game has just started
		if (player == null && Time.time < 2.0f)
		{
			player = GameObject.FindGameObjectWithTag("Player");
			starship = player.GetComponent<Starship>();
		}
		// The player has probably died at this point so no reason
		// to continue processing touch events
		if (player == null && Time.time > 2.0f)
		{
			if (buttonPressed == true)
			{
				buttonPressed = false;
				renderer.material.color = new Color(color.r, color.g, color.b, 0.5f);
			}
			return;
		}
		if (Input.touchCount > 0)
		{
			bool collisionDetected = false;
			for (int i = 0; i < Input.touchCount; i++)
			{
				Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
				Vector2 touchPos = new Vector2(wp.x, wp.y);
				if (collider2D == Physics2D.OverlapPoint(touchPos))
				{
					collisionDetected = true;
					if (Input.GetTouch(i).phase == TouchPhase.Ended)
					{
						starship.action = "";
						buttonPressed = false;
						renderer.material.color = new Color(color.r, color.g, color.b, 0.5f);
					}
					else
					{
						starship.action = action;
						buttonPressed = true;
						renderer.material.color = color;
					}
					break;
				}
			}
			// This is to account for the finger sliding away from a button without
			// lifting it (i.e. no TouchPhase.Ended event to reset the action)
			if (collisionDetected == false && buttonPressed == true)
			{
				starship.action = "";
				buttonPressed = false;
				renderer.material.color = new Color(color.r, color.g, color.b, 0.5f);
			}
		}
		// If there are no touches and TouchPhase.Ended was never called for some reason
		else
		{
			if (buttonPressed == true)
			{
				starship.action  = "";
				buttonPressed = false;
				renderer.material.color = new Color(color.r, color.g, color.b, 0.5f);
			}
		}
	}
}
