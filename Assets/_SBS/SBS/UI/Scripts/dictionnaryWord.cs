using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class dictionnaryWord : MonoBehaviour, IPointerEnterHandler {

    private Button thisButton;

    public ScriptableSpell_Element wordElement;
    public ScriptableSpell_SpellType wordType;
    public ScriptableSpell_Attribute wordAtt;

    GameObject elementCont;
    craftElementScript elementCraftScript;

    GameObject typeCont;
    craftTypeScript typeCraftScript;

    GameObject attCont;
    craftAttributeScript attCraftScript;

    Text textComp;
    Text descText;

	// Use this for initialization
	void Start () {
        textComp = GetComponentInChildren<Text>();
        descText = transform.root.Find("Text_Description").GetComponent<Text>();

        if (wordElement != null)
        {
            textComp.text = "Element: " + wordElement.ElementName;
            elementCont = GameObject.Find("Text_SpellElement");
            elementCraftScript = elementCont.GetComponent<craftElementScript>();
        }
        if (wordType != null)
        {
            textComp.text = wordType.attName;
            typeCont = GameObject.Find("Text_SpellType");
            typeCraftScript = typeCont.GetComponent<craftTypeScript>();
        }
        if (wordAtt != null)
        {
            textComp.text = "Attribute: " + wordAtt.thisAttType.ToString() + " T" + wordAtt.Tier.ToString();
            attCont = GameObject.Find("Text_SpellAttribute");
            attCraftScript = attCont.GetComponent<craftAttributeScript>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descText.text = "";

        if (wordElement != null)
        {
            descText.text = "Description: " + wordElement.Description;
        }
        if (wordType != null)
        {
            descText.text = "Description: " + wordType.Description;
        }
        if (wordAtt != null)
        {
            descText.text = "Description: " + wordAtt.Description;
        }
    }
    public void wordClick()
    {
        //print("Click");
        if (wordElement != null)
        {
            elementCraftScript.craftElement = wordElement;
            //print("send1");
        }
            
        if (wordType != null)
        {
            typeCraftScript.craftType = wordType;
            //print("send2");
        }
            
        if (wordAtt != null)
        {
            attCraftScript.craftAttribute = wordAtt;
            //print("send3");
        }
            
    }
}
