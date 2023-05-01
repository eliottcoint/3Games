using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Paddle_Script : MonoBehaviour
{
    const float SCREEN_XLIMIT = 5.184f;
    protected AudioSource sfx_player_winpoint;
    [SerializeField] protected AudioClip winpoint_sfx;
    [SerializeField] protected Ball b;

    // Start is called before the first frame update
    void Start()
    {
        sfx_player_winpoint = gameObject.AddComponent<AudioSource>();
        b.setSounds(sfx_player_winpoint, winpoint_sfx);
    }

    // Update is called once per frame
    void Update()
    {
        float time = 8f * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < SCREEN_XLIMIT)
        {
            transform.Translate(time, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -SCREEN_XLIMIT)
        {
            transform.Translate(-time, 0, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            sfx_player_winpoint.Play();
            b.ScoreToAdd(100);
        }
    }
}
