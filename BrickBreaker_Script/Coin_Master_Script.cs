using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Master_Script : MonoBehaviour
{
    [SerializeField] protected GameObject prefab_Coin;
    [SerializeField] protected Ball b;
    protected GameObject clone_coin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateCoin()
    {
        clone_coin = Instantiate(prefab_Coin, new Vector3(b.transform.position.x, b.transform.position.y + 0.3f, 0), Quaternion.identity);
    }
}
