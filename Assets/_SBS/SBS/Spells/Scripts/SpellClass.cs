using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellClass {

    public ScriptableSpell_Element spellElement;
    public ScriptableSpell_SpellType spellType;
    public ScriptableSpell_Attribute spellAttribute;

    public string name;

    public SpellClass(ScriptableSpell_Element _element, ScriptableSpell_SpellType _type, ScriptableSpell_Attribute _attribute)
    {
        spellAttribute = _attribute;
        spellElement = _element;
        spellType = _type;
        name = spellElement.ElementName + " " + spellType.Spell.name + " " + spellAttribute.thisAttType;
    }

}
