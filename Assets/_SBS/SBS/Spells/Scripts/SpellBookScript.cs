using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookScript : MonoBehaviour {

    public ScriptableSpell_Element baseSpellElement;
    public ScriptableSpell_SpellType baseSpellType;
    public ScriptableSpell_Attribute baseSpellAttribute;

    public int PageNb = 0;
    public int SpellbookSelected = 0;

    public Text TextPage;
    public Image SpellIcon;

    public GameObject IconPref;
    public GameObject originDrop;

    public List<SpellClass> SpellBookList;

    public GameObject SlotManagerCont;
    private SBS_SpellSlotManager slotManagerScript;

	// Use this for initialization
	void Start () {
        slotManagerScript = SlotManagerCont.GetComponent<SBS_SpellSlotManager>();

        SpellBookList = new List<SpellClass>();
        SpellBookList.Add(new SpellClass(baseSpellElement, baseSpellType, baseSpellAttribute));
        slotManagerScript.ListAwake();

        PageNb = 0;
        SpellbookSelected = 0;

        UpdateUI();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnIcon()
    {
        Instantiate(IconPref, originDrop.transform.position, originDrop.transform.rotation, originDrop.transform);
        UpdateUI();
    }

    public void ChangePage(GameObject button)
    {
        //print("Click");
        if(button.name == "Button_Left")
        {
            //print("Left");
            if (SpellbookSelected > 0)
                SpellbookSelected--;
            else
                SpellbookSelected = SpellBookList.Count - 1;
        }
        else if (button.name == "Button_Right")
        {
            //print("Right");
            if (SpellbookSelected < SpellBookList.Count - 1)
                SpellbookSelected++;
            else
                SpellbookSelected = 0;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        SpellIcon = originDrop.transform.GetChild(0).GetComponent<Image>();
        ItemDragHandler iconDrag = SpellIcon.gameObject.GetComponent<ItemDragHandler>();

        iconDrag.SpellCont = SpellBookList[SpellbookSelected];
        SpellIcon.sprite = iconDrag.SpellCont.spellType.Icon;
        SpellIcon.color = iconDrag.SpellCont.spellElement.spellMainColor;

        //print(SpellIcon.gameObject.GetComponent<ItemDragHandler>().SpellCont.name);

        PageNb = SpellbookSelected;
        TextPage.text = PageNb.ToString();
    }
}
