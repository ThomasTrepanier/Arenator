using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class ShootBullet : MonoBehaviour {

	const int MANAT1 = 2;
	const int MANAT2 = 8;
	const int MANAT3 = 30;

    private Player Player;

	public GameObject spell;
    public SpellClass selectedSpell;
    public int spellID;
    public bool isCD; 

    public VRTK_Pointer pointer;
    public VRTK_BezierPointerRenderer pointerRend;

    public GameObject HandFX;
    private GameObject thisFX;

    public GameObject SlotManagerCont;
    private SBS_SpellSlotManager slotManagerScript;

	private float cdDur;
	public Image CDImage;

    private AudioSource audioSource;

    void Awake () {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();

        thisFX = Instantiate(HandFX, transform.position, transform.rotation, transform);
		CDImage = thisFX.GetComponentsInChildren<Image>()[0];

        slotManagerScript = SlotManagerCont.GetComponent<SBS_SpellSlotManager>();
		SelectSpell ();

        if (GetComponent<VRTK_ControllerEvents>() == null)
		{
			VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
			return;
		}
		GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
        GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(DoGripPress);
    }
	
	void Update () {

        /*if(Input.GetKeyUp(KeyCode.Space)){
			Instantiate (spell, Camera.main.transform.position, Camera.main.transform.rotation);
            spell.GetComponent<SpellToCast>().CastedSpell = selectedSpell;
        }*/

		if (isCD) 
		{
			CDImage.fillAmount += (1f / cdDur) * Time.deltaTime;
			//print (CDImage.fillAmount);
		}

        #region Show Pointer or Nah
        if (selectedSpell != null)
        {
            if (!Player.inSpawn)
            {
                pointer.enabled = true;
                if (selectedSpell.spellType.projectile)
                {
                    if (pointer.IsActivationButtonPressed())
                    {
                        pointer.Toggle(!enabled);
                    }
                    pointer.enabled = false;
                }
                else
                {
                    pointer.enabled = true;
                    if (pointer.IsActivationButtonPressed())
                    {
                        if (!isCD)
                        {
                            pointer.Toggle(enabled);

                            if (selectedSpell.spellType.attName == "Explosion")
                                pointerRend.cursorRadius = 4.8f;
                            else
                                pointerRend.cursorRadius = 0.5f;
                        }
                    }
                }
            }
            else
            {
                pointer.Toggle(!enabled);
                pointer.enabled = false;
            }
        }
        #endregion Show pointer or Nah
    }

    private void DoGripPress(object sender, ControllerInteractionEventArgs e)
    {
        slotManagerScript.SwitchSet();
    }

    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e){
        if(!Player.inSpawn && selectedSpell.spellType.projectile == true)
        {
            Shoot();
        }
    }
    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        if(!Player.inSpawn && selectedSpell.spellType.projectile == false)
        {
            Shoot();
        }
    }

	public void UpdateCD(float _startCD, float _cdLeft)
	{
        StopCoroutine("InstanceEndCD");
        if (_startCD > 0)
        {
            cdDur = _startCD;
            CDImage.fillAmount = 1f - (_cdLeft / _startCD);

            StartCoroutine(InstanceEndCD(selectedSpell, spellID, _cdLeft));
        }
        else
        {
            CDImage.fillAmount = 1f;
        }
	}
    private void Shoot()
    {
        if (!isCD)
        {
            if (selectedSpell != null)
            {
                int manaCost = ManaCost(selectedSpell);

                if(Player.Mana >= manaCost)
                {
                    if (selectedSpell.spellType.projectile == true)
                    {
                        GameObject castedSpell = Instantiate(spell, transform.position, transform.rotation);

                        castedSpell.GetComponent<SpellToCast>().SetCastedSpell(selectedSpell);
                    }
                    else
                    {
                        GameObject castedSpell = Instantiate(spell, pointerRend.GetDestinationHit().point, Quaternion.identity);
                        castedSpell.transform.position += new Vector3(0, 0.5f, 0);

                        castedSpell.GetComponent<SpellToCast>().SetCastedSpell(selectedSpell);
                    }

                    //Start Cooldown and sends the spell and slot coming from
                    cdDur = slotManagerScript.CallCooldown(selectedSpell, spellID);

                    StartCoroutine(InstanceEndCD(selectedSpell, spellID, cdDur));
					CDImage.fillAmount = 0f;
                    isCD = true;

                    Player.Mana -= manaCost;
                    Player.UpdateUI();
                    PlaySound(selectedSpell.spellType.Cast_Sound);
                }
            }
        }
    }

    IEnumerator InstanceEndCD(SpellClass castedSpell, int castedID, float _cdDur)
    {
        yield return new WaitForSeconds(_cdDur);
        
        if (selectedSpell == castedSpell & spellID == castedID)
        {
            isCD = false;
			cdDur = 0f;
            GameManager.instance.PlaySound(GameManager.instance.EndCDClip, 0.3f);
            //print(gameObject.name + " Hand");
            //print("EndHand");
        }
    }
    public void SelectSpell()
    {
        //Debug.Log("ColorCalled");
        if (selectedSpell != null)
        {
            ScriptableSpell_Element selectedElement = selectedSpell.spellElement;

			if (thisFX != null) {

                foreach (ParticleSystem psChildren in thisFX.GetComponentsInChildren<ParticleSystem>())
                {
                    psChildren.Play();
                    var main = psChildren.main;

                    if (main.startColor.mode == ParticleSystemGradientMode.TwoColors)
                    {
                        Color minColor = new Color(selectedElement.spellMainColor.r, selectedElement.spellMainColor.g, selectedElement.spellMainColor.b, main.startColor.colorMax.a);
                        Color maxColor = new Color(selectedElement.spellSecColor.r, selectedElement.spellSecColor.g, selectedElement.spellSecColor.b, main.startColor.colorMin.a);

                        main.startColor = new ParticleSystem.MinMaxGradient(minColor, maxColor);
                    }
                    else if (main.startColor.mode == ParticleSystemGradientMode.Color)
                    {
                        main.startColor = new Color(selectedElement.spellMainColor.r, selectedElement.spellMainColor.g, selectedElement.spellMainColor.b, main.startColor.color.a);
                    }
                }
			}
        }
        else
        {
            if (thisFX != null)
            {
                foreach (ParticleSystem psChildren in thisFX.GetComponentsInChildren<ParticleSystem>())
                {
                    psChildren.Stop();
                }
            }

        }
    }

    private int ManaCost(SpellClass spell)
    {
        int cost = 0;

        int eleTier = spell.spellElement.Tier;
        int abiTier = spell.spellType.Tier;
        int attTier = spell.spellAttribute.Tier;

        int tierSum = eleTier + abiTier + attTier;
        float tierAverage = tierSum / 3f;

        //Manual balancing
        if (spell.spellType.attName == "Shield" && tierAverage < 2f)
        {
            tierAverage = 2f;
        }

        //Sets CD Duration
        if (tierAverage <= 4 / 3f)
        {
			cost = MANAT1 + tierSum;
        }
        else if (tierAverage > 4 / 3f && tierAverage < 2.5f)
        {
			cost = MANAT2 + tierSum;
        }
        else if (tierAverage >= 2.5f)
        {
			cost = MANAT3 + tierSum;
        }

        if (spell.spellType.attName == "Missile" || spell.spellType.attName == "Swipe")
        {
            if (tierAverage <= 1)
                cost = 0;
        }

        return cost;
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
