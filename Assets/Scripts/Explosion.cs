using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
	private float timeToDeath;
	public AudioClip explodeSound;
	// Use this for initialization
	void Start ()
	{
		timeToDeath = Time.time + 4.0f;
		if (explodeSound)
		{
			AudioSource.PlayClipAtPoint(explodeSound, transform.position);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > timeToDeath)
		{
			Destroy(gameObject);
		}
	}
}
