using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class Ennemi : MonoBehaviour {

	public GameObject Bullet;
	//private GameObject VRTK_SDK_Manager;
	public EnemyAIScript AIScript;
	private Animator anim;

	private GameObject player;
	private Player playerScript;
	private GameObject bulletSpawn;
	private GameObject healthUI;
    private GameObject healthText;
    private Text KPText;
    private GameObject enmText;
	//private GameObject SimulatorRig;
	//private GameObject SteamVrRig;

	public int MaxHp;
    public int CurrentHP;
    public float BulletDamage = 10;
    public float DistanceDeDetection = 20f;
	public float VitesseDeTir = 30f;
	private float DureeDeVieDesBulletsEnnemi = 5f;
    public int MINDROP = 20;
    public int MAXDROP = 30;
    public int roundMultiplier = 1;
	private bool MontrerLesHP = true;
	public bool PeutTirer = true;
	public bool PeutRegarderLeJoueur = true;
    public bool canBeStunned;
    public bool isKamikaze;
    //public bool isGolem = false;
    //public GameObject AncientWord;
	public AudioClip Hit_Sound;
	public AudioClip Shoot_Sound;

	private float simulatorBulletsSpeedMultiplier = 0.75f;
	private bool readyToShoot = true;
	private float HealthWidth;
	private float playerDist;
	private int HeartIconWidth;
	private bool hitOnce = false;
    private bool dotShield = false;
    private float damageShield;
	private bool checkedIfSim = false;
	private Vector3 stayPos;
    public Vector3 currentPos;

	// Use this for initialization
	void Awake () {

		player = GameObject.FindGameObjectWithTag ("Player");
		playerScript = player.GetComponent<Player> ();

		healthUI = transform.Find("_AJOUTE-MOI_DANS_ENNEMI/Canvas/HealthUI").gameObject;
        healthText = transform.Find("_AJOUTE-MOI_DANS_ENNEMI/Canvas/HPText").gameObject;
        KPText = transform.Find("_AJOUTE-MOI_DANS_ENNEMI/Canvas/KPDropped").GetComponent<Text>();
        KPText.gameObject.SetActive(false);
        //heartUI = this.gameObject;
        //print (transform.Find ("Canvas/HeartUI").gameObject.name);

        //VRTK_SDK_Manager = GameObject.Find("[VRTK_SDKManager]");
		AIScript = gameObject.GetComponent<EnemyAIScript> ();
		anim = GetComponent<Animator> ();

		//player = Camera.main.gameObject;
		//print(player.name);
		bulletSpawn = transform.Find("_AJOUTE-MOI_DANS_ENNEMI/bulletSpawn").gameObject;
		enmText = transform.Find("_AJOUTE-MOI_DANS_ENNEMI/Canvas/EnmText").gameObject;
		enmText.SetActive(false);
		//HeartIconWidth = GetComponent<RawImage>().mainTexture.width;

		//SteamVrRig = VRTK_SDK_Manager.transform.Find ("SDKSetups/SteamVR").gameObject;
		//SimulatorRig = VRTK_SDK_Manager.transform.Find ("SDKSetups/Simulator").gameObject;

        //SetStats();
		//player = GameObject.FindGameObjectWithTag("Player");

	}

    public void SetStats()
    {
        DistanceDeDetection = AIScript.ATTACKDISTANCE;
        CurrentHP = MaxHp;
        HealthWidth = healthUI.GetComponent<RectTransform>().rect.width;
        SetHealthUI();
        healthUI.SetActive(MontrerLesHP);

        AIScript.SetStats();
    }
	// Update is called once per frame
	void Update () {

        currentPos = gameObject.transform.position;
        //print(gameObject.name + " " + currentPos);

        /*if (SteamVrRig.activeSelf){
			player = SteamVrRig.transform.Find ("[CameraRig]/Camera (eye)").gameObject;
		}

		if (SimulatorRig.activeSelf){
			player = SimulatorRig.transform.Find ("VRSimulatorCameraRig/Neck").gameObject;
		}*/

        Vector3 playerPos = player.transform.position;

		if (player != null && playerScript.inSpawn == false){

            if (!isKamikaze)
            {
				playerDist = Vector3.Distance(transform.position, playerPos);

                if (playerDist < DistanceDeDetection)
                {
                    /*if(PeutRegarderLeJoueur){
                        transform.LookAt(player.transform);
                    }*/
                    //bulletSpawn.transform.LookAt(player.transform);
					bulletSpawn.transform.LookAt(playerPos);
                    //enmText.SetActive(true);
                    if (readyToShoot)
                    {
                        readyToShoot = false;
                        StartCoroutine("WaitAndFire");
                    }
                }
                else
                {
                    enmText.SetActive(false);
                    //transform.eulerAngles = new Vector3(0,0,0);
                    //justStoppedLooking = true;
                }
            }
		}
	}

	public void Fire1(){
		if(PeutTirer && !playerScript.inSpawn){
			GetComponent<AudioSource>().clip = Shoot_Sound;
			GetComponent<AudioSource>().Play();
			GameObject enemyBulletClone = Instantiate(Bullet,bulletSpawn.transform.position,bulletSpawn.transform.rotation);
			Rigidbody enemyBulletRigidbody = enemyBulletClone.GetComponent<Rigidbody>();
			/*if (SimulatorRig.activeSelf && !checkedIfSim){
				VitesseDeTir *= simulatorBulletsSpeedMultiplier;
				checkedIfSim = true;
			}*/
            enemyBulletClone.GetComponent<Bullet>().bulletDamage = BulletDamage;
            enemyBulletClone.GetComponent<Bullet>().VitesseDuBullet = VitesseDeTir;
			enemyBulletClone.GetComponent<Bullet>().DureeDeVieDuBullet = DureeDeVieDesBulletsEnnemi;
		}
	}

	public IEnumerator WaitAndFire(){
		//Fire1();
		float stateLength = anim.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds (stateLength);
		Fire1();
		readyToShoot = true;
	}

	void OnTriggerEnter (Collider col){
		if(col.gameObject.tag == "Spell"){
            //HUDController.targetsRemaining -= 1; // changes the targets remaining in the HUDController script on the HUD game object.
            Dictionary<string, float> colSpell = col.gameObject.GetComponent<SpellContScript>().thisElement;
            Dictionary<string, float> colAbilityValues = col.gameObject.GetComponent<SpellContScript>().abiValues;
            ScriptableSpell_SpellType colAbility = col.gameObject.GetComponent<SpellContScript>().thisAbility;

            TakeDamage(colSpell["Damage"] + colAbility.Tier * 5);
            SpellEffect(colSpell["Dot"], colSpell["Slow"], colSpell["Stun"], colSpell["Duration"]);

            PlaySound(Hit_Sound, 0.75f);

            if (colAbility.attName == "Missile")
			    Destroy(col.gameObject);

            /*if (colAbility.attName == "Shield")
            {
                dotShield = true;
                damageShield = colSpell["Damage"] + colAbility.Tier * 5;
                StartCoroutine(ShieldDot(damageShield));
            }*/
            //print("Enemy hit");
        }
	}

    private void OnTriggerExit(Collider col)
    {
        /*if(col.gameObject.transform.root.name == "GO_Shield")
        {
            StopCoroutine("ShieldDot");
            dotShield = false;
            damageShield = 0;
            
        }*/
    }

    void TakeDamage(float Damage){
		CurrentHP -= (int) Damage;

		SetHealthUI();
		SetEnemyColor();
		//print("Enemy has "+CurrentHP+"HP left");
	}

    void SpellEffect(float _dot, float _slow, float _stun, float _duration)
    {
        float startTime = Time.time;

        if(_dot > 0)
        {
            StartCoroutine(DotRoutine(_dot, startTime, _duration));
        }

        if(_slow > 0)
        {
            StartCoroutine(SlowRoutine(_slow, _stun, _duration));
        }
        if(_stun > 0)
        {
            if(canBeStunned)
                StartCoroutine(StunRoutine(_stun));
        }
    }

    /*IEnumerator ShieldDot(float damage)
    {
        while (dotShield)
        {
            TakeDamage(damage);
            print("Ouch");
            yield return new WaitForSeconds(1);
        }
    }*/
    IEnumerator DotRoutine(float dot, float _startTime, float duration)
    {
        while(Time.time < _startTime + duration)
        {
            CurrentHP -= (int) dot;
            SetHealthUI();
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator SlowRoutine(float slow, float stunDur, float duration)
    {
        if (canBeStunned)
        {
            yield return new WaitForSeconds(stunDur);
        }
        else
        {
            yield return new WaitForSeconds(0);
        }

        float _startTime = Time.time;
        AIScript.agent.speed = AIScript.Speed - AIScript.Speed * slow;
        AIScript.agent.angularSpeed = AIScript.AngularSpeed - AIScript.AngularSpeed * slow;

        yield return new WaitForSeconds(duration);
        AIScript.agent.speed = AIScript.Speed;
        AIScript.agent.angularSpeed = AIScript.AngularSpeed;
    }

    IEnumerator StunRoutine(float stun)
    {
        AIScript.agent.speed = 0;
        AIScript.agent.angularSpeed = 0;
        AIScript.Stunned = true;
        PeutTirer = false;

        yield return new WaitForSeconds(stun);

        AIScript.agent.speed = AIScript.Speed;
        AIScript.agent.angularSpeed = AIScript.AngularSpeed;
        AIScript.Stunned = false;
        PeutTirer = true;
    }

    public void SetHealthUI (){
		Image healthImage = healthUI.GetComponent<Image> ();
        Text hpText = healthText.GetComponent<Text>();
		healthImage.fillAmount = (float) CurrentHP / MaxHp;
        hpText.text = CurrentHP + "/" + MaxHp;

        if (CurrentHP <= 0 && !hitOnce)
        {
            StopCoroutine("WaitAndFire");
            CurrentHP = 0;
            hitOnce = true;

            DropKP();

            if (!GetComponent<MeshRenderer>())
            {
                transform.Find("default").gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
			#region StatTracker
			if(gameObject.name.Contains("Imp"))
			{
				DeathScreen.NormalKilled++;
			}
			else if(gameObject.name.Contains("Golem"))
			{
				DeathScreen.FatKilled++;
			}
			else if(gameObject.name.Contains("Kamikaze"))
			{
				DeathScreen.KamKilled++;
			}
			else if(gameObject.name.Contains("Warlock"))
			{
				DeathScreen.NecroKilled++;
			}
			#endregion StatTracker

            GetComponent<BoxCollider>().enabled = false;
            AIScript.agent.speed = 0;

            Destroy(gameObject, 2f);
            //print("Enemy destroyed");
        }

        /*RectTransform HeartUIRect = heartUI.GetComponent<RectTransform>();
		RawImage HeartUIRI = heartUI.GetComponent<RawImage>();
		HeartUIRect.sizeDelta = new Vector3 ((HeartUIRI.mainTexture.width * 4) * CurrentHP, HeartUIRect.rect.height);
		HeartUIRI.uvRect = new Rect(0,0,CurrentHP,1);*/
    }

	//NOT USED for this project
	void SetEnemyColor(){
		Renderer rend = GetComponent<Renderer>();
		float CurrentHPf = (float) CurrentHP;
		float HPf = (float) MaxHp;
		rend.material.color = new Color(1,1f-((HPf-CurrentHPf)/HPf),1f-((HPf-CurrentHPf)/HPf));
		//print("Current HP "+CurrentHPf);
		//print("Max HP "+MaxHPf);
		//print(((MaxHPf-CurrentHPf)/MaxHPf));
	}

    void DropKP()
    {
        int round = GameManager.CurrentRound;
        int KPDropped = Random.Range(MINDROP, MAXDROP) + round * roundMultiplier;
        KnowledgePts.KLPts += KPDropped;
		DeathScreen.KPGained += KPDropped;
        KnowledgePts.UpdateUI();

        KPText.gameObject.SetActive(true);
		//KPText.gameObject.GetComponent<Animation> ().Play ("KPDrop");
        healthText.gameObject.SetActive(false);
        healthUI.gameObject.SetActive(false);
        transform.Find("_AJOUTE-MOI_DANS_ENNEMI/Canvas/HealthBG").gameObject.SetActive(false);

        KPText.GetComponent<Text>().text = "+ " + KPDropped + " KP";

        //StartCoroutine("StartAnimation");
        //print(KPText.GetComponent<Text>().text);
    }

    /*private IEnumerator StartAnimation()
    {
        while (KPText.gameObject.activeSelf)
        {
            print("lol");
            KPText.transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
            KPText.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }*/

    void PlaySound(AudioClip clip, float Volume)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = Volume;
        audio.clip = clip;
        audio.Play();
    }
}
