using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class craftElementScript : MonoBehaviour {

    Text thisText;
    public ScriptableSpell_Element craftElement;

	// Use this for initialization
	void Start () {
        thisText = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        if (craftElement != null)
        {
            thisText.text = craftElement.ElementName + " T" + craftElement.Tier;
        }
        else
            thisText.text = "Choose an Element";
    }
}
