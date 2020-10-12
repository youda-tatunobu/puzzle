
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    int size;

    public bool iswall=false;
    
    public bool isplayer = false;

    TMPro.TextMeshProUGUI tm;
    Image im;
    // Start is called before the first frame update
    private void Awake()
    {
        tm = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        im = transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Countpush(int i)
    {
        
        tm.text = ""+i;
        tm.fontSize = 32;
        iswall = false;

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
        tm.fontSize = 32;
    }

    public void player(bool s)
    {
        isplayer = s;
        
        if(isplayer == true)
            im.color =  Color.red;

        else
            im.color = Color.white;
        

    }

    public void Placement(float i,float j)
    {
        transform.localPosition = new Vector2(i, j);
    }


}
