using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContScript : MonoBehaviour {

    public ScriptableSpell_SpellType thisAbility;
    public Dictionary<string, float> thisElement;
    public Dictionary<string, float> abiValues;

    private void Update()
    {
        if(thisAbility != null)
        {
            if (thisAbility.attName != "Missile" && GetComponentInChildren<ParticleSystem>())
            {
                if (!GetComponentInChildren<ParticleSystem>().IsAlive(true))
                {
                    Destroy(gameObject.transform.root.gameObject);
                }
            }

            if (thisAbility.attName == "Shield")
            {
                Vector3 body = Camera.main.transform.position - new Vector3(0, 0.5f, 0);
                transform.root.position = body;
                //print("Shield Spawned");
            }
        }
    }
}
