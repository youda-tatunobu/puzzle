using UnityEngine;

public class manager : MonoBehaviour
{
    const int MaxTip = 16;

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
    int[] data = { 1, 1, 2 };
    int level=1;

    double total = 0;

    double[] debug = { 0.8,0.8,0.4,0.4 };


    // Start is called before the first frame update

    void Awake()
    {
        for(int a=0;a<4;a++)
        {
            total += debug[a];
        }
        var test= Random.value * total;
        Debug.Log(test);
        part = cl.Split(',');

        startfrg = false;

        
        pattern= Random.Range(1, 4);
        Annoying = Random.Range(size[level] - 4, size[level] - 2);
        Basics = ((MaxTip - size[level]) / 2);
        
        for (i = 0; i < MaxTip; i++)
        {
            for (j = 0; j < MaxTip; j++)
            {
                tip[i, j] = Instantiate(masu);
                tip[i, j].transform.SetParent(transform);

                
                if(    i < Basics
                    || j < Basics
                    || i >= Basics + size[level]
                    || j >= Basics + size[level] )
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
        return;
        puzzle();
    }


    void DecideRate()
    {

        switch(pattern)
        {
            case 1:

                if (   i < Basics + data[level]
                    || j < Basics + data[level]
                    || i >= Basics + size[level]- data[level]
                    || j >= Basics + size[level]- data[level])
                {
                    rate[i, j] = 0.8f;
                }
                else
                {
                    rate[i, j] = 0.4f;
                }
                break;

            case 2:

                if (i < Basics + data[level]
                    || j < Basics + data[level]
                    || i >= Basics + size[level] - data[level]
                    || j >= Basics + size[level] - data[level])
                {
                    rate[i, j] = 0.4f;
                }
                else
                {
                    rate[i, j] = 0.8f;
                }
                break;

            case 3:

                if (i < Basics + data[level]
                    || i >= Basics + size[level] - data[level]
                    || j < Basics + data[level]
                    && j >= Basics + size[level] - data[level])
                {
                    rate[i, j] = 0.4f;
                }
                else
                {
                    rate[i, j] = 0.8f;
                }
                break;
        }
        tip[i,j].debug(rate[i, j]);
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
        
        clear=1000;
    }

    void move(int MaxTipi, int MaxTipj)
    {


        undo[undoCount] = new Vector2(i, j);


        if (MaxTipi == 0)
        {
            if (tip[i, j + MaxTipj].iswall == true)
                return;
            tip[i, j].player(false);
            j += MaxTipj;
            tip[i, j].Countpush(-1);


        }
        else
        {
            if (tip[i + MaxTipi, j].iswall == true)
                return;

            tip[i, j].player(false);
            i += MaxTipi;
            tip[i, j].Countpush(-1);
        }

        tip[(int)undo[undoCount].x, (int)undo[undoCount].y].CheckZero();
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

                search();
                for (int s = 0; s < 4; s++)
                {

                    if (t[s] == true)
                    {
                        break;
                    }
                    tip[i, j].wall(true);
                }
            }

            ReMaxTipBool();

        }
        RandomWalk();
    }
    void search()
    {

        if (tip[i, j + 1].iswall == true)
        {
            total= rate[i, j + 1];
            t[0] = true;
        }


        if (tip[i, j - 1].iswall == true)
        {
            total = rate[i, j - 1];
            t[1] = true;
        }


        if (tip[i + 1, j].iswall == true)
        {
            total = rate[i + 1, j];
            t[2] = true;
        }


        if (tip[i - 1, j].iswall == true)
        {
            total = rate[i - 1, j];
            t[3] = true;
        }



    }

    void TipValue()
    {

    }

    void ReMaxTipBool()
    {
        for (int s = 0; s < 4; s++)
            t[s] = false;

    }
    void RandomWalk()
    {
        
        walk = Random.Range((int)Mathf.Pow(size[level] - 2, 2), (int)Mathf.Pow(size[level] - 2, 3) / (size[level] / 2));

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


            search();
            for (int s = 0; s < 4; s++)
            {

                if (t[s] == false)
                {

                    dirs[dirCount] = s;
                    dirCount++;
                }

            }

            ReMaxTipBool();


            var dir = Random.Range(0, dirCount);
            //Debug.Log(i + "," + j + "," + dirs[dir]);
            switch (dirs[dir])
            {
                case 0:

                    tip[i, j].Countpush(1);
                    j++;
                    break;
                case 1:

                    tip[i, j].Countpush(1);
                    j--;
                    break;
                case 2:

                    tip[i, j].Countpush(1);
                    i++;
                    break;
                case 3:

                    tip[i, j].Countpush(1);
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
                tip[s, c].CheckZero();
            }
        }
    }
    ///
    //Debug.Log(size[level]);
    //Debug.Log(Annoying);
    //Debug.Log(dirs);
    ///*//*/
    ///
}
