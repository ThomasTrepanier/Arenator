using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

[RequireComponent(typeof(VRTK_ControllerEvents))]
public class InputTouchPad : MonoBehaviour {

    private SpellListScript handSpellList;
    private VRTK_ControllerEvents vrtkEvents;

    void Start () {
        vrtkEvents = GetComponent<VRTK_ControllerEvents>();
        handSpellList = GetComponent<SpellListScript>();

        //vrtkEvents.TouchpadPressed += new ControllerInteractionEventHandler(DoTouchPadPressed);
        //vrtkEvents.GripPressed += new ControllerInteractionEventHandler(DoGripPressed);
        
    }

    /*public void DoTouchPadPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (GetComponent<VRTK_ControllerEvents>().GetTouchpadAxis().x < 0)
        {
            if (handSpellList.Selected < 3)
                handSpellList.Selected++;
            else
                handSpellList.Selected = 0;
            //print(handSpellList.Selected);
        }
        else
        {
            if (handSpellList.Selected > 0)
                handSpellList.Selected--;
            else
                handSpellList.Selected = 3;
            //print(handSpellList.Selected);
        }
        textSelected.text = handSpellList.Selected.ToString();
		handSpellList.SelectSpell ();
    }*/

    /*public void DoGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        handSpellList.AddSpellToHand(0);
    }*/

}
