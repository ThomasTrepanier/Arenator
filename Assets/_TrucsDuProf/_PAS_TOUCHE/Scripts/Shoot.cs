using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Shoot : MonoBehaviour {

	public GameObject Bullet;
	public GameObject bulletSpawnSim;
	private GameObject bulletSpawnVR;

	//public float playerDist;
	public static float VitesseDesBullets;		//Contrôlé par HUDController
	public static float DureeDeVieDesBullets;	//Contrôlé par HUDController
	public static AudioClip Hit_Sound;			//Contrôlé par HUDController
	public static AudioClip Shoot_Sound;		//Contrôlé par HUDController
	private float rateOfFire = 0.1f;
	private bool readyToShoot = true;
	private bool pressingTrigger = false;
	private bool pressingBar = false;
	private bool autoFire = false;  			//Sets the shooter to auto-fire mode
	public static GameObject Instance;			//Makes this script variables accessible to any othe script

	// Use this for initialization
	void Start () {
		transform.parent.GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
		transform.parent.GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased); 	//Used for auto-shoot
		bulletSpawnVR = transform.Find("bulletSpawnVR").gameObject;
		Instance = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		//Used for auto-shoot
		if(pressingTrigger || pressingBar){
			if(readyToShoot && autoFire){
				StartCoroutine ("WaitAndFire");
				//print ("Fire !");
			}
		}

		if(Input.GetKeyDown(KeyCode.Space)){
//			if(readyToShoot) {
//				GameObject bulletClone = Instantiate (Bullet, bulletSpawnSim.transform.position, bulletSpawnSim.transform.rotation);
//				bulletClone.GetComponent<Bullet>().VitesseDuBullet = VitesseDesBullets;
//				bulletClone.GetComponent<Bullet>().DureeDeVieDuBullet = DureeDeVieDesBullets;
//				PlaySound(Shoot_Sound);
//				print ("Fire !");
//			}
			pressingBar = true;
			Fire(0);
		}
		else if(Input.GetKeyUp(KeyCode.Space))
			pressingBar = false;
	}

	private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e){
		Fire(1);
		pressingTrigger = true;
	}

	void Fire(int i){
		GameObject bulletClone;
		if(i == 0){
			bulletClone = Instantiate (Bullet, bulletSpawnSim.transform.position, bulletSpawnSim.transform.rotation);
			bulletClone.GetComponent<Bullet>().VitesseDuBullet = VitesseDesBullets;
		}
		else if(i == 1){
			bulletClone = Instantiate (Bullet, bulletSpawnVR.transform.position, bulletSpawnVR.transform.rotation);
			bulletClone.GetComponent<Bullet>().VitesseDuBullet = VitesseDesBullets;
		}
		PlaySound (Shoot_Sound);
		print ("Fire !");
	}

	public static void PlaySound(AudioClip clip){
		Instance.GetComponent<AudioSource>().clip = clip;
		Instance.GetComponent<AudioSource>().Play();
	}

	//Used for auto-shoot
	private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e){
		pressingTrigger = false;
		readyToShoot = true;
		StopCoroutine("WaitAndFire");
		//print ("Trigger released");
	}

	IEnumerator WaitAndFire(){
		if (pressingBar && readyToShoot) {
			Fire (0);
			//print ("ordered fire");
		}
		if (pressingTrigger && readyToShoot) {
			Fire (1);
			//print ("ordered fire");
		}
		readyToShoot = false;
		yield return new WaitForSeconds (rateOfFire);
		readyToShoot = true;
		//print ("ready to shoot");
	}
}
