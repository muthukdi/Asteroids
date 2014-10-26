using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{

	public Transform explosion;
	public Transform asteroid;
	private Vector3 rotationAngle;
	public bool thirdChunk;

	// Use this for initialization
	void Start ()
	{
		rotationAngle = new Vector3(0.0f, 0.0f, Random.Range(-1.0f, 1.0f));
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Torpedo")
		{
			// Destroy the torpedo on impact
			Destroy(coll.gameObject);
			Torpedo.hitCount++;
			// Vibrate the device if it is a handheld
			Handheld.Vibrate();
			// Spawn an explosion
			Transform explosionClone = (Transform) Instantiate(explosion, transform.position, Quaternion.identity);
			explosionClone.localScale = new Vector3(transform.localScale.x * explosionClone.localScale.x, 
			                                        transform.localScale.y * explosionClone.localScale.y, 
			                                        explosionClone.localScale.z);
			// If this asteroid is big enough, then break it up into smaller chunks
			if (transform.localScale.x > 0.3f)
			{
				// Create the three asteroid chunks using the prefab
				Transform clone1 = (Transform) Instantiate(asteroid, transform.position, Quaternion.identity);
				Transform clone2 = (Transform) Instantiate(asteroid, transform.position, Quaternion.identity);
				Transform clone3 = (Transform) Instantiate(asteroid, transform.position, Quaternion.identity);
				// Make the chunks appear smaller than the original
				Vector3 cloneScale = new Vector3(0.75f * transform.localScale.x, 
				                                 0.75f * transform.localScale.y, 
				                                 transform.localScale.z);
				clone1.localScale = clone2.localScale = clone3.localScale = cloneScale;
				// Mark the third chunk with special attributes so that it
				// will no longer interact with the game world and will appear to fly away.
				Asteroid script = clone3.GetComponent<Asteroid>();
				script.thirdChunk = true;
				script.rigidbody2D.collider2D.enabled = false;
				// Apply an initial force to the first two chunks in opposite directions, normal to the direction of impact
				clone1.gameObject.rigidbody2D.AddForce(40.0f * new Vector2(coll.contacts[0].normal.y, -coll.contacts[0].normal.x) + transform.rigidbody2D.velocity);
				clone2.gameObject.rigidbody2D.AddForce(40.0f * new Vector2(-coll.contacts[0].normal.y, coll.contacts[0].normal.x) + transform.rigidbody2D.velocity);
			}
			// Destroy the current asteroid object
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Rotate the asteroid by a fixed angle
		transform.Rotate(rotationAngle);
		// Screen wrapping effect
		if (transform.position.x > GameController.rightEdge && rigidbody2D.velocity.x > 0.05f)
		{
			transform.position = new Vector3(GameController.leftEdge, transform.position.y);
		}
		else if (transform.position.x < GameController.leftEdge && rigidbody2D.velocity.x < -0.05f)
		{
			transform.position = new Vector3(GameController.rightEdge, transform.position.y);
		}
		else if (transform.position.y > GameController.topEdge && rigidbody2D.velocity.y > 0.05f)
		{
			transform.position = new Vector3(transform.position.x, GameController.bottomEdge);
		}
		else if (transform.position.y < GameController.bottomEdge && rigidbody2D.velocity.y < -0.05f)
		{
			transform.position = new Vector3(transform.position.x, GameController.topEdge);
		}
		// If the asteroid strays too far from any of the edges, destroy it
		if (transform.position.x > GameController.rightEdge + 1.0f ||
		    transform.position.x < GameController.leftEdge - 1.0f ||
		    transform.position.y > GameController.topEdge + 1.0f ||
		    transform.position.y < GameController.bottomEdge - 1.0f) 
		{
			Destroy(gameObject);
		}
		// Shrinking the scale of this asteroid will give the illusion
		// of it flying away into the third dimension
		if (thirdChunk)
		{
			transform.localScale *= 0.995f;
		}
		// Destroy the asteroid if it is no longer visible
		if (transform.localScale.x < 0.05f)
		{
			Destroy(gameObject);
		}
	}
}
