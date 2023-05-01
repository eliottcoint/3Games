using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Script : MonoBehaviour
{
    public Image fader_renderer;
    public GameObject ecran;

    protected AudioSource sfx_spacesound;
    [SerializeField] public AudioClip spaceSound_sfx;
    protected AudioSource sfx_selectedGame;
    [SerializeField] public AudioClip selectedGame_sfx;

    protected bool hasLeft = false;
    protected float current_alpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set sounds
        sfx_spacesound = gameObject.AddComponent<AudioSource>();
        setSounds(sfx_spacesound, spaceSound_sfx);
        sfx_spacesound.loop = true;
        sfx_selectedGame = gameObject.AddComponent<AudioSource>();
        setSounds(sfx_selectedGame, selectedGame_sfx);
        sfx_selectedGame.loop = false;
        sfx_spacesound.Play();


        ecran.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // You can load a game also by a letter
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAppleCatcher();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayBrickBreaker();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayFurapiBird();
        }
    }

    IEnumerator LoadScene_Game(string sceneToLoad)
    {
        sfx_selectedGame.Play();
        ecran.SetActive(true);

        //Wait for that sound to end (with a margin)
        yield return new WaitForSeconds(0.8f);

        //Fade the white fader into "existence"
        while (current_alpha < 1)
        {
            current_alpha += Time.deltaTime / 2;
            fader_renderer.color = new Color(1, 1, 1, current_alpha);
            yield return null;
        }

        //Wait a tiny bit
        yield return new WaitForSeconds(0.5f);

        //Load game scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // Set all the sound with this function
    public void setSounds(AudioSource asource, AudioClip aclip)
    {
        asource.volume = 0.5f;
        asource.playOnAwake = false;
        asource.clip = aclip;
    }

    // Load the different scene
    public void PlayAppleCatcher()
    {
        StartCoroutine(LoadScene_Game("Title"));
    }

    public void PlayBrickBreaker()
    {
        StartCoroutine(LoadScene_Game("BrickBreaker"));
    }

    public void PlayFurapiBird()
    {
        StartCoroutine(LoadScene_Game("FurapiBird"));
    }
}
