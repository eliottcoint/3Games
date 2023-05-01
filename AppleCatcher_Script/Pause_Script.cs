using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause_Script : MonoBehaviour
{
    private bool pause = false;
    [SerializeField] protected TextMeshPro pauseinstruction_text;
    [SerializeField] protected TextMeshPro pause_text;

    // Start is called before the first frame update
    void Start()
    {
        pause_text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        isPause();
    }

    public void isPause()
    {
        if (Input.GetKeyDown(KeyCode.P) && !pause)
        {
            // Set the timeScale to 0 for the pause mode
            Time.timeScale = 0;
            pause = true;
            pause_text.enabled = true;
            // Change the text to tellswhat you need to do to play again
            pauseinstruction_text.SetText("Press \"P\" to resume");
        }
        else if (Input.GetKeyDown(KeyCode.P) && pause)
        {
            // Disable the pause mode
            Time.timeScale = 1;
            pause = false;
            pause_text.enabled = false;
            // Set the text to indicate what you need to do to pause
            pauseinstruction_text.SetText("Press \"P\" to pause");
        }
    }
}
