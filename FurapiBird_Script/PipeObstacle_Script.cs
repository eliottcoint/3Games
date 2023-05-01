using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeObstacle_Script : MonoBehaviour
{
    private float pipeSpeed = 4f;
    const float despawn_posX = -12f;
    public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( -pipeSpeed * Time.deltaTime , 0, 0 );
        ChangeSpeed();
        if (transform.position.x < despawn_posX)
        {
            SelfDestruction();
        }
    }
    public void SelfDestruction()
    {
        manager.RemoveFromPipeList(this);
    }
    public void ChangeSpeed()
    {
        pipeSpeed = manager.speedPipe; 
    }
}
