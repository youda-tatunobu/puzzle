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
    int size = 0;


    // Start is called before the first frame update

    void Awake()
    {


        part = cl.Split(',');

        startfrg = false;

        size = Random.Range(4, 10);
        pattern= Random.Range(1, 4);
        Annoying = Random.Range(size - 4, size - 2);
        Basics = ((MaxTip - size) / 2);

        for (i = 0; i < MaxTip; i++)
        {
            for (j = 0; j < MaxTip; j++)
            {
                tip[i, j] = Instantiate(masu);
                tip[i, j].transform.SetParent(transform);

                
                if(    i < Basics
                    || j < Basics
                    || i >= Basics + size
                    || j >= Basics + size )
                { 
                    tip[i, j].wall(true);
                }
                else
                {
                    DecideRate();
                }


                tip[i, j].Placement(i * UIsize - (MaxTip * UIsize / 2) + 2 * UIsize, j * UIsize - (MaxTip * UIsize / 2) + UIsize / 2);

                /*
                if (size % 2 == 1)
                     
                    || i < Basics_i + size
                    || j < Basics_j + size)

                if (size % 2 == 0)
                    tip[i, j].Placement(i * UIsize - (MaxTip * UIsize / 2) + 2 * UIsize + 24, j * UIsize - (MaxTip * UIsize / 2) + UIsize/2);
                */
            }
        }

        puzzle();
    }


    void DecideRate()
    {

        switch(pattern)
        {
            case 1:

                if (   i < Basics + size * 1 / 4
                    || j < Basics + size * 1 / 4
                    || i >= Basics + size
                    || j >= Basics + size)
                {
                    rate[i, j] = 0.8f;
                }
                else
                {
                    rate[i, j] = 0.4f;
                }
                break;

            case 2:
                if (i < Basics + size * 1 / 4
                    || j < Basics + size * 1 / 4
                    || i >= Basics + size  
                    || j >= Basics + size  )
                {
                    rate[i, j] = 0.4f;
                }
                else
                {
                    rate[i, j] = 0.8f;
                }
                break;

            case 3:
                if (i < Basics + size * 2 / 4
                    || j < Basics + size * 2 / 4
                    || i >= Basics +  size * 3 / 4
                    || j >= Basics +  size * 3 / 4)
                {
                    rate[i, j] = 0.8f;
                }
                else
                {
                    rate[i, j] = 0.4f;
                }
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(clear>walk)
        {
            return;
        }
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
        i = 11;
        j = 5;


        for (int s = 0; s < hoge; s++)
        {
            tip[i, j].Clear(part[s]);
            i++;
        }
        
        clear++;
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

            i = Random.Range(Basics, Basics + size);
            j = Random.Range(Basics, Basics + size);

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
            t[0] = true;

        if (tip[i, j - 1].iswall == true)
            t[1] = true;

        if (tip[i + 1, j].iswall == true)
            t[2] = true;

        if (tip[i - 1, j].iswall == true)
            t[3] = true;


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
        
        walk = Random.Range((int)Mathf.Pow(size - 2, 2), (int)Mathf.Pow(size - 2, 3) / (size / 2));

        while (true)
        {
            start_i = Random.Range(Basics, Basics + size - 1);
            start_j = Random.Range(Basics, Basics + size - 1);

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
        for (int s = Basics; s < Basics+size; s++)
        {
            for (int c = Basics; c < Basics+size; c++)
            {
                tip[s, c].CheckZero();
            }
        }
    }
    ///
    //Debug.Log(size);
    //Debug.Log(Annoying);
    //Debug.Log(dirs);
    ///*//*/
    ///
}
