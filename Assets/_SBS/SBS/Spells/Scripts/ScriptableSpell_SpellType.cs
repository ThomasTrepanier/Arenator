using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Spell Type", menuName = "Spell Type")]
public class ScriptableSpell_SpellType : ScriptableObject {

    public GameObject Spell;
    public string attName;
    public Sprite Icon;
    public int Tier;

    public float speed;
    public float speedMultiplier;
    public float sizeMultiplier;
    public float lifetimeMultiplier;

    public float Cooldown;

    public bool projectile = true;
	public AudioClip Cast_Sound;

    [TextArea(4,6)]
    public string Description;
}
