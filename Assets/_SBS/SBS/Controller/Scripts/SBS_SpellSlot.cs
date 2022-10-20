using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBS_SpellSlot : MonoBehaviour {

	const float CDT1 = 1f;
	const float CDT2 = 6f;
	const float CDT3 = 15f;

    public int ID;
    public bool OnCD;

    public float startCD = 0;
	public float cdLeft;

	public void Update()
	{
		if (OnCD) 
		{
			cdLeft -= Time.deltaTime;
		}
	}

    public float StartCooldown(SpellClass castedSpell, int _ID)
    {
		cdLeft = 0f;
        OnCD = true;

        ScriptableSpell_SpellType ability = castedSpell.spellType;

        startCD = ability.Cooldown;

        //Starts Cooldown
        Invoke("EndCoolDown", startCD);

        //print("Cooldown = " + cdTime);
		cdLeft = startCD;
        return startCD;
    }

    private void EndCoolDown()
    {
        OnCD = false;
        startCD = 0;
		cdLeft = 0f;
        //print("EndSlot");
        //GameObject.Find("PlayerHUDManager").GetComponent<SBS_SpellSlotManager>().EndCooldownOnHand(ID);
        //print("EndSlot");
    }
}
