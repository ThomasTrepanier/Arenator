using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float rotLeftRight = Input.GetAxis("Mouse X");
		transform.Rotate (0, rotLeftRight, 0);
		float rotUpDown = Input.GetAxis("Mouse Y");
		Camera.main.transform.Rotate (-rotUpDown, 0, 0);

		float forwardSpeed = Input.GetAxis("Vertical");
		float sideSpeed = Input.GetAxis ("Horizontal");

		Vector3 speed = new Vector3 (sideSpeed, 0, forwardSpeed);
		CharacterController cc = GetComponent<CharacterController> ();
		speed = transform.rotation * speed;
		cc.SimpleMove (speed);
	}
}
