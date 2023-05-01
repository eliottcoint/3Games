using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField] private Manager manager;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float flapForce = 6f;
    [SerializeField] private float maxRotation = 0;
    [SerializeField] private float maxVelocityY = 3;
    private bool isDead;
    private float rotation_vel = 0;
    [SerializeField] private float rotation_time = 0.5f;
    private AudioSource audioBird;

    public CameraShake ck;
    public float shakeMagnitude = 0.2f;
    public float shakeDuration = 0.2f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioBird = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isGameStarted)
        {
            rb.isKinematic = false;
        }
        //Prevent the code from executing if the bird is dead
        if (isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            flap();
        }
        SetRotation();
    }


    //Set rotation of bird sprite based on the velocity 
    public void SetRotation()
    {
        float rotation_goal = Mathf.Clamp(maxRotation*(rb.velocity.y/maxVelocityY), -90, 90);

        float rotation_act = rb.rotation;
        float rotation = Mathf.SmoothDamp(rotation_act, rotation_goal, ref rotation_vel, rotation_time);
        rb.SetRotation(rotation); 
    }

    //Make the bird flap its wings to fly
    public void flap()
    {
        //Set velocity to 0
        rb.velocity = new Vector2(0, 0);

        //Flap 
        rb.AddForce(flapForce * Vector2.up, ForceMode2D.Impulse);
    }

    public void death()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        manager.isGameOver = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pipe"))
        {
            death();
            audioBird.Play();
            ck.ShakeCam(shakeDuration, shakeMagnitude);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gap") && isDead == false)
        {
            manager.UpdateScore(1);
        }
        else if (collision.gameObject.CompareTag("deathZone") && isDead == false)
        {
            death();
            audioBird.Play();
            ck.ShakeCam(shakeDuration, shakeMagnitude);
        }
    }
}
