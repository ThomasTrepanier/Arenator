using UnityEngine;
using System.Collections;
//Il faut la librairie UnityEngine.SceneManagement pour charger/décharger des niveaux
using UnityEngine.SceneManagement;

public class ChangerNiveau : MonoBehaviour {

	//Cette valeur permet de modifier à partir de l'éditeur quel niveau charger
	public int quelNiveauCharger;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	// S'il y a une collision ET que le tag de l'objet est Player, charger un niveau (selon le # de niveau)

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if(quelNiveauCharger ==1){
				//SceneManager.LoadScene ("Niveau1");
			}
			if(quelNiveauCharger ==2){
				//SceneManager.LoadScene ("Niveau2");
			}
		}
	}
}
