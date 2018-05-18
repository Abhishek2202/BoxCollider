using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Trap : MonoBehaviour {
	public float delatTime;


	// Use this for initialization
	void Start () {
		StartCoroutine (Go ());
	}

	IEnumerator Go()
	{
		while (true)
		{
			Animation pd = GetComponent<Animation>();
			pd.Play();	
			yield return new WaitForSeconds (3f);
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
