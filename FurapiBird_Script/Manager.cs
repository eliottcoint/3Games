using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{

    //Spawn Manager
    public Transform spawnPipePos;
    public GameObject pipeObj;
    public Sprite[] spritePipes; 

    public float timeAct = 0;
    public float timeBetweenSpawns = 3;

    public float timeActSpeedPipe = 0;
    public float timeBetweenAcc = 30;
    public float heightMaxSpawn = 2;

    public float ratioSpawnSpeed;
    public float speedPipe = 4f;
    public float speedIncrease = 1.20f;

    public float background_speed = 2f;

    public List<PipeObstacle_Script> pipeObsList;

    //Game Manager
    public bool isPlaying;
    public bool isGameStarted;
    public bool isGameOver;

    //Menu UI 
    public GameObject MenuTitle;
    public float exitPosXTitle;
    public float speedTitle;

    //Score UI
    public TextMeshProUGUI scoreText;
    public float score;

    //Sounds
    public AudioClip defeatSound;
    public AudioClip mainSound; 
    public AudioSource sourceSound;

    // Start is called before the first frame update
    void Start()
    {
        timeAct = timeBetweenSpawns;
        timeActSpeedPipe = timeBetweenAcc;
        ratioSpawnSpeed = timeBetweenSpawns / speedPipe;

        sourceSound.clip = mainSound;
        sourceSound.loop = true;
        sourceSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying == false && Input.GetKeyDown(KeyCode.Space) && isGameStarted == false)
        {
            isPlaying = true;
            isGameStarted = true;
            StartCoroutine(RemoveMenuUI());

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadScene_Game("Menu"));
        }

        if (isPlaying == true && isGameOver == true)
        {
            isPlaying = false;
            StopGame();

        }

        //Prevents the code from executing if the game is not started
        if (isPlaying == false)
        {
            return;
        }


        //Part that executes if the game is playing
        if (timeAct > 0)
        {
            timeAct -= Time.deltaTime;
        }
        else
        {
            SpawnPipe(); 
            timeAct = timeBetweenSpawns;
        }

        if (timeActSpeedPipe > 0)
        {
            timeActSpeedPipe -= Time.deltaTime;
        }
        else
        {
            AcceleratePipe();
            timeActSpeedPipe = timeBetweenAcc;
        }
    }

    public void SpawnPipe()
    {
        Vector3 spawnPos = new Vector3(spawnPipePos.position.x, spawnPipePos.position.y + Random.Range(-heightMaxSpawn, heightMaxSpawn), spawnPipePos.position.z) ;
        GameObject pipeTmp = Instantiate(pipeObj, spawnPos, Quaternion.identity);
        //Asign reference of pipe's manager to this manager 
        PipeObstacle_Script pipeScript = pipeTmp.GetComponent<PipeObstacle_Script>();
        pipeScript.manager = this;
        pipeObsList.Add(pipeScript);

        //Choose a random sprite for every pipe
        foreach(SpriteRenderer spriteRdr in pipeTmp.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRdr.sprite = spritePipes[Random.Range(0, spritePipes.Length)];
        }
    }
    public void StopGame()
    {
        speedPipe = 0;
        background_speed = 0;

        StartCoroutine(ReturnToMenu());
    }
    public IEnumerator ReturnToMenu()
    {
        //Play Sound
        sourceSound.clip = defeatSound;
        sourceSound.loop = false; 
        sourceSound.Play();
        //Wait until sourceSound is done playing
        while(sourceSound.isPlaying)
        {
            yield return null;
        }
        //Load Scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("FurapiBird");
        while(asyncLoad.isDone ==false)
        {
            yield return null;
        }
    }
    IEnumerator LoadScene_Game(string game)
    {
        //Play Sound
        sourceSound.clip = defeatSound;
        sourceSound.loop = false;
        sourceSound.Play();

        //Wait a tiny bit
        yield return new WaitForSeconds(0.5f);

        //Load game scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(game);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void AcceleratePipe()
    {
        speedPipe *= speedIncrease;
        timeBetweenSpawns /= speedIncrease;
        foreach(PipeObstacle_Script pipeScriptTmp in pipeObsList)
        {
            pipeScriptTmp.ChangeSpeed();
        }
    }

    public void RemoveFromPipeList(PipeObstacle_Script pipeScriptToRemove)
    {
        PipeObstacle_Script pipeScriptTmp = pipeObsList.Find(obj => obj == pipeScriptToRemove);
        pipeObsList.Remove(pipeScriptTmp);
        Destroy(pipeScriptTmp.gameObject);
    }

    public IEnumerator RemoveMenuUI()
    {
        while(MenuTitle.transform.position.x < exitPosXTitle)
        {
            MenuTitle.transform.Translate(Vector2.right * Time.deltaTime * speedTitle);
            yield return null;
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        //Update self score 
        this.score += scoreToAdd;
        //Update text ui
        scoreText.text = this.score.ToString();
    }
}
