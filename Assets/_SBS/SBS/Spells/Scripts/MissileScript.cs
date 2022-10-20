using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
            Destroy(gameObject);
    }
}
