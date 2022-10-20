using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    private GameManager GM;
    public Text[] HSText;

    private List<float> HighScores;

    private void Start()
    {
        GM = GameManager.instance;

        HighScores = new List<float>();

        SortHS();

        for (int i = 0; i < HighScores.Count && i < HSText.Length; i++)
        {
            float key = HighScores[i];
            AddScore(HSText[i], key, GM.HighScore[key]);

            PlayerPrefs.SetFloat("HS" + i, key);
            PlayerPrefs.SetString("HSInit" + i, GM.HighScore[key]);
        }
    }

    float GetKey(int _i)
    {
        return GM.HighScore.Keys.ElementAt(_i);
    }

    void SortHS()
    {
        for (int i = 0; i < GM.HighScore.Count; i++)
        {
            float key = GetKey(i);
            HighScores.Add(key);
            //print(HighScores[i]);
        }

        HighScores.Sort();
        HighScores.Reverse();
    }

    public void AddScore(Text text, float Score, string Init)
    {
        text.text = Init + " : " + Score.ToString();
    }
}
