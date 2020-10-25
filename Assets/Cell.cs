
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    int WallSize;

    [SerializeField]
    int UISize;

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
    public void test(float i)
    {
        tm.text = ""+i;
        tm.fontSize = UISize-8;
        iswall = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Countpush(int i)
    {
        
        tm.text = ""+i;
        tm.fontSize = UISize-8;
        iswall = false;

    }

    public void wall()
    {
        iswall = true;
        tm.text = "■";
        tm.fontSize = WallSize;
        
        
        
        //Debug.Log(tm.text);

    }

    public void Clear(string c)
    {
        tm.text = "" + c;
        tm.fontSize = UISize-8;
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
