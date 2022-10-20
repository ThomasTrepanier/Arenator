using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Attribute", menuName = "Spell Attribute")]
public class ScriptableSpell_Attribute : ScriptableObject {

    public enum attType
    {
        elementAttribute, abilityAttribute, combinedAttribute
    }

    public attType thisAttType;
    public int Tier;

    [TextArea(3, 5)]
    public string Description;
}
