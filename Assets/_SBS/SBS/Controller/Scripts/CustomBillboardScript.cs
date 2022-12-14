using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomBillboardScript : MonoBehaviour {

	private Vector3 lookTarget;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		lookTarget = GameObject.FindGameObjectWithTag ("Player").transform.position;

		transform.LookAt (lookTarget);

        //Color change
        if (gameObject.name == "UI_CD")
        {
            Transform parent = transform.parent;

            if (parent != null && parent.GetComponentsInChildren<ParticleSystem>()[0])
            {
                GetComponent<Image>().color = parent.GetComponentsInChildren<ParticleSystem>()[0].main.startColor.color;
            }
        }
	}
}
