using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour {

	public GameObject HUD;

	void OnTriggerEnter (){
		HUD.GetComponent<HUDController>().GainHP ();
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<BoxCollider> ().enabled = false;
		Destroy(this.gameObject,HUD.GetComponent<HUDController>().Pickup_Sound.length);
		print("HP +1");
	}
}
