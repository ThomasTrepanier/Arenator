using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation_Complexe : MonoBehaviour {

	int decompte = 0;

	public int Delai = 100;

	public Vector3 Direction1;
	public Vector3 Direction2;
	public Vector3 Direction3;
	public Vector3 Direction4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Augmente le decompte de 1 à chaque frame
		decompte ++;

		//BOUCLE 1 : Fait avancer l'objet dans la Direction1 pendant le temps du décompte
		if (decompte < Delai) {
			transform.Translate (Direction1 * 0.001f);
			//print ("Le cube se déplace dans une première direction"); 
		}

		//BOUCLE 2 : Fait avancer l'objet dans la Diretion2 pendant le temps du décompte
		else if (decompte +1 < (Delai * 2)) {
			transform.Translate (Direction2 * 0.001f);
			//print ("Le cube se déplace dans une deuxième direction");
		}

		//BOUCLE 3 : Fait avancer l'objet dans la Diretion3 pendant le temps du décompte
		else if (decompte +1 < (Delai * 3)) {
			transform.Translate (Direction3 * 0.001f);
			//print ("Le cube se déplace dans une troisième direction");
		}

		//BOUCLE 4 : Fait avancer l'objet dans la Diretion4 pendant le temps du décompte
		else if (decompte +1 < (Delai * 4)) {
			transform.Translate (Direction4 * 0.001f);
			//print ("Le cube se déplace dans une troisième direction");
		}

		//Remet de decompte à 0 pour recommencer à la boucle 1
		else if (decompte < (Delai * 5)) {
			decompte = 0;
			//print ("La boucle recommence"); 
		}
	}
}
