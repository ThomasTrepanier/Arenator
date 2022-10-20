using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class craftAttributeScript : MonoBehaviour {

    Text thisText;
    public ScriptableSpell_Attribute craftAttribute;

    // Use this for initialization
    void Start()
    {
        thisText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (craftAttribute != null)
        {
            thisText.text = craftAttribute.thisAttType + " T" + craftAttribute.Tier;
        }
        else
            thisText.text = "Choose an Attribute";
    }
}
