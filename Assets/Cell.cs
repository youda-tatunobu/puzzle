
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    int stepcount=0;
    [SerializeField]
    int size;

    public bool iswall=false;
    
    public bool isplayer = false;

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


    public void Countpush(int i)
    {
        stepcount+=i;
        tm.text = ""+stepcount;
        tm.fontSize = 6;
        iswall = false;

    }

    public void CheckZero()
    {
        
        if(stepcount==0)
        {
            wall(true);
        }
    }


    public void wall(bool i)
    {
        iswall = i;
        tm.text = "■";
        tm.fontSize = size;
        
        //Debug.Log(tm.text);

    }

    public void Clear(string c)
    {
        tm.text = "" + c;
        tm.fontSize = 6;
    }

    public void player(bool s)
    {
        isplayer = s;
        var sp=transform.GetComponentInChildren<SpriteRenderer>();
        if(isplayer == true)
            sp.color =  Color.red;

        else
            sp.color = Color.white;
        

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
