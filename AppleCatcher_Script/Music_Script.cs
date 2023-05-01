using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Script : MonoBehaviour
{
    protected AudioSource sfx_player;
    [SerializeField] protected AudioClip backgroung_sfx;

    // Start is called before the first frame update
    void Start()
    {
        // Set the music with all the parameters
        sfx_player = gameObject.AddComponent<AudioSource>();
        sfx_player.volume = 0.3f;
        sfx_player.loop = true;
        sfx_player.playOnAwake = false;
        sfx_player.clip = backgroung_sfx;
        sfx_player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopMusic()
    {
        sfx_player.Pause();
    }
}
