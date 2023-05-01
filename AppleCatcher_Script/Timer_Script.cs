using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer_Script : MonoBehaviour
{
    [SerializeField] protected TextMeshPro timer_texte;
    [SerializeField] protected float timeRemaining = 120;
    private bool timerIsRunning = true;

    [SerializeField] protected Fader f;
    [SerializeField] protected TextMeshPro end_text;

    // Start is called before the first frame update
    void Start()
    {
        end_text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            // Test if there is time remainning
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                end_text.enabled = true;
                // Set a slowmotion to the game
                Time.timeScale = 0.25f;
                StartCoroutine(LoadScene_Game());
            }
        }
    }

    // Display the time with the number of second left
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
        timer_texte.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    IEnumerator LoadScene_Game()
    {
        // Set the fader like the Title scene
        f.RequestSceneOut();
        yield return new WaitForSeconds(Fader.DURATION/3);

        //Wait a tiny bit
        yield return new WaitForSeconds(0.5f/4);

        Time.timeScale = 1f;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Title");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
