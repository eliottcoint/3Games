using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float backSpeed = 4f;
    [SerializeField] private float percentage_speed_back =1;
    [SerializeField] private float randomHeight = 0; 
    [SerializeField] private float despawn_posX = -12f;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-backSpeed* percentage_speed_back * Time.deltaTime, 0, 0);
        SetSpeed();
        if (transform.position.x < despawn_posX)
        {
            Respawn(); 
        }
    }

    public void Respawn()
    {
        transform.position = spawnPos.position - new Vector3(0, Random.Range(0, randomHeight),0); 
    }
    public void SetSpeed()
    {
        backSpeed = manager.background_speed;
    }
}