using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Simple : MonoBehaviour {

	int decompte = 0;

	public int Delai = 100;
	public Vector3 TourneAutourDe;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (TourneAutourDe);
	}
}
