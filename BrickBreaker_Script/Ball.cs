using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] protected float force = 5f;
    private bool wait = false;
    [SerializeField] protected GameObject paddle;
    private int score = 0;
    [SerializeField] protected TextMeshPro text_Score;
    private float Screen_Y = 5f;
    [SerializeField] private Master_Script ms;
    [SerializeField] private Coin_Master_Script cms;
    private float SPEED_THRESHOLD_Y = 1f;
    private float SPEED_THRESHOLD_X = 1f;
    private bool isStatic = true;
    private bool isBeginning = true;

    //Audio Source and Clip for the ball when it collide
    protected AudioSource sfx_player_wall;
    [SerializeField] protected AudioClip wall_sfx;
    protected AudioSource sfx_player_paddle_brick;
    [SerializeField] protected AudioClip paddle_brick_sfx;
    protected AudioSource sfx_player_intro;
    [SerializeField] protected AudioClip intro_sfx;
    protected AudioSource sfx_player_death;
    [SerializeField] protected AudioClip death_sfx;
    protected AudioSource sfx_player_gameover;
    [SerializeField] protected AudioClip gameover_sfx;
    protected AudioSource sfx_player_winpoint;
    [SerializeField] protected AudioClip winpoint_sfx;

    // Start is called before the first frame update
    void Start()
    {
        //Set the Audio with their components and clip
        sfx_player_wall = gameObject.AddComponent<AudioSource>();
        setSounds(sfx_player_wall, wall_sfx);
        sfx_player_paddle_brick = gameObject.AddComponent<AudioSource>();
        setSounds(sfx_player_paddle_brick, paddle_brick_sfx);
        sfx_player_intro = gameObject.AddComponent<AudioSource>();
        setSounds(sfx_player_intro, intro_sfx);
        sfx_player_death = gameObject.AddComponent<AudioSource>();
        setSounds(sfx_player_death, death_sfx);
        sfx_player_gameover = gameObject.AddComponent<AudioSource>();
        setSounds(sfx_player_gameover, gameover_sfx);
        sfx_player_winpoint = gameObject.AddComponent<AudioSource>();
        setSounds(sfx_player_winpoint, winpoint_sfx);

        sfx_player_intro.Play();
        rb = GetComponent<Rigidbody2D>();
        wait = true;
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -Screen_Y && wait == false)
        {
            wait = true;
            StartCoroutine(waiter());
        }

        if(Mathf.Abs(rb.velocity.y) < SPEED_THRESHOLD_Y && isStatic == true)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, -SPEED_THRESHOLD_Y);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, SPEED_THRESHOLD_Y);
            }
        }
        if (Mathf.Abs(rb.velocity.x) < SPEED_THRESHOLD_X && isStatic == true)
        {
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(-SPEED_THRESHOLD_X, rb.velocity.y );
            }
            else
            {
                rb.velocity = new Vector2(SPEED_THRESHOLD_X, rb.velocity.y );
            }
        }

    }

    public void respawn()
    {
        rb.AddForce(new Vector3(0, -force, 0), ForceMode2D.Impulse);
        wait = false;
    }

    public void posNull()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector3(0, -2, 0);
    }

    public IEnumerator waiter()
    {
        isStatic = false;
        posNull();

        if (isBeginning == false)
        {
            if (score <= 500)
            {
                score = 0;
                sfx_player_gameover.Play();
            }
            else
            {
                score -= 500;
                sfx_player_death.Play();
            }
        }
        
        Debug.Log(score);
        text_Score.SetText("Score : " + score);
        yield return new WaitForSeconds(2);
        Debug.Log("Waiter");
        respawn();
        isStatic = true;
        isBeginning = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Paddle")
        {
            float Diff_X = 0;
            Diff_X = (paddle.transform.position.x - transform.position.x) * 50;
            rb.AddForce(new Vector3(-Diff_X, 0, 0));
            sfx_player_paddle_brick.Play();
        }

        if (collision.gameObject.tag == "Brick")
        {
            sfx_player_winpoint.Play();
            ScoreToAdd(50);
            ms.ReportBrickDeath();
            sfx_player_paddle_brick.Play();

            int spawncoin = Mathf.RoundToInt(Random.Range(0f, 10f));
            if (spawncoin == 2)
            {
                cms.generateCoin();
            }
        }

        if(collision.gameObject.tag == "Wall")
        {
            sfx_player_wall.Play();
        }
    }

    public void setSounds(AudioSource asource, AudioClip aclip)
    {
        asource.volume = 0.5f;
        asource.loop = false;
        asource.playOnAwake = false;
        asource.clip = aclip;
    }

    public void newlevel()
    {
        isBeginning = true;
        sfx_player_intro.Play();
        StartCoroutine(waiter());
    }

    public void ScoreToAdd(int s)
    {
        score += s;
        Debug.Log(score);
        text_Score.SetText("Score : " + score);
    }
}
