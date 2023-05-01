using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rules_Script : MonoBehaviour
{
    protected AudioSource sfx_leave;
    [SerializeField] protected AudioClip leave_sfx;
    [SerializeField] protected Fader f;
    [SerializeField] protected Music_Script ms;

    // Start is called before the first frame update
    void Start()
    {
        sfx_leave = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Do certain interraction du to the keyboard
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Play();
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

    // Load the Game scene via the button (the "title" is the button)
    public void Play()
    {
        StartCoroutine(LoadScene_Game("Game"));
    }
}
