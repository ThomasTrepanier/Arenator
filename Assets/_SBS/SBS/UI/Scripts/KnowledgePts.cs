using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnowledgePts : MonoBehaviour {

    public static int KLPts = 0;

    static Text KPtext;

    // Use this for initialization
    void Start () {
        KPtext = gameObject.GetComponent<Text>();

        UpdateUI();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyUp(KeyCode.K))
        {
            KLPts += 100;
            UpdateUI();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            KLPts += 1000;
            UpdateUI();
        }

        
    }

    public static void UpdateUI()
    {
        KPtext.text = "Knowledge Points : " + KLPts;
    }
}
