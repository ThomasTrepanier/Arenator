using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public float multiplier = 0.5f;
	// Update is called once per frame
	void Update () {
		float rotate = Mathf.Sin (Time.time)* multiplier;
		transform.Rotate(rotate, rotate, rotate);
		//print (rotate);
	}

	void OnTriggerEnter(Collider col)
	{
		//print (col.transform);
		if (col.gameObject.tag == "Player")
		{
			//print ("Play");
			GetComponent<AudioSource>().Play();
		}
	}
}
