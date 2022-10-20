using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class SpellListScript : MonoBehaviour {

	public Light HandLight;
	private Light thisLight;

    public VRTK_Pointer rightPointer;

    public GameObject SpellBookCont;
    private SpellBookScript spellBookHere;

	void Start () {
        //spellBookHere = SpellBookCont.GetComponent<SpellBookScript>();
    }
	
	void Update () {

        /*if (Input.GetKeyUp("y"))
        {
            AddSpellToHand(0);
        }*/

        /*if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Selected < 3)
                Selected++;
            else
                Selected = 0;
            //print(Selected);
            textSelected.text = Selected.ToString();
			SelectSpell ();
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Selected > 0)
                Selected--;
            else
                Selected = 3;
            //print(Selected);
            textSelected.text = Selected.ToString();
			SelectSpell ();
        }*/

    }

    /*public void AddSpellToHand(int ID)
    {
        SpellClass addedSpell = spellBookHere.SpellBookList[spellBookHere.SpellbookSelected];

        SpellAvailable.RemoveAt(ID);
        SpellAvailable.Insert(ID, addedSpell);

        //print("Added spell " + addedSpell.name + " from SpellBook index " + spellBookHere.SpellBookList.IndexOf(addedSpell) + " at SpellList index " + SpellAvailable.IndexOf(addedSpell));
    }*/

	/*public void SelectSpell(){

		if (SpellAvailable [Selected] != null) {
			ScriptableSpell_Element selectedSpell = SpellAvailable [Selected].spellElement;

			//print (selectedSpell.spellMainColor);
			thisLight.color = selectedSpell.spellMainColor;

			ParticleSystem ps = thisLight.GetComponent<ParticleSystem> ();

			var main = ps.main;

			if (main.startColor.mode == ParticleSystemGradientMode.TwoColors) {
				Color minColor = new Color (selectedSpell.spellMainColor.r, selectedSpell.spellMainColor.g, selectedSpell.spellMainColor.b, main.startColor.colorMax.a);
				Color maxColor = new Color (selectedSpell.spellSecColor.r, selectedSpell.spellSecColor.g, selectedSpell.spellSecColor.b, main.startColor.colorMin.a);

				main.startColor = new ParticleSystem.MinMaxGradient (minColor, maxColor);
			} else if (main.startColor.mode == ParticleSystemGradientMode.Color) {
				main.startColor = new Color (selectedSpell.spellMainColor.r, selectedSpell.spellMainColor.g, selectedSpell.spellMainColor.b, main.startColor.color.a);
			}

		}
	}*/
}
