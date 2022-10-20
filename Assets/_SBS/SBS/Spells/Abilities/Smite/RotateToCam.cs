using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCam : MonoBehaviour {

	void Awake()
    {
        ParticleSystem.MainModule psMain = gameObject.GetComponent<ParticleSystem>().main;
        psMain.startRotation = Camera.main.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
		print(gameObject);
    }

}
