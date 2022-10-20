using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation_Simple : MonoBehaviour {

	public Vector3 BougeDansLaDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (BougeDansLaDirection * 0.01f);
	}
}
