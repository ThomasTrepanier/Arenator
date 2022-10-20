using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.AI;

public class EnemyAIScript : MonoBehaviour {

	private Animator anim;
	public NavMeshAgent agent;
	private Ennemi EnnemiScript;

	[Header("VRTK Setup")]
	public VRTK_SDKManager VRTKManager; 
	public VRTK_SDKSetup ViveSetup;
	public VRTK_SDKSetup SimSetup;

	public Transform player;

    [Header("Agent Variables")]
    public bool Stunned = false;

	public float ATTACKDISTANCE = 10f;
    public float Speed;
    public float AngularSpeed;
	[SerializeField]
	private float TURNSPEED = 2f;
    public bool isWander;

    private GameObject[] WanderPoints;
	void Awake () {
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();

        WanderPoints = new GameObject[GameObject.FindGameObjectsWithTag("SpawnPoint").Length];

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SpawnPoint").Length; i++)
        {
            WanderPoints[i] = GameObject.FindGameObjectsWithTag("SpawnPoint")[i];
        }

        InvokeRepeating("MoveToPlayer", 0, 0.05f);
    }
	
    public void SetStats()
    {
        Speed = agent.speed;
        AngularSpeed = agent.angularSpeed;
    }
	// Update is called once per frame
	void Update () {

		EnnemiScript = gameObject.GetComponent<Ennemi> ();
		//Sets player
		if(Camera.main != null && player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

    void MoveToPlayer()
    {
        //AI
        if (player != null && agent.isOnNavMesh)
        {
            Player playerScript = player.GetComponent<Player>();
            //print(playerScript);
            if (!Stunned)
            {
                if (playerScript.inSpawn == false)
                {
                    CancelInvoke("Wander");
                    isWander = false;

                    if (!EnnemiScript.isKamikaze)
                    {
                        //Sets destination
                        if (Vector3.Distance(player.position, transform.position) > ATTACKDISTANCE)
                        {
                            agent.isStopped = false;
                            agent.SetDestination(player.position);

                            anim.SetBool("isIdle", false);
                            anim.SetBool("isWalking", true);
                            anim.SetBool("isAttacking", false);
                        }
                        //Attacking
                        else if (Vector3.Distance(player.position, transform.position) <= ATTACKDISTANCE)
                        {
                            Vector3 direction = player.position - transform.position;
                            direction.y = 0f;
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), TURNSPEED * Time.deltaTime);

                            agent.isStopped = true;

                            anim.SetBool("isIdle", false);
                            anim.SetBool("isWalking", false);
                            anim.SetBool("isAttacking", true);
                        }
                    }
                    else
                    {
                        agent.isStopped = false;
                        agent.SetDestination(player.position);

                        anim.SetBool("isIdle", false);
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isAttacking", false);
                    }
                }
                else
                {
                    if (!isWander)
                    {
                        StopCoroutine(EnnemiScript.WaitAndFire());
                        Wander();
                        isWander = true;
                    }
                }
            }
        }
    }
    void Wander()
    {
        if (agent.isOnNavMesh)
        {
            Vector3 wanderNext = WanderPoints[Random.Range(0, WanderPoints.Length)].transform.position;
            agent.isStopped = false;
            agent.SetDestination(wanderNext);
        }

        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);

        float delay = Random.Range(5, 30);
        Invoke("Wander", delay);
    }
}
