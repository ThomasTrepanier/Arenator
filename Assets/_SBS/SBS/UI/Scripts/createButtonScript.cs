using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createButtonScript : MonoBehaviour {

    public GameObject[] craftingMat = new GameObject[3];

    private ScriptableSpell_Element craftingElement;
    private ScriptableSpell_SpellType craftingType;
    private ScriptableSpell_Attribute craftingAttribute;

    public int Cost;
    [SerializeField]
    private Text KpText;
    [SerializeField]
    private Text costText;
    [SerializeField]
    private Text SpellName;
    [SerializeField]
    private GameObject ButtonRight;
    [SerializeField]
    private Text AlreadyCrafted;

    public GameObject SpellBookCont;
    private SpellBookScript spellBookHere;

    public AudioClip Create_Sound;
    public AudioClip Cant_Sound;
    public void Awake()
    {
        spellBookHere = SpellBookCont.GetComponent<SpellBookScript>();
    }

    void Update()
    {
        if(craftingMat[0].GetComponent<craftElementScript>().craftElement != null)
            craftingElement = craftingMat[0].GetComponent<craftElementScript>().craftElement;

        if (craftingMat[1].GetComponent<craftTypeScript>().craftType != null)
            craftingType = craftingMat[1].GetComponent<craftTypeScript>().craftType;

        if (craftingMat[2].GetComponent<craftAttributeScript>().craftAttribute != null)
            craftingAttribute = craftingMat[2].GetComponent<craftAttributeScript>().craftAttribute;

        if(craftingElement != null &&
           craftingType != null &&
           craftingAttribute != null)
        {
            SpellName.text = craftingElement.ElementName + " " + craftingType.attName + " with " + craftingAttribute.thisAttType + "T" + craftingAttribute.Tier;
            SpellCost();
        }
    }

    public void CreateClick()
    {
        if (KnowledgePts.KLPts >= Cost)
        {
            if(craftingElement != null && 
                craftingType != null && 
                craftingAttribute != null)
            {
                //spellBookHere = SpellBookCont.GetComponent<SpellBookScript>();
                //print(spellBookHere.SpellBookList.Count);

                SpellClass newSpell = new SpellClass(craftingElement, craftingType, craftingAttribute);

                if (!HasSpell(newSpell))
                {
                    spellBookHere.SpellBookList.Add(newSpell);

                    KnowledgePts.KLPts -= Cost;
                    DeathScreen.KPUsed += Cost;
                    KnowledgePts.UpdateUI();
                    ButtonRight.GetComponent<Animation>().Play("ButtonRightAnimation");
                    Invoke("StopRightAnim", 3f);

                    if (KpText.gameObject.GetComponent<Animation>().isPlaying == false)
                        KpText.gameObject.GetComponent<Animation>().Play("FlashOnce");
                    GetComponent<AudioSource>().clip = Create_Sound;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
        else
        {
            KpText.gameObject.GetComponent<Animation>().Play("Flash");
            GetComponent<AudioSource>().clip = Cant_Sound;
            GetComponent<AudioSource>().Play();
        }
    }

    public void SpellCost()
    {
        int costElement;
        int costType;
        int costAtt;

        if (craftingElement != null)
            costElement = craftingElement.Tier * 150;
        else
            costElement = 0;

        if (craftingType != null)
            costType = craftingType.Tier * 150;
        else
            costType = 0;

        if (craftingAttribute != null)
            costAtt = craftingAttribute.Tier * 150;
        else
            costAtt = 0;

        Cost = 0;
        Cost = costElement + costType + costAtt;
        costText.GetComponent<Text>().text = "Cost : " + Cost;
    }
    void StopRightAnim()
    {
        ButtonRight.GetComponent<Animation>().Stop();
    }
    bool HasSpell(SpellClass _newSpell)
    {
        print(spellBookHere.SpellBookList.Count);
        /*for (int i = 0; i < spellBookHere.SpellBookList.Count; i++)
        {
            print(spellBookHere.SpellBookList[i]);
            SpellClass spell = spellBookHere.SpellBookList[i];
            print(spell.name);

            if (spell.spellElement == _newSpell.spellElement &&
                spell.spellType == _newSpell.spellType &&
                spell.spellAttribute == _newSpell.spellAttribute)
            {
                return true;
            }
            return false;
        }*/
        foreach (SpellClass spell in spellBookHere.SpellBookList)
        {
            print(spell.name);
            if(spell.spellElement == _newSpell.spellElement &&
                spell.spellType == _newSpell.spellType &&
                spell.spellAttribute == _newSpell.spellAttribute)
            {
                AlreadyCrafted.enabled = true;
                AlreadyCrafted.gameObject.GetComponent<Animation>().Play("AlreadyCraftedClip");
                Invoke("AlreadyCraftedEnd", 2f);
                return true;
            }
        }
        return false;
    }

    void AlreadyCraftedEnd()
    {
        AlreadyCrafted.enabled = false;
    }
}
