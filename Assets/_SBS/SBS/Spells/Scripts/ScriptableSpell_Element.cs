using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Element", menuName = "Spell Element")]
public class ScriptableSpell_Element : ScriptableObject {

    public string ElementName;
    public int Tier;

    public float damage;
    public float damageMultiplier;
    public float dot;
    public float dotMultiplier;
    public float slow;
    public float slowMultiplier;
    public float stun;
    public float stunMultiplier;
    public float effectDuration;

    public Color spellMainColor;
    public Color spellSecColor;

    [TextArea(3, 5)]
    public string Description;
}
