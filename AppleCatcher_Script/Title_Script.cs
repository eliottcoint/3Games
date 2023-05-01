using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Script : MonoBehaviour
{
    protected AudioSource sfx_leave;
    [SerializeField] protected AudioClip leave_sfx;
    [SerializeField] protected Fader f;
    protected bool hasLeft = false;
    [SerializeField] protected Music_Script ms;

    // Start is called before the first frame update
    void Start()
    {
        sfx_leave = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // All the commands with the keyboard
        if (Input.GetKeyDown(KeyCode.Escape) && !hasLeft)
        {
            hasLeft = true;
            StartCoroutine(LoadScene_Game("Menu"));
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !hasLeft)
        {
            hasLeft = true;
            StartCoroutine(LoadScene_Game("Game"));
        }
        else if (Input.GetKeyDown(KeyCode.R) && !hasLeft)
        {
            hasLeft = true;
            StartCoroutine(LoadScene_Game("RulesAC"));
        }
    }

    IEnumerator LoadScene_Game(string game)
    {
        //Stop music and play the exit sound
        ms.stopMusic();
        sfx_leave.clip = leave_sfx;
        sfx_leave.loop = false;
        sfx_leave.Play();

        //Wait for that sound to end (with a margin)
        yield return new WaitForSeconds(0.8f);

        f.RequestSceneOut();
        yield return new WaitForSeconds(Fader.DURATION);

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
}
