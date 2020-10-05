using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sura : MonoBehaviour
{
    [SerializeField]
    int[] a;
    float MAX=-100000.0f;
    float MIN=100000.0f;
    float Total=0;
    int Count = 0;
    float iryoku = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        foreach(int i in a)
        {
            Count++;
            Total += (float)a[Count-1] * iryoku;
            
            if (MAX < (float)a[Count - 1] * iryoku)
                MAX = (float)a[Count - 1] * iryoku;

            if (MIN > (float)a[Count - 1] * iryoku)
                MIN = (float)a[Count - 1] * iryoku
                    ;
        }
        Debug.Log("MAX=" + MAX + "," + "MIN=" + MIN + "," + "Total=" + Total + "," + "wariai=" + Total / Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
