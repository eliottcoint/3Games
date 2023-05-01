using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_script : MonoBehaviour
{
    public FirstScript fs;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // Check if the apple's position is above -5 then destroy it
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
            // If the gameObject is a Bomb do not loose 2 points
            if (gameObject.tag != "BombCollectible")
            {
                fs.fail();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the gameObject touch the "panier" destroy it
        if (collision.gameObject.name == "panier")
        {
            Destroy(gameObject);
        }
    }
}
