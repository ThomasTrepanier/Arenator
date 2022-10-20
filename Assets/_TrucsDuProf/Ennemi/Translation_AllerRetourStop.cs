using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation_AllerRetourStop : MonoBehaviour {

	int decompte = 0;

	public int Delai = 100;

	public Vector3 BougeDansLaDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		decompte ++;

		//Délai avant le départ de la boucle
		if (decompte < Delai) {
			//print ("L'objet ne bouge pas"); 
		}

		//BOUCLE 1 : Fait avancer l'objet dans la Direction pendant le temps du décompte
		else if (decompte +1 < Delai * 2) {
			transform.Translate (BougeDansLaDirection * 0.001f);
			//print ("L'objet se déplace dans une première direction"); 
		}

		//BOUCLE 1 inversée : Fait avancer l'objet dans la Direction inverse pendant le temps du décompte
		else if (decompte+1 < (Delai * 3)) {
			transform.Translate (-BougeDansLaDirection * 0.001f);
			//print ("L'objet se déplace dans la direction inverse");
		}

		//Remet de decompte à 0 pour recommencer à la boucle 1
		else if (decompte < (Delai * 4)) {
			decompte = 0;
			//print ("La boucle recommence"); 
		}

	}
}
