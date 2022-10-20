using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Dictionary<float, string> HighScore;
    //public Dictionary<float, string> InitHS;

    public string Menu_Scene;
    public string Game_Scene;
    
    public static GameManager instance = null;
    public AudioSource audioSource;

    public static int CurrentRound = 1;
    [SerializeField]
    private int maxEnemy;
    [SerializeField]
    private int enemyCount = 0;
    private bool allSpawned;

    [Space(10)]
    [SerializeField]
    private int MINDELAY = 1;
    [SerializeField]
    private int MAXDELAY = 10;
    [SerializeField]
    private int MINSPAWN = 1;
    [SerializeField]
    private int MAXSPAWN = 5;

    [Space(10)]
    public GameObject RoundText;
    private bool roundTextDropping = false;
    private float initY;
    public GameObject AncientWord;
    public GameObject Pedestral;

    public GameObject[] SpawnPoints;
    public GameObject[] EnemiesList;

	[Space(3)]
	[Header("Sounds")]
	public AudioClip ButtonClick_Sound;
	public AudioClip CraftSpell_Sound;
    public AudioClip StartRound_Sound;
    public AudioClip EndCDClip;

    [Space(3)]
    [Header("Musics")]
    public AudioSource MusicAudio;

    // Use this for initialization
    void Awake()
    {
        if (HighScore == null)
        {
            HighScore = new Dictionary<float, string>();

            for (int i = 0; i < 10; i++)
            {
                if(PlayerPrefs.HasKey("HS" + i))
                {
                    float Score = PlayerPrefs.GetFloat("HS" + i);
                    string Init = PlayerPrefs.GetString("HSInit" + i);
                    HighScore.Add(Score, Init);
                }
            }
        }
        /*if(InitHS == null)
        {
            InitHS = new Dictionary<float, string>();
        }*/

        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (Menu_Scene == null)
            Menu_Scene = "Menu";
        if (Game_Scene == null)
            Game_Scene = "Level_1";

        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        /*SpawnPoints = new GameObject[GameObject.FindGameObjectsWithTag("SpawnPoint").Length];

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SpawnPoint").Length; i++)
        {
            SpawnPoints[i] = GameObject.FindGameObjectsWithTag("SpawnPoint")[i];
        }
        RoundText = GameObject.Find("[VRTK_Scripts]/PlayerHUDManager/RoundText").gameObject;
        Pedestral = GameObject.Find("Pedestral_SpawnPoint");

        CurrentRound = 1;
        StartRound();*/

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundTextDropping == true)
        {
            //RoundText.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

            float textY = Mathf.Lerp(RoundText.transform.localPosition.y, initY - 0.6f, 0.01f);
            RoundText.transform.localPosition = new Vector3(RoundText.transform.localPosition.x, textY, RoundText.transform.localPosition.z);

            if(RoundText.transform.localPosition.y - (initY - 0.6f) <= 0.01f)
            {
                RoundText.transform.localPosition = new Vector3(RoundText.transform.localPosition.x, initY - 0.6f, RoundText.transform.localPosition.z);
                roundTextDropping = false;
            }

            /*while(RoundText.transform.position.y >= (initY - 0.6f))
            {
                RoundText.transform.localPosition -= new Vector3(0, 0.0001f, 0);
            }
            if(RoundText.transform.position.y == (initY - 0.6f))
                roundTextDropping = false;*/
        }
        #region Debug Control
        if (Input.GetKeyUp(KeyCode.O))
        {
            GameObject SpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)];
            GameObject enemy = EnemiesList[Random.Range(0, EnemiesList.Length - 1)];
            Instantiate(enemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            CurrentRound++;
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            Instantiate(AncientWord);
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            NextRound();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            ResetHS();
        }
        #endregion Debug Control
        if (allSpawned == true)
        {
            //Debug.Log(GameObject.FindGameObjectsWithTag("Ennemi").Length);
            if (GameObject.FindGameObjectsWithTag("Ennemi").Length <= 0)
                NextRound();
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(Game_Scene);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    void ResetHS()
    {
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey("HS" + i))
            {
                PlayerPrefs.DeleteKey("HS" + i);
                PlayerPrefs.DeleteKey("HSInit" + i);
            }
        }
    }

    public void ReturnToMenu()
    {
        //PlayerPrefs.SetFloat("HighScore", DeathScreen.instance.Score);
        //PlayerPrefs.SetString("HSInit", DeathScreen.instance.HSInitials.text);

        HighScore.Add(DeathScreen.instance.Score, DeathScreen.instance.HSInitials.text);

        //InitHS.Add(PlayerPrefs.GetFloat("HighScore"), PlayerPrefs.GetString("HSInit"));

        //Debug.Log("New HighScore : " + InitHS[PlayerPrefs.GetFloat("HighScore")] + " : " + HighScore[PlayerPrefs.GetString("HSInit")]);

        SceneManager.LoadScene(Menu_Scene);
		Time.timeScale = 1f;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
		ResetVariables ();
        if(SceneManager.GetActiveScene().name == Game_Scene)
        {
            MusicAudio.Play();
            SpawnPoints = new GameObject[GameObject.FindGameObjectsWithTag("SpawnPoint").Length];

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("SpawnPoint").Length; i++)
            {
                SpawnPoints[i] = GameObject.FindGameObjectsWithTag("SpawnPoint")[i];
            }
            RoundText = GameObject.Find("[VRTK_Scripts]/PlayerHUDManager/RoundText").gameObject;
            Pedestral = GameObject.Find("Pedestral_SpawnPoint");

			StartRound();
        }

        /*if (scene.name == Game_Scene)
        {
            SpawnPoints = new GameObject[GameObject.FindGameObjectsWithTag("SpawnPoint").Length];

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("SpawnPoint").Length; i++)
            {
                SpawnPoints[i] = GameObject.FindGameObjectsWithTag("SpawnPoint")[i];
            }
            RoundText = GameObject.Find("[VRTK_Scripts]/PlayerHUDManager/RoundText").gameObject;
            Pedestral = GameObject.Find("Pedestral_SpawnPoint");

            CurrentRound = 1;
            StartRound();
        }*/
    }
	void ResetVariables(){
		CurrentRound = 1;
		enemyCount = 0;
		allSpawned = false;
		RoundText = null;
		Pedestral = null;
	}

    public void StartRound()
    {
		PlaySound (StartRound_Sound, 0.5f);
        RoundText.GetComponent<Text>().text = "Round : " + CurrentRound;
        RoundText.transform.localPosition += new Vector3(0, 0.6f, 0);
        initY = RoundText.transform.localPosition.y;
        roundTextDropping = true;

        maxEnemy = 10 + CurrentRound * 10;
        enemyCount = 0;

        if(GameManager.instance != null)
            StartCoroutine("RoundSpawn");

        float delay = Random.Range(30, 60);
        Invoke("WordSpawn", delay);
    }
    public IEnumerator RoundSpawn()
    {
        while(enemyCount < maxEnemy)
        {
            float spawnDelay = Random.Range(MINDELAY, MAXDELAY);
            float spawnAmount = Random.Range(MINSPAWN, MAXSPAWN);
            yield return new WaitForSeconds(spawnDelay);

            for (int i = 0; i < spawnAmount; i++)
            {
                if(enemyCount < maxEnemy)
                {
                    GameObject SpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
                    GameObject enemy = EnemiesList[Random.Range(0, EnemiesList.Length)];
                    GameObject enemyInst = Instantiate(enemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

                    if(enemyInst.name == EnemiesList[0].name + "(Clone)")
                    {
                        SpawnEnemy(enemyInst);
                    }
                    else if (enemyInst.name == EnemiesList[1].name + "(Clone)")
                    {
                        SpawnGolem(enemyInst);
                    }
                    else if (enemyInst.name == EnemiesList[2].name + "(Clone)")
                    {
                        SpawnKamikaze(enemyInst);
                    }

                    enemyCount++;
                }
                else
                {
                    continue;
                }  
            }
        }
        allSpawned = true;
    }
    public void NextRound()
    {
        allSpawned = false;

        //PlaySound(NextRoundClip, 0.9f);

        CurrentRound++;
        enemyCount = 0;

		StartRound();
    }

    void SpawnEnemy(GameObject _BasicEnemy)
    {
        _BasicEnemy.GetComponent<NavMeshAgent>().speed += CurrentRound / 2;
        _BasicEnemy.GetComponent<NavMeshAgent>().acceleration += CurrentRound / 2;

        Ennemi enemyScript = _BasicEnemy.GetComponent<Ennemi>();
        enemyScript.BulletDamage += CurrentRound;
        enemyScript.MaxHp += CurrentRound * 2;
        enemyScript.SetStats();
    }

    void SpawnGolem(GameObject _Golem)
    {
        _Golem.GetComponent<NavMeshAgent>().speed += CurrentRound / 2;
        _Golem.GetComponent<NavMeshAgent>().acceleration += CurrentRound / 2;

        Ennemi enemyScript = _Golem.GetComponent<Ennemi>();
        enemyScript.BulletDamage += CurrentRound;
        enemyScript.MaxHp += CurrentRound * 2;
        enemyScript.SetStats();
    }
    
    void SpawnKamikaze(GameObject _Kamikaze)
    {
        _Kamikaze.GetComponent<NavMeshAgent>().speed += CurrentRound / 2;
        _Kamikaze.GetComponent<NavMeshAgent>().acceleration += CurrentRound / 2;

        Ennemi enemyScript = _Kamikaze.GetComponent<Ennemi>();
        enemyScript.BulletDamage += CurrentRound;
        enemyScript.MaxHp += CurrentRound * 2;
        enemyScript.SetStats();
    }

    public void WordSpawn()
    {
        PedestralScript pedScript = Pedestral.GetComponent<PedestralScript>();
        if(pedScript.PickedUp == true)
        {
            Instantiate(AncientWord, Pedestral.transform.position, Pedestral.transform.rotation);
            pedScript.PickedUp = false;
        }
    }

    public void PlaySound(AudioClip clip, float Volume)
    {
        audioSource.clip = clip;
        audioSource.volume = Volume;
        audioSource.Play();
    }
}
