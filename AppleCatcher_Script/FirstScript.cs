using UnityEngine;
using TMPro;

public class FirstScript : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] protected TextMeshPro ref_texte;
    const float SCREEN_XLIMIT = 7.8f;

    protected AudioSource sfx_player;
    [SerializeField] protected AudioClip collect_sfx;

    [SerializeField] protected Animator ref_animator;
    private float newSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Set the sound and the animator
        sfx_player = gameObject.AddComponent<AudioSource>();
        sfx_player.volume = 0.5f;
        sfx_player.loop = false;
        sfx_player.playOnAwake = false;
        sfx_player.clip = collect_sfx;

        ref_animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check the action of the player (right or left arrow) and move the "panier" + affect an angular angles + the animator
        float time = 15f * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < SCREEN_XLIMIT)
        {
            transform.eulerAngles = new Vector3(0f, 0f, -15f);
            transform.Translate(time, 0, 0, Space.World);
            newSpeed = 10f;
            if(newSpeed != time)
            {
                ref_animator.SetTrigger("Forwards");
            }  
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -SCREEN_XLIMIT)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 15f);
            transform.Translate(-time, 0, 0, Space.World);
            newSpeed = -10f;
            if (newSpeed != time)
            {
                ref_animator.SetTrigger("Backward");
            }
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            if (newSpeed != time)
            {
                ref_animator.SetTrigger("Idle");
            }
        }     
    }

    //React to a collision (collision start)
    private void OnCollisionEnter2D(Collision2D col)
    {
        // Check wath kind of object the play had cought
        if (col.gameObject.tag == "Collectible")
        {
            score++;
            Debug.Log("Current Score : " + score);
            ref_texte.SetText("Score : " + score);
            sfx_player.Play();
        }
        else if (col.gameObject.tag == "GoldenCollectible")
        {
            score+=5;
            Debug.Log("Current Score : " + score);
            ref_texte.SetText("Score : " + score);
            sfx_player.Play();
        }
        else if (col.gameObject.tag == "BombCollectible")
        {
            score -= 5;
            Debug.Log("Current Score : " + score);
            ref_texte.SetText("Score : " + score);
            sfx_player.Play();
        }
    }

    // Do -2 point of the player's score if he don't catch it
    public void fail()
    {
        score -= 2;
        ref_texte.SetText("Score : " + score);
    }

    // Save score of the player
    void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
    }
}
