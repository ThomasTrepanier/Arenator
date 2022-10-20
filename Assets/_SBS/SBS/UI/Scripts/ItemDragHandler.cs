using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour {

    private Vector3 origin;
    public bool sloted = false;
    private int draggedID;
    public SpellClass SpellCont;

    private VRTK_UIDraggableItem drag;
    public float dragDistanceDestroy = 1f;

    public GameObject SpellBookCont;
    public SpellBookScript spellBookHere;

    public GameObject spellSlotCont;
    private SBS_SpellSlotManager handSlotScript;

    void Start () {
        drag = GetComponent<VRTK_UIDraggableItem>();
        origin = transform.localPosition;
        sloted = false;

        SpellBookCont = GameObject.Find("SpellBook");
        spellBookHere = SpellBookCont.GetComponent<SpellBookScript>();

        spellSlotCont = GameObject.Find("PlayerHUDManager");
        handSlotScript = spellSlotCont.GetComponent<SBS_SpellSlotManager>();
    }
	
    void Update()
    {
        if(SpellCont == null)
        {
            Spawn();
        }
    }
    public void Spawn()
    {
        Image SpellIcon = GetComponent<Image>();
        Transform dropZone = transform.parent;
        int SpellbookID = 0;
        if (dropZone.GetComponent<ItemDropHandler>())
        {
            ItemDropHandler dropHandler = dropZone.GetComponent<ItemDropHandler>();

            if (!dropHandler.origin)
            {
                SpellbookID = dropHandler.DropID;

                SpellCont = spellBookHere.SpellBookList[0];
                SpellIcon.sprite = SpellCont.spellType.Icon;
                SpellIcon.color = SpellCont.spellElement.spellMainColor;
            }
        }
    }
    public void OnDrop()
    {
        Transform dropZone = transform.parent;

        if (dropZone.GetComponent<ItemDropHandler>())
        {
            ItemDropHandler dropHandler = dropZone.GetComponent<ItemDropHandler>();

            if(dropHandler.origin)
            {
                handSlotScript.RemoveSpellFromSlot(draggedID);
                Destroy(gameObject);
            }
            else
            {
                if (dropZone.transform.childCount > 1)
                {
                    Destroy(dropZone.GetChild(0).gameObject);
                }

                foreach (ItemDropHandler drop in SpellBookCont.GetComponentsInChildren<ItemDropHandler>())
                {
                    if (drop.origin)
                    {
                        if(drop.transform.childCount < 1)
                            spellBookHere.SpawnIcon();
                    }
                }
                handSlotScript.AddSpellToSlot(dropZone.GetComponent<ItemDropHandler>().DropID, SpellCont);
                sloted = true;
                draggedID = dropHandler.DropID;
            }
        }
    }

    public void ResetDestroy()
    {
        handSlotScript.RemoveSpellFromSlot(draggedID);
        Destroy(gameObject);
    }
}
