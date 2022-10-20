using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonXScript : MonoBehaviour {

    public GameObject craftMat;

	public void XClick()
    {
        if (craftMat.GetComponent<craftElementScript>() != null)
            craftMat.GetComponent<craftElementScript>().craftElement = null;
        if (craftMat.GetComponent<craftTypeScript>() != null)
            craftMat.GetComponent<craftTypeScript>().craftType = null;
        if (craftMat.GetComponent<craftAttributeScript>() != null)
            craftMat.GetComponent<craftAttributeScript>().craftAttribute = null;
    }
}
