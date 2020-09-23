using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    float countup = 0.0f;
    [SerializeField]
    Timer[] time = new Timer[4];

    
    
    int placeX;
    
    int[] s = { 600,60,10,1};


    int[] place = new int[4];

    bool clear = false;

    // Start is called before the first frame update
    private void Awake()
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
        
        if (clear==true)
        {
            return;
        }

        countup += Time.deltaTime;
        place[0] = (int)countup / s[0];
        time[0].display(place[0]);

        for (int i = 1; i < 4; i++)
        {
            for (int j = 0; j < i; j++)
            {
                placeX -= place[j] * s[j];
                //Debug.Log("j="+j+","+"i="+i);
                Debug.Log(j);

            }
            place[i] = ((int)countup + placeX) / s[i];
            time[i].display(place[i]);

            placeX = 0;
        }


    }

    public void TimeStart()
    {
        clear = false;
    }

    public void TimerStop()
    {

        var timeScore = new System.TimeSpan(0, 0, place[0]*10+place[1], place[2] * 10 + place[3], 0);
        Debug.Log(timeScore);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(timeScore);

        countup = 0;
        for (int i = 0; i < 4; i++)
        {
            place[i] = 0;
        }
            clear = true;
    }
}
/*
 thousand_place= (int)countup / 600;
        hundreds_place = (int)(countup-(thousand_place * 600)) / 60;
        tens_place = (int)(countup - (thousand_place * 600)-hundreds_place*60) / 10;
        ones_place= (int)countup- (thousand_place * 600)-(hundreds_place * 60)- (tens_place * 10);

 countup += Time.deltaTime;
        place[0] = (int)countup / s[0];
        time[0].display(place[0]);

        for (int i = 1; i < 4; i++)
        {
            for (int j = 0; j < i; j++)
            {
                placeX -= place[j] * s[j];
                //Debug.Log("j="+j+","+"i="+i);
                Debug.Log(j);
            
            }
            place[i] = ((int)countup + placeX) / s[i];
            time[i].display(place[i]);

            placeX = 0;
        }
 
 
 */
