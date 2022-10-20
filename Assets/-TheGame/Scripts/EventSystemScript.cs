using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemScript : MonoBehaviour {

	public static EventSystemScript instance = null;

	// Use this for initialization
	void Awake () {
		
		if (instance == null)

			instance = this;

		else if (instance != this)

			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}
}
