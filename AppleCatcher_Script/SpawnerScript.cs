using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Reference to the "FirstScript" to have the GetCompenent of the apples
    public FirstScript fs;
    // Set some limits to the spawn position
    const float SCREEN_XLIMIT = 8.0f;
    const float SPAWN_POSY = 6.0f;
    // Get the component RigiBody to change the gravity of each apples
    private Rigidbody2D speed;
    // Our 3 types of apples
    [SerializeField] protected GameObject prefab_pomme;
    [SerializeField] protected GameObject prefab_goldenapple;
    [SerializeField] protected GameObject prefab_bomb;
    // First prefab to set the apple wanted
    protected GameObject Prefab;
    // Second prefab to instantiate a clone
    protected GameObject newApple;
    // To set the time between two spawn
    protected float spawnTimer;
    // Test if we need to respawn a apple
    private bool respawn;

    // Start is called before the first frame update
    void Start()
    {
        // Set the first respawn to true
        respawn = true;   
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the apple needto respawn
        if (respawn == true)
        {
            SpawnTime();
        }

        spawnTimer -= Time.deltaTime;
        Prefab = prefab_pomme;
        // Set thechance to have a special apple
        int chancespecial = Random.Range(0, 100);

        if (spawnTimer <= 0)
        {
            if (chancespecial < 10)
            {
                Prefab = prefab_goldenapple;
            }
            else if (chancespecial > 90)
            {
                Prefab = prefab_bomb;
            }
            // Instantiate a clone of the apple selected
            newApple = Instantiate(Prefab);
            newApple.GetComponent<Apple_script>().fs = this.fs;
            newApple.transform.position = new Vector3(Random.Range(-SCREEN_XLIMIT, SCREEN_XLIMIT), SPAWN_POSY, 0);

            // Change the gravity scale of the GameObject "newApple"
            speed = Prefab.GetComponent<Rigidbody2D>();
            speed.gravityScale = Random.Range(0.5f, 2f);

            respawn = true;
        }
    }

    // Used to make variation between to respawn of apples
    public void SpawnTime()
    {
        // Play on the time during the party to affect the randomizer of the spawn time
        spawnTimer = Random.Range(0.5f, 1.0f) - Time.deltaTime * 2;
        respawn = false;
    }
}
