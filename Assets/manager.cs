using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    const int MaxTip = 16;

    int[,] stepcount = new int[MaxTip, MaxTip];

    [SerializeField]
    Cell masu;

    [SerializeField]
    Vector2[] undo = new Vector2[180];

    [SerializeField]
    int undoCount = 0;


    [SerializeField]
    int clear = 0;

    [SerializeField]
    int walk;

    [SerializeField]
    int UIsize;

    [SerializeField]
    float[,] rate = new float[MaxTip, MaxTip];

    Cell[,] background = new Cell[MaxTip, MaxTip];
    Cell[,] tip = new Cell[MaxTip, MaxTip];

    int[] Searchlocation = { 0, 1, 0, -1, 1, 0, -1, 0 };



    string cl = "c,l,e,a,r,!";
    string[] part;

    bool[] t = new bool[4];
    bool startfrg;

    int[] dirs = { 0, 0, 0, 0 };

    int pattern = 0;

    int i = 0;
    int j = 0;
    int Basics_i;
    int Basics_j;
    int Basics;
    int start_i;
    int start_j;

    int hoge = 6;
    int dirCount = 0;
    int Annoying = 0;

    int[] size = { 4, 6, 8 };
    int[] data = { 1, 2, 2 };
    int level = 1;

    float total = 0.0f;


    void Awake()
    {

        part = cl.Split(',');

        startfrg = false;


        pattern = Random.Range(1, 4);
        Annoying = 6;
        Basics = ((MaxTip - size[level]) / 2);

        for (i = 0; i < MaxTip; i++)
        {
            for (j = 0; j < MaxTip; j++)
            {
                tip[i, j] = Instantiate(masu);
                tip[i, j].transform.SetParent(transform);


                if (i < Basics
                    || j < Basics
                    || i >= Basics + size[level]
                    || j >= Basics + size[level])
                {
                    tip[i, j].wall(true);
                }
                else
                {
                    DecideRate();
                }


                tip[i, j].Placement(i * UIsize - (MaxTip * UIsize / 2) + 2 * UIsize, j * UIsize - (MaxTip * UIsize / 2) + UIsize / 2);


            }
        }

        puzzle();
    }


    void DecideRate()
    {
        if (tip[i, j].iswall == true)
        {
            rate[i, j] = 0.00f;
            return;
        }
        switch (pattern)
        {
            case 1:
                if (i < Basics + data[level]
                    || j < Basics + data[level]
                    || i >= Basics + size[level] - data[level]
                    || j >= Basics + size[level] - data[level])
                {
                    rate[i, j] = 0.9f;
                }
                else
                {
                    rate[i, j] = 0.3f;
                }
                break;

            case 2:

                if (i < Basics + data[level]
                    || j < Basics + data[level]
                    || i >= Basics + size[level] - data[level]
                    || j >= Basics + size[level] - data[level])
                {
                    rate[i, j] = 0.3f;
                }
                else
                {
                    rate[i, j] = 0.9f;
                }
                break;

            case 3:

                if (i < Basics + data[level]
                    || i >= Basics + size[level] - data[level]
                    || j < Basics + data[level]
                    && j >= Basics + size[level] - data[level])
                {
                    rate[i, j] = 0.3f;
                }
                else
                {
                    rate[i, j] = 0.9f;
                }
                break;
        }

    }


    // Update is called once per frame
    void Update()
    {

        if (startfrg == false)
        {
            return;
        }


        if (clear != 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Redo();
            }
        }

        if (clear == walk)
        {
            Clear();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move(-1, 0);
        }

    }

    void Clear()
    {
        var pe = GetComponentInParent<TimeManager>();
        pe.TimerStop();

        tip[i, j].player(false);
        tip[i, j].wall(true);
        i = 5;
        j = 11;


        for (int s = 0; s < hoge; s++)
        {
            tip[i, j].Clear(part[s]);
            i++;
        }

        clear = 1000;
    }

    void move(int seti, int setj)
    {


        undo[undoCount] = new Vector2(i, j);


        if (seti == 0)
        {
            if (tip[i, j + setj].iswall == true)
                return;
            tip[i, j].player(false);
            j += setj;
            stepcount[i, j]--;
            tip[i, j].Countpush(stepcount[i, j]);


        }
        else
        {
            if (tip[i + seti, j].iswall == true)
                return;

            tip[i, j].player(false);
            i += seti;
            stepcount[i, j]--;
            tip[i, j].Countpush(stepcount[i, j]);
        }

        CheckZero((int)undo[undoCount].x, (int)undo[undoCount].y);
        tip[i, j].player(true);


        undoCount++;
        clear++;
    }
    void Redo()
    {
        undoCount--;
        tip[i, j].player(false);
        tip[i, j].Countpush(1);


        i = (int)undo[undoCount].x;
        j = (int)undo[undoCount].y;

        tip[i, j].player(true);
        tip[i, j].Countpush(0);

        clear--;
    }

    void Initialization()
    {
        for (i = 0; i < MaxTip; i++)
        {
            for (j = 0; j < MaxTip; j++)
            {
                tip[i, j] = null;
            }
        }
    }

    void puzzle()
    {

        for (int f = 0; f < Annoying; f++)
        {

            i = Random.Range(Basics, Basics + size[level]);
            j = Random.Range(Basics, Basics + size[level]);

            if (tip[i, j].iswall == false)
            {


                for (int s = 0; s < 4; s++)
                {

                    if (t[s] == true)
                    {
                        break;
                    }
                    tip[i, j].wall(true);
                }
            }

            ResetBool();

        }
        RandomWalk();
    }

    int Search()
    {


        total = 0;

        for (int nb = 0; nb < 4; nb++)
        {

            total = rate[i + Searchlocation[nb * 2], j + Searchlocation[nb * 2 + 1]];

        }

        float randomPoint = Random.value * total;

        for (int nb = 0; nb < 4; nb++)
        {

            if (randomPoint < rate[i + Searchlocation[nb * 2], j + Searchlocation[nb * 2 + 1]])
            {
                return nb;
            }
            else
            {
                randomPoint -= rate[i + Searchlocation[nb * 2], j + Searchlocation[nb * 2 + 1]];
            }
        
        }

        for (int nb = 0; nb < 4; nb++)
        {
            if (tip[i + Searchlocation[nb * 2], j + Searchlocation[nb * 2 + 1]].iswall == false)
            {
                return nb;
            }
        }
        return 0;
    }

    void ResetBool()
    {
        for (int s = 0; s < 4; s++)
            t[s] = false;

    }
    void RandomWalk()
    {

        walk = 56;

        while (true)
        {
            start_i = Random.Range(Basics, Basics + size[level] - 1);
            start_j = Random.Range(Basics, Basics + size[level] - 1);

            if (tip[start_i, start_j].iswall == false)
                break;

        }

        i = start_i;
        j = start_j;




        for (int f = 0; f < walk; f++)
        {
            
            int dir = Search();
            Debug.Log(dir);
            switch (dir)
            {
                case 0:
                    stepcount[i, j]++;
                    tip[i, j].Countpush(stepcount[i,j]);
                    rate[i, j] -= 0.3f;
                    j++;
                    break;
                case 1:
                    stepcount[i, j]++;
                    tip[i, j].Countpush(stepcount[i, j]);
                    rate[i, j] -= 0.3f;
                    j--;
                    break;
                case 2:
                    stepcount[i, j]++;
                    tip[i, j].Countpush(stepcount[i, j]);
                    rate[i, j] -= 0.3f;
                    i++;
                    break;
                case 3:
                    stepcount[i, j]++;
                    tip[i, j].Countpush(stepcount[i, j]);
                    rate[i, j] -= 0.3f;
                    i--;
                    break;
            }
            dirCount = 0;
        }

        undo[undoCount] = new Vector2(i, j);
        undoCount++;

        wall();
        tip[i, j].player(true);
        tip[i, j].Countpush(0);
        startfrg = true;
    }

    void wall()
    {
        for (int s = Basics; s < Basics+size[level]; s++)
        {
            for (int c = Basics; c < Basics+size[level]; c++)
            {
                CheckZero(s,c);
            }
        }
    }

    public void CheckZero(int dataA ,int dataB)
    {

        if (stepcount[dataA,dataB] == 0)
        {
            tip[i, j].wall(true);
        }
    }


}
    ///
    //Debug.Log(size[level]);
    //Debug.Log(Annoying);
    //Debug.Log(dirs);
    ///*//*/
    ///

