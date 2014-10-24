using UnityEngine;
using System.Collections;

public class Starship : MonoBehaviour
{
	public Transform torpedo;
	public Transform explosion;
	private Vector3 rotationAngle;
	private float nextFire;
	private float nextReload;
	public string action;
	private int torpedoHealth;  // torpedo launcher health (0 means disabled)
	public AudioClip emptySound;
	public AudioClip reloadSound;

	// Use this for initialization
	void Start ()
	{
		rotationAngle = new Vector3(0.0f, 0.0f, 1.0f);
		// Give it a random orientation
		transform.Rotate(new Vector3 (0.0f, 0.0f, Random.Range(-100.0f, 100.0f)));
		nextFire = Time.time;
		action = "";
		torpedoHealth = 5;
		nextReload = 0.0f;
	}

	// Called when this collides with an asteroid
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Asteroid")
		{
			// Destroy this starship on impact
			Destroy(gameObject);
			// Spawn an explosion
			Transform explosionClone = (Transform) Instantiate(explosion, transform.position, Quaternion.identity);
			explosionClone.localScale = new Vector3(transform.localScale.x * explosionClone.localScale.x, 
			                                        transform.localScale.y * explosionClone.localScale.y, 
			                                        explosionClone.localScale.z);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey("left") || action == "left")
		{
			// Rotate the startship counter-clockwise by a fixed angle
			transform.Rotate(rotationAngle);
		}
		if (Input.GetKey("right") || action == "right")
		{
			// Rotate the starship clockwise by a fixed angle
			transform.Rotate(-rotationAngle);
		}
		if (Input.GetKey("up") || action == "up")
		{
			// Apply forward thrust
			if (rigidbody2D.velocity.x < 3.0f && rigidbody2D.velocity.y < 3.0f)
			{
				rigidbody2D.AddForce(1.0f * transform.right);
			}
		}
		if (Input.GetKey("down") || action == "down")
		{
			// Apply backward thrust
			if (rigidbody2D.velocity.x < 3.0f && rigidbody2D.velocity.y < 3.0f)
			{
				rigidbody2D.AddForce(-1.0f * transform.right);
			}
		}
		if ((Input.GetKey("space") || action == "fire") && Time.time > nextFire)
		{
			if (torpedoHealth > 0)
			{
				Transform clone = (Transform) Instantiate(torpedo, transform.position, transform.rotation);
				// If the ship is moving when it fires the torpedo, the torpedo must inherit some of that momentum
				clone.gameObject.rigidbody2D.AddForce(transform.rigidbody2D.velocity);
				// If they fire too rapidly, decrease their weapon health temporarily
				if (Time.time < nextFire + 0.05f)
				{
					torpedoHealth--;
				}
				else
				{
					// Gradually recover weapon health
					if (torpedoHealth < 5)
					{
						torpedoHealth++;
					}
				}
				nextFire = Time.time + 0.5f;
				// Set the delay before the next reload
				if (torpedoHealth == 0)
				{
					nextReload = Time.time + 4.0f;
				}
			}
			else
			{
				if (emptySound)
				{
					AudioSource.PlayClipAtPoint(emptySound, transform.position);
				}
				nextFire = Time.time + 0.5f;
			}
		}
		if (Time.time > nextReload && torpedoHealth == 0)
		{
			torpedoHealth = 5;
			if (reloadSound)
			{
				AudioSource.PlayClipAtPoint(reloadSound, transform.position);
			}
		}
	}
}
