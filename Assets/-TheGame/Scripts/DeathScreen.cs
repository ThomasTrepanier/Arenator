using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {

    public static DeathScreen instance;
    public string ShownStats;
    public static int NormalKilled;
    public static int FatKilled;
    public static int KamKilled;
    public static int NecroKilled;
    private float TimeStart;
	private float FinalTime;
	public float Score;
    public Text HSInitials;

    public static int T1Gathered;
    public static int T2Gathered;
    public static int T3Gathered;
    public static int KPGained;
    public static int KPUsed;

    private Text StatTitle;
    private Text P1_Title;
    private Text P1_1;
    private Text P1_2;
    private Text P1_3;
    private Text P1_4;
    private Text P2_1;
    private Text P2_2;
	private Text ScoreText;

    private void Awake()
    {
        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);

        ResetStats();
    }

    private void ResetStats()
    {
        NormalKilled = 0;
        FatKilled = 0;
        KamKilled = 0;
        NecroKilled = 0;
        Score = 0;
        T1Gathered = 0;
        T2Gathered = 0;
        T3Gathered = 0;
        KPGained = 0;
        KPUsed = 0;
        TimeStart = Time.time;
    }
    public void SetVariables()
    {
        StatTitle = transform.Find("Canvas/StatTitle").GetComponent<Text>();
        P1_Title = transform.Find("Canvas/P1_Title").GetComponent<Text>();
        P1_1 = transform.Find("Canvas/P1_1").GetComponent<Text>();
        P1_2 = transform.Find("Canvas/P1_2").GetComponent<Text>();
        P1_3 = transform.Find("Canvas/P1_3").GetComponent<Text>();
        P1_4 = transform.Find("Canvas/P1_4").GetComponent<Text>();
        P2_1 = transform.Find("Canvas/P2_1").GetComponent<Text>();
        P2_2 = transform.Find("Canvas/P2_2").GetComponent<Text>();
        ScoreText = transform.Find("Canvas/Score").GetComponent<Text>();
		FinalTime = Time.time - TimeStart;
        ShownStats = "Statistiques de Combat";

        SetStats();
    }

	// Update is called once per frame
	public void SetPos () {
		Transform player = GameObject.Find ("Player").transform;
        Vector3 setPos = new Vector3(player.position.x + player.forward.x * 3, player.position.y, player.position.z + player.forward.z * 3);
        transform.position = setPos;
        transform.localEulerAngles = new Vector3(0, player.localEulerAngles.y, 0);
    }

    void SetStats()
    {
        StatTitle.text = ShownStats;
        if (ShownStats == "Statistiques de Combat")
        {
            P1_Title.text = "Démons tués: ";
            P1_1.text = "Imp tués: " + NormalKilled;
            P1_2.text = "Beast tués: " + FatKilled;
            P1_3.text = "Kamikaze tués: " + KamKilled;
            P1_4.text = "Warlock tués: " + NecroKilled;

            P2_1.text = "Round finale: " + GameManager.CurrentRound;
			P2_2.text = "Temps de survie: " + Mathf.Round(FinalTime) + "s";
        }
		else if (ShownStats == "Statistiques d'Artisanat")
        {
            P1_Title.text = "Mots anciens récupérés: ";
            P1_1.text = "Tier 1: " + T1Gathered;
            P1_2.text = "Tier 2: " + T2Gathered;
            P1_3.text = "Tier 3: " + T3Gathered;
            P1_4.text = "";

            P2_1.text = "KP gagnés: " + KPGained;
            P2_2.text = "KP dépensés " + KPUsed;
        }
		Score = (NormalKilled + FatKilled + KamKilled + NecroKilled) * 5 + GameManager.CurrentRound * 10 + T1Gathered * 5 + T2Gathered * 10 + T3Gathered * 20 * KPUsed;
		ScoreText.text = "Score: " + Score;
    }

    public void ChangeButton(string StatsToShow)
    {
        ShownStats = StatsToShow;
        SetStats();
    }
}
