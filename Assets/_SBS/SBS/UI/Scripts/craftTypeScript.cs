using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class craftTypeScript : MonoBehaviour {

    Text thisText;
    public ScriptableSpell_SpellType craftType;

    // Use this for initialization
    void Start()
    {
        thisText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (craftType != null)
        {
            thisText.text = craftType.attName + " T" + craftType.Tier;
        }
        else
            thisText.text = "Choose an Ability";
    }
}
