using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class SBS_SpellSlotManager : MonoBehaviour {

    public VRTK_SDKManager currentManager;
    private GameObject Player;

    public VRTK_SDKSetup ViveSetup;
    public VRTK_SDKSetup SimulatorSetup;
    private VRTK_SDKSetup currentSetup;

    public ShootBullet[] Hands;
    public int CurrentSet = 1;

    public GameObject SpellBookCont;
    private SpellBookScript spellBookHere;

    public SpellClass[] spellAvailable;

    private Text SetupText;
    public SBS_SpellSlot[] slotScript;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SetupText = gameObject.transform.Find("SetText").GetComponent<Text>();

        spellBookHere = SpellBookCont.GetComponent<SpellBookScript>();
        spellAvailable = new SpellClass[GameObject.Find("[VRTK_Scripts]").GetComponentsInChildren<SBS_SpellSlot>().Length];
    }

    public void ListAwake()
    {
        foreach (Transform child in transform.root.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<SBS_SpellSlot>())
            {
                SBS_SpellSlot childSlotScript = child.GetComponent<SBS_SpellSlot>();

                spellAvailable[childSlotScript.ID] = spellBookHere.SpellBookList[0];
            }
        }

        Hands[0].selectedSpell = spellAvailable[0];
        Hands[0].SelectSpell();
        Hands[1].selectedSpell = spellAvailable[1];
        Hands[1].SelectSpell();

        SetupText.text = "Set: " + (CurrentSet);
    }

	void Update () {

        if (currentSetup == null)
        {
            if (currentManager.loadedSetup == ViveSetup)
            {
                currentSetup = ViveSetup;
            }
            else if (currentManager.loadedSetup == SimulatorSetup)
            {
                currentSetup = SimulatorSetup;
            }
        }
        if (currentSetup == SimulatorSetup)
        {
            transform.position = Player.transform.position - new Vector3(0, 2f, 0f);
        }
        if (currentSetup == ViveSetup)
        {
		    transform.localPosition = Camera.main.gameObject.transform.position -= new Vector3(0, 2f, 0f);
		    /*Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + 90, transform.eulerAngles.z);
		    transform.rotation = Quaternion.Euler(eulerRotation);*/
	    }

		if (Camera.main) {
			float yRot = Camera.main.transform.eulerAngles.y;
			if (yRot < 45 | yRot > 315)
			{
				transform.eulerAngles = new Vector3(0, 90, 0);
			}
			else if (yRot < 135 && yRot > 45)
			{
				transform.eulerAngles = new Vector3(0, 180, 0);
			}
			else if (yRot < 225 && yRot > 135)
			{
				transform.eulerAngles = new Vector3(0, 270, 0);
			}
			else if (yRot < 315 && yRot > 225)
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}
    }

    /*public void TouchSlot(SBS_SpellSlot slotScript, GameObject handShoot)
    {
        //print(slotID);
        if (handShoot.GetComponent<ShootBullet>())
        {
            ShootBullet handShootScript = handShoot.GetComponent<ShootBullet>();
            if (handShootScript.selectedSpell != spellAvailable[slotScript.ID] & handShootScript.spellID != slotScript.ID)
            {
                handShootScript.selectedSpell = spellAvailable[slotScript.ID];
                //Debug.Log(spellAvailable[slotID].name);

                handShootScript.SelectSpell();
                //Debug.Log(handShootScript.selectedSpell.name);
                handShootScript.spellID = slotScript.ID;
                handShootScript.isCD = slotScript.OnCD;

                handShootScript.UpdateCD(slotScript.startCD, slotScript.cdLeft);
                //print(handShoot.name + " Update");
            }
        }
        else
            return;
    }*/
    public void SwitchSet()
    {
        SBS_SpellSlot LeftSlotScript = slotScript[0];
        SBS_SpellSlot RightSlotScript = slotScript[1];

        if (CurrentSet == 1)
        {
            CurrentSet = 2;
            LeftSlotScript = slotScript[2];
            RightSlotScript = slotScript[3];
        }
        else if (CurrentSet == 2)
        {
            CurrentSet = 1;
            LeftSlotScript = slotScript[0];
            RightSlotScript = slotScript[1];
        }
        Hands[0].selectedSpell = spellAvailable[LeftSlotScript.ID];
        Hands[0].SelectSpell();
        Hands[0].spellID = LeftSlotScript.ID;
        Hands[0].isCD = LeftSlotScript.OnCD;
        Hands[0].UpdateCD(LeftSlotScript.startCD, LeftSlotScript.cdLeft);

        Hands[1].selectedSpell = spellAvailable[RightSlotScript.ID];
        Hands[1].SelectSpell();
        Hands[1].spellID = RightSlotScript.ID;
        Hands[1].isCD = RightSlotScript.OnCD;
        Hands[1].UpdateCD(RightSlotScript.startCD, RightSlotScript.cdLeft);

        SetupText.text = "Set: " + (CurrentSet);
    }

    public void AddSpellToSlot(int ID, SpellClass ToAddSpell)
    {
        //print(ToAddSpell.name);

        spellAvailable[ID] = ToAddSpell;

        foreach (SBS_SpellSlot slot in GetComponentsInChildren<SBS_SpellSlot>())
        {
            if (slot.ID == ID)
            {
                slot.OnCD = false;
                //print("EndManager");
            }
        }
        Hands[0].SelectSpell();
        Hands[1].SelectSpell();
    }

    public void RemoveSpellFromSlot(int ID)
    {
        spellAvailable.SetValue(null, ID);
        Hands[0].SelectSpell();
        Hands[1].SelectSpell();
    }

    public float CallCooldown(SpellClass shootedSpell, int originID)
    {
        float _cdDur = 0;
        foreach (SBS_SpellSlot slot in transform.root.GetComponentsInChildren<SBS_SpellSlot>())
        {
            if (slot.ID == originID)
            {
                _cdDur = slot.StartCooldown(shootedSpell, originID);
            }
        }
        return _cdDur;
    }

    /*public void EndCooldownOnHand(int ID)
    {
        for (int i = 0; i < Hands.Length; i++)
        {
            ShootBullet iHand = Hands[i].GetComponent<ShootBullet>();

            if(iHand.spellID == ID && iHand.selectedSpell == spellAvailable[ID])
            {
                iHand.isCD = false;
                GameManager.instance.PlaySound(GameManager.instance.EndCDClip, 0.3f);
            }
        }
    }*/
}
