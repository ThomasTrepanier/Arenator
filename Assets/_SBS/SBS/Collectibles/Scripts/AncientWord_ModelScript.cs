using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientWord_ModelScript : MonoBehaviour {

    public Transform parent;
    public AncientWordScript parentScript;

	public void Awake () {

        parent = transform.root;
        parentScript = parent.gameObject.GetComponent<AncientWordScript>();

        #region ColorModifier
        if (GetComponent<Renderer>())
        {
            if (parentScript.thisElement != null)
                if(parentScript.thisElement.Tier == 1)
                {
                    GetComponent<Renderer>().material.color = Color.green;
                }
                else if (parentScript.thisElement.Tier == 2)
                {
                    GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    GetComponent<Renderer>().material.color = new Color(1, 0.5f, 0);
                }
            else if (parentScript.thisForm != null)
            {
                if (parentScript.thisForm.Tier == 1)
                {
                    GetComponent<Renderer>().material.color = Color.green;
                }
                else if (parentScript.thisForm.Tier == 2)
                {
                    GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    GetComponent<Renderer>().material.color = new Color(1, 0.5f, 0);
                }
            }
            else
            {
                if (parentScript.thisAttribute.Tier == 1)
                {
                    GetComponent<Renderer>().material.color = Color.green;
                }
                else if (parentScript.thisAttribute.Tier == 2)
                {
                    GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    GetComponent<Renderer>().material.color = new Color(1, 0.5f, 0);
                }
            }
            //print(GetComponent<Renderer>().material.color.g);
        }
        else
        {
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                foreach (Material mat in rend.materials)
                {
                    if (parentScript.thisElement != null)
                        if (parentScript.thisElement.Tier == 1)
                        {
                            mat.color = Color.green;
                        }
                        else if (parentScript.thisElement.Tier == 2)
                        {
                            mat.color = Color.blue;
                        }
                        else
                        {
                            mat.color = new Color(1, 0.5f, 0);
                        }
                    else if (parentScript.thisForm != null)
                    {
                        if (parentScript.thisForm.Tier == 1)
                        {
                            mat.color = Color.green;
                        }
                        else if (parentScript.thisForm.Tier == 2)
                        {
                            mat.color = Color.blue;
                        }
                        else
                        {
                            mat.color = new Color(1, 0.5f, 0);
                        }
                    }
                    else
                    {
                        if (parentScript.thisAttribute.Tier == 1)
                        {
                            mat.color = Color.green;
                        }
                        else if (parentScript.thisAttribute.Tier == 2)
                        {
                            mat.color = Color.blue;
                        }
                        else
                        {
                            mat.color = new Color(1, 0.5f, 0);
                        }
                    }
                }
            }
        }
        #endregion ColorModifier
    }

    private void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
