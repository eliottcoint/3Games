using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Script : MonoBehaviour
{
    [SerializeField] protected GameObject prefab_Brick;
    [SerializeField] protected Ball b;
    private int[,] tableau_brick;
    private float max_X = 5.184f;
    private float max_Y = 2.92f;
    [SerializeField] protected Color Red;
    [SerializeField] protected Color Green;
    [SerializeField] protected Color Blue;

    //protected List<GameObject> listeBriques;
    private int nb_brick = 0;

    // Start is called before the first frame update
    void Start()
    {
        //listeBriques = new List<GameObject>();
        generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generate()
    {
        int nb_lines = Random.Range(4, 10);
        tableau_brick = new int[12, nb_lines];

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < nb_lines; j++)
            {
                tableau_brick[i, j] = Mathf.RoundToInt(Random.Range(0f, 1f));

                if (tableau_brick[i, j] == 1)
                {
                    double posX = -max_X + i * (1.72 / 2);
                    double posY = max_Y - j * (0.92 / 2);
                    GameObject clone_Brick = Instantiate(prefab_Brick, new Vector3((float)posX, (float)posY, 0), Quaternion.identity);
                    clone_Brick.GetComponent<SpriteRenderer>().color = RandomColor();
                    //listeBriques.Add(clone_Brick);
                    nb_brick += 1;
                    if(i <6)
                    {
                        double otherposX = max_X - i * (1.72 / 2);
                        double otherposY = max_Y - j * (0.92 / 2);
                        GameObject other_clone_Brick = Instantiate(prefab_Brick, new Vector3((float)otherposX, (float)otherposY, 0), Quaternion.identity);
                        other_clone_Brick.GetComponent<SpriteRenderer>().color = RandomColor();
                        //listeBriques.Add(other_clone_Brick);
                        nb_brick += 1;
                    }                  
                }
            }
        }
    }

    public void ReportBrickDeath()
    {
        if (nb_brick == 1)
        {
            nb_brick -= 1;
            b.newlevel();
            generate();
        }
        else
        {
            nb_brick -= 1;
        }
        Debug.Log(nb_brick);
    }

    public Color RandomColor()
    {
        int RGB = Mathf.RoundToInt(Random.Range(0f, 3f));
        switch (RGB)
        {
            case 0:
                return Red;
            case 1:
                return Green;
            default:
                return Blue;
        }
    }
}
