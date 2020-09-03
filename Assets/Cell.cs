
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    int stepcount=0;

    public bool iswall=false;

    TMPro.TextMeshPro tm;
    // Start is called before the first frame update
    private void Awake()
    {
        tm = GetComponent<TMPro.TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Stepcutback()
    {

    }

    void Steppush()
    {
        
    }

    public void wall(bool i)
    {
        iswall = i;
        tm.text = "■";
        Debug.Log(tm.text);
        
    }

    public void Placement(float i,float j)
    {
        transform.position = new Vector2(i, j);
    }

    public void Pull(int set)
    {
        stepcount = set;
    }
}
