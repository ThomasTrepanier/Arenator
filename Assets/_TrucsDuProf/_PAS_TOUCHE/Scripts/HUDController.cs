using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK;

public class HUDController : MonoBehaviour {

	public GameObject HUDCanvas;
	public GameObject UIHandCanvas;
	public GameObject ennemisHolder;
	public GameObject heartsHolder;
	public GameObject LeftController;
	public GameObject RightController;

	[Range(1,12)] private int PlayerHP = 3;
	public float VitesseDesBulletsJoueur = 20f;
	public float DureeDeVieDesBulletsJoueur = 0.7f;
	//public bool ShowHpBar = true;
	//public bool CanFire = true;
	public int totalTargets; // set total number of targets in the inspector.
	public static int targetsRemaining; // this cannot be set here, instead it will be a global int changed by Target.cs.
	public Text TimePlayingText; // in the inspector: attach the "Time Remaining Text" object to use as the timer.
	public AudioClip Hit_Sound;
	public AudioClip Shoot_Sound;
	public AudioClip Pickup_Sound;

	private int CurrentHP;
	private int MaxHP;
	private GameObject heartUI;
	public static float TimePlaying; // time played, added at runtime.
	private GameObject BtnStart;
	private GameObject BtnRestart;
	public static bool gameStarted = false;
	private Vector3 HUDCanvasScale;

	// Use this for initialization
	void Start () {
		BtnStart = GameObject.Find("HUD Canvas/Btn-Start");
		BtnRestart = GameObject.Find("HUD Canvas/Btn-Restart");
		TimePlayingText.gameObject.SetActive (false);
		HUDCanvasScale = HUDCanvas.transform.localScale;

		ennemisHolder.SetActive(false);
		heartsHolder.SetActive (false);
		LeftController.GetComponent<VRTK_Pointer> ().enableTeleport = false;
		RightController.GetComponent<VRTK_Pointer> ().enableTeleport = false;

		BtnStart.SetActive (true);
		BtnRestart.SetActive(false);

		HUDCanvas.SetActive (true);
		//Time.timeScale = 0.0f;
		HUDController.TimePlaying = 0.0f; // set the start time.	
		targetsRemaining = totalTargets; // here we set the starting number of targets based on the totalTargets set in the inspector.

		heartUI = UIHandCanvas.transform.Find("HeartUI").gameObject;
		CurrentHP = PlayerHP;
		MaxHP = PlayerHP;
		SetHeartUI();

		Shoot.VitesseDesBullets = VitesseDesBulletsJoueur;
		Shoot.DureeDeVieDesBullets = DureeDeVieDesBulletsJoueur;
		Shoot.Hit_Sound = Hit_Sound;
		Shoot.Shoot_Sound = Shoot_Sound;
	}
	
	// Update is called once per frame
	void Update () {
		AttachToActiveCamera ();

		if (targetsRemaining <= 0 || CurrentHP <= 0) {
			GameOver ();
		}

		if (HUDController.gameStarted){
			// This handles the timeRemaining.
			HUDController.TimePlaying += Time.deltaTime; // add time based on the time between frames.
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			StartGame();
			print ("Start Game");
		}
	}

	public void LoseHP(){
		CurrentHP--;
		SetHeartUI();
		Shoot.PlaySound(Hit_Sound);
		print(CurrentHP+"HP left");

	}

	public void GainHP (){
		CurrentHP++;
		SetHeartUI ();
		Shoot.PlaySound (Pickup_Sound);
		print (CurrentHP + "HP left");
	}

	void SetHeartUI (){
		RectTransform HeartUIRect = heartUI.GetComponent<RectTransform>();
		RawImage HeartUIRI = heartUI.GetComponent<RawImage>();
		//HeartUIRect.sizeDelta = new Vector3 (HeartUIRI.mainTexture.width * CurrentHP, HeartUIRect.rect.height);
		HeartUIRect.sizeDelta = new Vector3 ((HeartUIRI.mainTexture.width / 8) * CurrentHP, HeartUIRect.rect.height);
		HeartUIRI.uvRect = new Rect(0,0,CurrentHP,1);
	}

	public void StartGame() {
		Time.timeScale = 1.0f;
		HUDController.gameStarted = true;
		HUDCanvas.transform.localScale = new Vector3 (0,0,0);
		BtnStart.SetActive(false);
		ennemisHolder.SetActive (true);
		heartsHolder.SetActive (true);
		//HUDCanvas.SetActive (false);
		LeftController.GetComponent<VRTK_Pointer> ().enableTeleport = true;
		RightController.GetComponent<VRTK_Pointer> ().enableTeleport = true;
	}

	public void GameOver() {
		HUDCanvas.SetActive (true);
		HUDCanvas.transform.localScale = HUDCanvasScale;
		BtnRestart.SetActive(true);
		Time.timeScale = 0.0f; // stop the ability for any objects to move (you can still look around) when all targets are hit.
		HUDController.gameStarted = false;
		TimePlayingText.gameObject.SetActive (true);

		if(targetsRemaining <= 0){
			TimePlayingText.text = "Time: " + System.Math.Round (HUDController.TimePlaying, 3); // show the time on the text.
		}
		else
			TimePlayingText.text = "GAME OVER";
	}
		
	public void RestartGame() {
		HUDCanvas.SetActive (true);
		HUDCanvas.transform.localScale = HUDCanvasScale;
		Time.timeScale = 1.0f;
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void AttachToActiveCamera(){
		// Yes it's a lot of code for one simple thing. The issue is that the main camera is not available for a couple seconds. 
		// This code attaches the HUD to the main camera, and should work for the Simulator and the Vive.
		if (Camera.main != null && gameObject.transform.parent != Camera.main.transform) {
			gameObject.transform.position = Camera.main.transform.position; // match the HUD position with the Camera positon.
			gameObject.transform.parent = Camera.main.transform; // parent the HUD to the Camera.
			gameObject.transform.localRotation = Quaternion.Euler(0,0,0); // make sure the rotation stays 0 (was 90 on y for some reason).
		}

	}
}