using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellToCast : MonoBehaviour {

    public SpellClass CastedSpellClass;
    private GameObject spellCont;

	private Dictionary<string, float> elementDict;
    private Dictionary<string, float> typeValues;
    private ScriptableSpell_Element element;
    private ScriptableSpell_SpellType type;
    private ScriptableSpell_Attribute attribute;

    private float speedAtt = 0;
    private float sizeAtt = 0;
    private float cdAtt = 0;

    public void SetCastedSpell(SpellClass castedSpell)
    {
        CastedSpellClass = castedSpell;

        elementDict = SetElement(CastedSpellClass.spellElement);
        typeValues = SetAbility(CastedSpellClass.spellType);
        element = CastedSpellClass.spellElement;
        type = CastedSpellClass.spellType;
        attribute = CastedSpellClass.spellAttribute;

        //print(attribute.Tier);

        CustomAwake();
    }
    void CustomAwake () {
            
        //print("Cast spell: " + CastedSpell.name);

        spellCont = Instantiate(type.Spell, transform.position, transform.rotation);
        Destroy(gameObject, 1f);

        if(attribute.thisAttType == ScriptableSpell_Attribute.attType.elementAttribute)
        {
            elementDict["Damage"] += element.damage * 1/3f * attribute.Tier;
            elementDict["Dot"] += element.dot * 1/3f * attribute.Tier;
            elementDict["Slow"] += element.slow * 1 / 3f * attribute.Tier;
            elementDict["Stun"] += element.stun * 1 / 3f * attribute.Tier;
            elementDict["Duration"] += element.effectDuration * 1 / 3f * attribute.Tier;

            //Debug.Log(elementDict["Damage"]);
            //Debug.Log(element.effectDuration);
        }
        else if (attribute.thisAttType == ScriptableSpell_Attribute.attType.abilityAttribute)
        {
            speedAtt = 1/3f * attribute.Tier;
            sizeAtt = 1/3f * attribute.Tier;
            cdAtt = 1/6f * attribute.Tier;
            Debug.Log(type.Cooldown);
        }
        else if (attribute.thisAttType == ScriptableSpell_Attribute.attType.combinedAttribute)
        {
            elementDict["Damage"] += element.damage * 1 / 3f * attribute.Tier;
            elementDict["Dot"] += element.dot * 1 / 3f * attribute.Tier;
            elementDict["Slow"] += element.slow * 1 / 3f * attribute.Tier;
            elementDict["Stun"] += element.stun * 1 / 3f * attribute.Tier;
            elementDict["Duration"] += element.effectDuration * 1 / 3f * attribute.Tier;

            speedAtt = (0.16f) * attribute.Tier;
            sizeAtt = (0.16f) * attribute.Tier;
            cdAtt = (0.08f) * attribute.Tier;
        }

        if (spellCont.GetComponent<Rigidbody>() != null)
        {
            Vector3 spellVelo = spellCont.transform.forward * type.speed * type.speedMultiplier;
            spellCont.GetComponent<Rigidbody>().velocity = spellVelo + spellVelo * speedAtt;
        }
        #region Attribute Modifier

        if (spellCont.GetComponent<Renderer>()){
            spellCont.GetComponent<Renderer>().material.color = element.spellMainColor;
        }

        foreach (ParticleSystem spellChild in spellCont.GetComponentsInChildren<ParticleSystem>())
        {
            if (spellChild.GetComponent<ParticleSystem>())
            {
                ParticleSystem ps = spellChild.GetComponent<ParticleSystem>();

                var main = ps.main;

                #region Color Modifier
                if (main.startColor.mode == ParticleSystemGradientMode.TwoColors)
                {
                    Color minColor = new Color(element.spellMainColor.r, element.spellMainColor.g, element.spellMainColor.b, main.startColor.colorMax.a);
                    Color maxColor = new Color(element.spellSecColor.r, element.spellSecColor.g, element.spellSecColor.b, main.startColor.colorMin.a);

                    main.startColor = new ParticleSystem.MinMaxGradient(minColor, maxColor);
                }
                else if (main.startColor.mode == ParticleSystemGradientMode.Color)
                {
                    main.startColor = new Color(element.spellMainColor.r, element.spellMainColor.g, element.spellMainColor.b, main.startColor.color.a);
                }
                #endregion ColorModifier

                #region Size Modifier

                if (main.startSize.mode == ParticleSystemCurveMode.TwoConstants)
                {
                    float currentMin = main.startSize.constantMin;
                    float currentMax = main.startSize.constantMax;

                    float newMin = currentMin + currentMin * sizeAtt;
                    float newMax = currentMax + currentMax * sizeAtt;

                    main.startSize = new ParticleSystem.MinMaxCurve(newMin, newMax);
                }
                else if (main.startSize.mode == ParticleSystemCurveMode.Constant)
                {
                    float currentSize = main.startSize.constant;

                    float newSize = currentSize + currentSize * sizeAtt;

                    main.startSize = newSize;
                }
                #endregion Size Modifier
            }
        }
        foreach(Light light in spellCont.GetComponentsInChildren<Light>())
        {
            light.color = new Color(element.spellMainColor.r, element.spellMainColor.g, element.spellMainColor.b, element.spellMainColor.a);
        }

        #region Duration Modifier Shield
        if(CastedSpellClass.spellType.attName == "Shield")
        {
            ParticleSystem ps = spellCont.transform.Find("Shield_FX/DistortedSphere").GetComponent<ParticleSystem>();
            ParticleSystem ps2 = spellCont.transform.Find("Shield_FX/Sphere").GetComponent<ParticleSystem>();
            ParticleSystem ps3 = spellCont.transform.Find("Shield_FX/DifformedSphere").GetComponent<ParticleSystem>();

            var main = ps.main;
            var main2 = ps2.main;
            var main3 = ps3.main;

            main.startLifetime = elementDict["Duration"];
            main2.startLifetime = elementDict["Duration"];
            main3.startLifetime = elementDict["Duration"];
        }
        #endregion Duration Modifier Shield

        #region Cooldown Modifier
        
        #endregion Cooldown Modifier

        #endregion Attribute Modifier

        if (spellCont.GetComponent<SpellContScript>())
        {
            spellCont.GetComponent<SpellContScript>().thisElement = elementDict;
            spellCont.GetComponent<SpellContScript>().abiValues = typeValues;
            spellCont.GetComponent<SpellContScript>().thisAbility = type;
        }

        foreach (SpellContScript childSpell in spellCont.GetComponentsInChildren<SpellContScript>())
        {
            childSpell.thisElement = elementDict;
            childSpell.abiValues = typeValues;
            childSpell.thisAbility = type;
        }
    }

    Dictionary<string, float> SetElement(ScriptableSpell_Element element)
    {
        Dictionary<string, float> eleDict = new Dictionary<string, float>();

        eleDict.Add("Damage", element.damage);
        eleDict.Add("Dot", element.dot);
        eleDict.Add("Slow", element.slow);
        eleDict.Add("Stun", element.stun);
        eleDict.Add("Duration", element.effectDuration);

        return eleDict;
    }
    Dictionary<string, float> SetAbility(ScriptableSpell_SpellType ability)
    {
        Dictionary<string, float> abiDict = new Dictionary<string, float>();

        abiDict.Add("Cooldown", ability.Cooldown);

        return abiDict;
    }
}
