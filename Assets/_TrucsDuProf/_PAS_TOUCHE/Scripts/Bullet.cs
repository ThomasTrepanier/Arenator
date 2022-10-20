using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	//private float speed = 0.1f;
	public float DureeDeVieDuBullet = 1.0f;
    public float bulletDamage;
	public string Origine;
    public GameObject ImpactSmoke;

	public float VitesseDuBullet = 10;
	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody>();
		//rb.AddForce(transform.forward * speed);
		rb.velocity = transform.forward * VitesseDuBullet;
		Destroy (gameObject, DureeDeVieDuBullet);
	}
	void OnTriggerEnter(Collider col){ //void OnCollisionEnter(Collision col){
		if(col.gameObject.tag != "Shrine" && col.gameObject.tag != "SpellSlot") {
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Shield")
        {
            Instantiate(ImpactSmoke, transform.position, transform.rotation);
            Destroy(gameObject);
        }
		/*if (col.gameObject.tag != Origine && col.gameObject.tag != "MainCamera" && col.gameObject.tag != "Shrine") { // if (col.gameObject.tag != "Target") {
//			col.gameObject.GetComponent<PlayerHitBox> ().PlayerLoseHP ();
			print ("HIT");
			Destroy (gameObject);
		}*/
	}

	// Update is called once per frame
	void Update () {
		
	}
}
