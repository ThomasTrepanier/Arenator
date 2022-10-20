using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingScript : MonoBehaviour {

    [Range(-5, 5)]
    public float a = 5;
    [Range(-1, 1)]
    public float b = 0.02f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.Translate(0, Mathf.Sin(Time.time * a) * b, 0);
	}
    void FixedUpdate(){
        gameObject.transform.Translate(0, Mathf.Sin(Time.time * a) * b, 0);
    }
}
