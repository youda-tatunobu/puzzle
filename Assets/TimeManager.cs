using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    double countup = 0.0f;
    [SerializeField]
    Timer[] time = new Timer[4];


    [SerializeField]
    int[] place = new int[4];
    int placeX;
    
    int[] s = { 600,60,10,1};



    [SerializeField]
    string[] sp;

    [SerializeField]
    string tx;


    bool clearflag=false;


    

    // Start is called before the first frame update
    void Start()
    {
        for (int hoge = 0; hoge < 4; hoge++)
        {
            
            var tr = transform.GetChild(hoge).GetComponent<Timer>();
            time[hoge] = tr;
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clearflag == true)
            return;

        countup += Time.deltaTime;
        place[0] = (int)countup / s[0];

        for (int i = 1; i < 4; i++)
        {
            for (int j = 0; j < i; j++)
            {
                placeX += place[j] * s[j];
                //Debug.Log("j="+j+","+"i="+i);

            }
            //Debug.Log(((int)countup - placeX)+","+s[i]);
            place[i] = ((int)countup - placeX) / s[i];
            time[i].timer(place[i].ToString());

            placeX = 0;
        }

        

    }

    public void TimeStop()
    {
        clearflag = true;
        countup = 0;
        for (int i = 1; i < 4; i++)
            place[i] = 0;

        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(countup);

    }
}
/*
 thousand_place= (int)countup / 600;
        hundreds_place = (int)(countup-(thousand_place * 600)) / 60;
        tens_place = (int)(countup - (thousand_place * 600)-hundreds_place*60) / 10;
        ones_place= (int)countup- (thousand_place * 600)-(hundreds_place * 60)- (tens_place * 10);

tx = totalTime.Minutes.ToString("00");
        time[0].timer(tx);
        //time[0].timer(sp[0]);
        //time[1].timer(sp[1]);


        tx = totalTime.Seconds.ToString("00");
        time[2].timer(tx);
        //time[2].timer(sp[0]);
        //time[3].timer(sp[1]);
        Debug.Log(sp[0]);
        Debug.Log(sp[1]);

    [SerializeField]
    int thousand_place;

    [SerializeField]
    int hundreds_place=0;

    [SerializeField]
    int tens_place=0;

    [SerializeField]
    int ones_place=0;

     


       




    }
 */
