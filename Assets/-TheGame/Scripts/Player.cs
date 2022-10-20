using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK;
using UnityEngine.AI;

public class Player : MonoBehaviour {

    private AudioSource audioSource;
    
    public float Health = 100;
    public float Mana = 100;

	public Image HPBar;
	public Image ManaBar;

    public bool inSpawn = true;
    private bool OnShrine = false;
    private GameObject Shrine;

    [SerializeField]
    private VRTK_Pointer TPLeft;
    [SerializeField]
    private VRTK_Pointer TPRight;
    [SerializeField]
    private VRTK_Pointer UIPointerLeft;
    [SerializeField]
    private VRTK_Pointer UIPointerRight;

	public GameObject DeathScreen;

    public GameObject Xplosion;
    public GameObject GO_Heal;

    public AudioClip ButtonClick_Sound;
	public AudioClip TakeWord; 
	public AudioClip TP_Sound;
    public AudioClip Hit_Sound;
	public AudioClip Boom_Sound;
    public AudioClip ShieldDestroy_Sound;
    public AudioClip Death_Clip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        OnShrine = false;
        inSpawn = true;

        GO_Heal.SetActive(false);
        foreach (Canvas canvas in DeathScreen.GetComponentsInChildren<Canvas>())
        {
            canvas.enabled = false;
        }
    }

	void Update () {

        if (Health > 100)
            Health = 100;
        if (Mana > 100)
            Mana = 100;

        ManaRegen();

        if(OnShrine == true)
        {
            HealShrine();
        }

        #region Debug Control
        if (Input.GetKeyDown(KeyCode.M))
        {
            Mana += 25;
        }
        #endregion Debug Control

		if (Camera.main.gameObject.activeInHierarchy && gameObject.GetComponent<VRTK_TransformFollow>().gameObjectToFollow != Camera.main.gameObject)
        {
			gameObject.GetComponent<VRTK_TransformFollow> ().gameObjectToFollow =  Camera.main.gameObject;
        }

	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Shrine")
        {
            Shrine = col.gameObject;
            if(Shrine.GetComponent<ShrineScript>().refill == false)
            {
                OnShrine = true;
                GO_Heal.SetActive(true);
				//print ("Shrine");
            }
        }
        if(col.gameObject.name == "SpawnArea")
        {
            inSpawn = true;

            UIPointerLeft.enabled = true;
            UIPointerRight.enabled = true;
        }

        if (!inSpawn)
        {
            if (col.gameObject.tag == "EnemyBullet")
            {
                Health -= col.gameObject.GetComponent<Bullet>().bulletDamage;
                UpdateUI();
                PlaySound(Hit_Sound, 0.75f);
            }

            if (col.gameObject.tag == "Ennemi")
            {
                Ennemi ennemi = col.gameObject.GetComponent<Ennemi>();
                if (ennemi.isKamikaze)
                {
                    GameObject xplosion = Instantiate(Xplosion, ennemi.currentPos, Quaternion.identity);

                    ennemi.CurrentHP = 0;
                    ennemi.gameObject.GetComponent<AudioSource>().clip = Boom_Sound;
                    ennemi.gameObject.GetComponent<AudioSource>().volume = 0.1f;
                    ennemi.gameObject.GetComponent<AudioSource>().Play();
                    ennemi.SetHealthUI();

                    Health -= ennemi.BulletDamage;
                    UpdateUI();
                    PlaySound(Hit_Sound, 0.75f);

                    col.gameObject.GetComponent<EnemyAIScript>().enabled = false;
                    col.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                    ennemi.enabled = false;
                }
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Shrine")
        {
            GO_Heal.SetActive(false);
            OnShrine = false;
            Shrine = null;
        }
        if (col.gameObject.name == "SpawnArea")
        {
            inSpawn = false;

            UIPointerLeft.Toggle(!enabled);
            UIPointerRight.Toggle(!enabled);
            UIPointerLeft.enabled = false;
            UIPointerRight.enabled = false;
        }
    }

    public void UpdateUI()
    {
        HPBar.fillAmount = Health / 100f;
        ManaBar.fillAmount = Mana / 100f;

        if (Health <= 0)
        {
            GameOver();
        }
    }

    public void PlaySound(AudioClip clip, float Volume)
    {
        audioSource.clip = clip;
        audioSource.volume = Volume;
        audioSource.Play();
    }

    private void GameOver()
    {
        GetComponent<Collider>().enabled = false;
        TPLeft.enabled = false;
        TPRight.enabled = false;

        PlaySound(Death_Clip, 1f);

		inSpawn = true;
		UIPointerLeft.enabled = true;
		UIPointerRight.enabled = true;

        GameManager.instance.StopAllCoroutines();
        GameManager.instance.CancelInvoke("WordSpawn");

        DeathScreen.GetComponentsInChildren<Canvas>()[0].enabled = true;

        global::DeathScreen.instance.SetPos();

        global::DeathScreen.instance.SetVariables();
    }

    private void RestartGame()
    {
        Time.timeScale = 1.0f;
        GameManager.instance.Restart();
    }

    private void ManaRegen()
    {
        if(Mana < 100)
        {
            Mana +=  2 * Time.deltaTime;
            UpdateUI();
        }
    }
    private void HealShrine()
    {
        if(Shrine.GetComponent<ShrineScript>().refill == false)
        {
            GO_Heal.transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
            ShrineScript shrineInst = Shrine.GetComponent<ShrineScript>();

            float HPRegen = 10 * Time.deltaTime;
            float ManaRegen = 20 * Time.deltaTime; ;
            if (Health < 100)
            {
                Health += HPRegen;
                shrineInst.HPBank -= HPRegen;
                UpdateUI();
            }
            if (Mana < 100)
            {
                Mana += ManaRegen;
                shrineInst.ManaBank -= ManaRegen;
                UpdateUI();
            }
        }
        else
        {
            GO_Heal.SetActive(false);
            OnShrine = false;
        }
    }
}
