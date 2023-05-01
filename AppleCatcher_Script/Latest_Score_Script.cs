using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Latest_Score_Script : MonoBehaviour
{
    [SerializeField] protected TextMeshPro latestscore;
    private int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Use the score of the player at the latest game + show it
    void OnEnable()
    {
        playerScore = PlayerPrefs.GetInt("score");
        Debug.Log("Latest Score : " + playerScore);
        latestscore.SetText("Latest Score : " + playerScore);
    }
}
