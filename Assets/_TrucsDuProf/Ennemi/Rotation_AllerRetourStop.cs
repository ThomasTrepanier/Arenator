using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_AllerRetourStop : MonoBehaviour {

	int decompte = 0;

	public int Delai = 100;
	public Vector3 TourneAutourDe;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		decompte++;
		if (decompte < Delai) {
				
		}
		else if (decompte +1 < Delai * 2) {
			transform.Rotate (TourneAutourDe);
		}
		else if (decompte +1 < Delai * 3) {
			transform.Rotate (-TourneAutourDe * 2);
		}
		else if (decompte < Delai * 4) {
			decompte = 0;
		}
	}
}
