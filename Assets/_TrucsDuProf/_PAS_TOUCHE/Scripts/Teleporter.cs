using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	//Grâce à ce gameobject, on peut utiliser la position de la plateforme où on souhaite se déplacer
	public GameObject destination;

	// S'il y a une collision ET que le tag de l'objet est Player, on modifie la position du parent (le RIG de caméra) 
	//pour qu'il corresponde à la plateforme visée)

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player"){
			other.gameObject.transform.parent.parent.position = destination.transform.position;
		}
	}
}
