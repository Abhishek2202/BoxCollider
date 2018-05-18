using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public GameManager manager;
	public float moveSpeed;
	public bool usesManager=true;

	public GameObject deathParticles;
	private float maxSpeed=5f;

	private Vector3 input;
	private Vector3 spawn;

	public AudioClip[] audioClip;

	// Use this for initialization
	void Start () {
		spawn = transform.position;
		if (usesManager) 
		{
			manager = manager.GetComponent<GameManager> ();
			// GameManager GetComponent<GameManager>();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		input = new Vector3 (Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")); 
		if(GetComponent<Rigidbody>().velocity.magnitude< maxSpeed)
		{
		GetComponent<Rigidbody>().AddRelativeForce(input*moveSpeed);	
		}

		if (transform.position.y < -2) 
		{
			Die ();
		}
		
}

	void OnCollisionStay(Collision other)
	{

		if(other.transform.tag=="Enemy")
		{
			Die ();		
		}

	}

	void OnTriggerEnter(Collider other)
	{

		if(other.transform.tag=="Enemy")
		{
			PlaySound (2);
			Die ();
		}
		if(other.transform.tag=="Token")
		{
			if (usesManager)
			{
				manager.AddToken ();
			}
			PlaySound (0);
			Destroy (other.gameObject);
		}

		if(other.transform.tag=="Goal")
		{ 	
			PlaySound (1);
			Time.timeScale = 0f;
			manager.CompleteLevel ();
			//manager.LoadNextLevel ();

		}

	}

	void PlaySound(int clip)
	{
		AudioSource audio = GetComponent<AudioSource>();

		audio.clip = audioClip[clip];
		audio.Play ();
	}
	void Die()
	{
		Instantiate (deathParticles, transform.position, Quaternion.Euler(270,0,0)); 
		transform.position = spawn;	

	}
}