using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class Ennemi_Prof : MonoBehaviour {

	public GameObject Bullet;
	private GameObject VRTK_SDK_Manager;

	private GameObject player;
	private GameObject bulletSpawn;
	private GameObject heartUI;
	private GameObject enmText;
	private GameObject SimulatorRig;
	private GameObject SteamVrRig;

	[Range(1,5)]public int HP = 1;
	public float DistanceDeDetection = 20f;
	public float VitesseDeTir = 30f;
	public float CadenceDeTir = 2.0f;
	private float DureeDeVieDesBulletsEnnemi = 5f;
	private bool MontrerLesHP = true;
	public bool PeutTirer = true;
	public bool PeutRegarderLeJoueur = true;
	public AudioClip Hit_Sound;
	public AudioClip Shoot_Sound;

	private float simulatorBulletsSpeedMultiplier = 0.75f;
	private int CurrentHP;
	private float playerDist;
	private bool readyToShoot = true;
	private int HeartIconWidth;
	private bool hitOnce = false;
	private bool checkedIfSim = false;
	private Vector3 stayPos;

	// Use this for initialization
	void Start () {
		heartUI = transform.Find("_AJOUTE-MOI_DANS_ENNEMI/Canvas/HeartUI").gameObject;
		//heartUI = this.gameObject;
		//print (transform.Find ("Canvas/HeartUI").gameObject.name);

		VRTK_SDK_Manager = GameObject.FindGameObjectWithTag("Player");

		//player = Camera.main.gameObject;
		//print(player.name);
		bulletSpawn = transform.Find("_AJOUTE-MOI_DANS_ENNEMI/bulletSpawn").gameObject;
		enmText = transform.Find("_AJOUTE-MOI_DANS_ENNEMI/Canvas/EnmText").gameObject;
		enmText.SetActive(false);
		//HeartIconWidth = GetComponent<RawImage>().mainTexture.width;

		CurrentHP = HP;
		SetHealthUI();
		heartUI.SetActive(MontrerLesHP);

		SteamVrRig = VRTK_SDK_Manager.transform.Find ("SDKSetups/SteamVR").gameObject;
		SimulatorRig = VRTK_SDK_Manager.transform.Find ("SDKSetups/Simulator").gameObject;
		//player = GameObject.FindGameObjectWithTag("Player");

	}

	// Update is called once per frame
	void Update () {
		
		if (SteamVrRig.activeSelf){
			player = SteamVrRig.transform.Find ("[CameraRig]/Camera (eye)").gameObject;
		}

		if (SimulatorRig.activeSelf){
			player = SimulatorRig.transform.Find ("VRSimulatorCameraRig/Neck").gameObject;
		}

		if(player != null){
			playerDist = Vector3.Distance(transform.position, player.transform.position);

			if(playerDist < DistanceDeDetection){
				if(PeutRegarderLeJoueur){
					transform.LookAt(player.transform);
				}
				bulletSpawn.transform.LookAt(player.transform);
				//bulletSpawn.transform.LookAt(player.transform);
				//enmText.SetActive(true);
				if(readyToShoot){
					readyToShoot = false;
					StartCoroutine ("WaitAndFire");
				}
			}
			else {
				enmText.SetActive(false);
				transform.eulerAngles = new Vector3(0,0,0);
				//justStoppedLooking = true;
			}
		}
	}

	void Fire1(){
		if(PeutTirer){
			GetComponent<AudioSource>().clip = Shoot_Sound;
			GetComponent<AudioSource>().Play();
			GameObject enemyBulletClone = Instantiate(Bullet,bulletSpawn.transform.position,bulletSpawn.transform.rotation);
			Rigidbody enemyBulletRigidbody = enemyBulletClone.GetComponent<Rigidbody>();
			if (SimulatorRig.activeSelf && !checkedIfSim){
				VitesseDeTir *= simulatorBulletsSpeedMultiplier;
				checkedIfSim = true;
			}
			enemyBulletClone.GetComponent<Bullet>().VitesseDuBullet = VitesseDeTir;
			enemyBulletClone.GetComponent<Bullet>().DureeDeVieDuBullet = DureeDeVieDesBulletsEnnemi;
		}
	}

	IEnumerator WaitAndFire(){
		Fire1();
		yield return new WaitForSeconds (CadenceDeTir);
		readyToShoot = true;
	}

	void OnTriggerEnter (Collider col){
		if(col.gameObject.tag == "Bullet"){
			//HUDController.targetsRemaining -= 1; // changes the targets remaining in the HUDController script on the HUD game object.
			LoseHP();

			GetComponent<AudioSource>().clip = Hit_Sound;
			GetComponent<AudioSource>().Play();
			Destroy(col.gameObject);
			print("Enemy hit");
		}
	}

	void LoseHP(){
		CurrentHP--;
		SetHealthUI();
		SetEnemyColor();
		//print("Enemy has "+CurrentHP+"HP left");
		if(CurrentHP <= 0 && !hitOnce){
			hitOnce = true;
			HUDController.targetsRemaining--;
			transform.Find("default").gameObject.GetComponent<MeshRenderer>().enabled = false;
			Destroy(this.gameObject,GetComponent<AudioSource>().clip.length);
			print("Enemy destroyed");
		}
	}

	void SetHealthUI (){

		/*RectTransform HeartUIRect = heartUI.GetComponent<RectTransform>();
		RawImage HeartUIRI = heartUI.GetComponent<RawImage>();
		HeartUIRect.sizeDelta = new Vector3 ((HeartUIRI.mainTexture.width * 4) * CurrentHP, HeartUIRect.rect.height);
		HeartUIRI.uvRect = new Rect(0,0,CurrentHP,1);*/
	}

	//NOT USED for this project
	void SetEnemyColor(){
		Renderer rend = GetComponent<Renderer>();
		float CurrentHPf = (float) CurrentHP;
		float HPf = (float) HP;
		rend.material.color = new Color(1,1f-((HPf-CurrentHPf)/HPf),1f-((HPf-CurrentHPf)/HPf));
		//print("Current HP "+CurrentHPf);
		//print("Max HP "+MaxHPf);
		//print(((MaxHPf-CurrentHPf)/MaxHPf));
	}
}
