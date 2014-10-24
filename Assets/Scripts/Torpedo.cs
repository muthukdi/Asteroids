using UnityEngine;
using System.Collections;

public class Torpedo : MonoBehaviour
{
	public AudioClip torpedoSound;
	void Start()
	{
		// It took a while to figure out that transform.right always
		// moves this torpedo in the direction that its pointing to
		rigidbody2D.AddForce(200.0f * transform.right);
		if (torpedoSound)
		{
			AudioSource.PlayClipAtPoint(torpedoSound, transform.position);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if (transform.position.x > GameController.rightEdge ||
		    transform.position.x < GameController.leftEdge ||
		    transform.position.y > GameController.topEdge ||
		    transform.position.y < GameController.bottomEdge)
		{
			Destroy(gameObject);
		}
	}
}
