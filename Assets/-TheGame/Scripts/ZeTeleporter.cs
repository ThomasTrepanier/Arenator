using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ZeTeleporter : MonoBehaviour {

	//Grâce à ce gameobject, on peut utiliser la position de la plateforme où on souhaite se déplacer
	public Transform destination;

	public GameObject CameraRig;
	public GameObject SimRig;

	// S'il y a une collision ET que le tag de l'objet est Player, on modifie la position du parent (le RIG de caméra) 
	//pour qu'il corresponde à la plateforme visée)

	void OnTriggerEnter(Collider other) {
        foreach (NavMeshAgent agent in other.transform.root.GetComponentsInChildren<NavMeshAgent>())
        {
            agent.isStopped = true;
            agent.Warp(destination.position);

            if (agent.isOnOffMeshLink)
            {
                agent.CompleteOffMeshLink();
            }
        }

		if (other.gameObject.tag == "Player") 
		{
			other.gameObject.GetComponent<Player> ().PlaySound (other.gameObject.GetComponent<Player> ().TP_Sound, 0.5f);
		}

        if(other.gameObject.tag == "Ennemi")
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            agent.isStopped = false;
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
	}
}
