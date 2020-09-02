using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    const int set = 10;
    [SerializeField]
    Cell masu;

    Cell[][] tip = new Cell[set][];

    
    int Annoying;

    // Start is called before the first frame update
    void Start()
    {
        Annoying= Random.Range(1, 5);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                tip[i][j] = Instantiate(masu, new Vector3(-3 + j, 3 - i, 0), Quaternion.identity);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
