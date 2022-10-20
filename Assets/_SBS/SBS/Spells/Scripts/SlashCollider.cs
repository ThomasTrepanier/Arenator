using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashCollider : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 30;
        Destroy(gameObject, 0.7f);
	}
}
