using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTimer : MonoBehaviour {

	public Text TimerText;
	public Text FPSText;
	public static float TimeNow;
	private int totalTargets;

	public GameObject HUD;

	// Update is called once per frame
	void Update () {
		TimerText.text = System.Math.Round(HUDController.TimePlaying, 2) + "";	//Shows time remaining
		totalTargets = HUD.GetComponent<HUDController>().totalTargets;			//Updates ennemies remaining from HUD Controller
		FPSText.text = (totalTargets - HUDController.targetsRemaining) + " / " + totalTargets;	//Shows ennemies destroyed and ennemies remaining
	}
}
