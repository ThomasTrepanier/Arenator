using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation_AllerRetour : MonoBehaviour {

	int decompte = 0;

	public int Delai = 100;

	public Vector3 BougeDansLaDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		decompte ++;

		//BOUCLE 1 : Fait avancer l'objet dans la Direction pendant le temps du décompte
		if (decompte < Delai) {
			transform.Translate (BougeDansLaDirection * 0.001f);
			//print ("Le cube se déplace dans une première direction"); 
		}

		//BOUCLE 1 inversée : Fait avancer l'objet dans la Direction inverse pendant le temps du décompte
		else if (decompte +1 < (Delai * 2)) {
			transform.Translate (-BougeDansLaDirection * 0.001f);
			//print ("Le cube se déplace dans la direction inverse");
		}

		//Remet de decompte à 0 pour recommencer à la boucle 1
		else if (decompte < (Delai * 3)) {
			decompte = 0;
			//print ("La boucle recommence"); 
		}

	}
}
