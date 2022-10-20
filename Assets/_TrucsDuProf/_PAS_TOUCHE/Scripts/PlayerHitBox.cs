using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitBox : MonoBehaviour {

	public GameObject HUD;

	void OnTriggerEnter (Collider col){
		print ("Player HIT by something");


		if(col.gameObject.tag == "EnemyBullet"){
			HUD.GetComponent<HUDController> ().LoseHP();
			//print("Player hit by enemybullet");

		}
	}
}
