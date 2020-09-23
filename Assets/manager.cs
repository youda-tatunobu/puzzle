using UnityEngine;

public class manager : MonoBehaviour
{
    const int set = 16;

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

    Cell[,] background = new Cell[set, set];
    Cell[,] tip = new Cell[set, set];

    string cl = "c,l,e,a,r,!";
    string[] part;

    bool[] t = new bool[4];
    bool startfrg;

    int[] dirs = { 0, 0, 0, 0 };

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
        size = 8;
        Annoying = Random.Range(size - 4, size - 2);
        Basics = ((set - size) / 2);

        for (i = 0; i < set; i++)
        {
            for (j = 0; j < set; j++)
            {
                tip[i, j] = Instantiate(masu);
                tip[i, j].transform.SetParent(transform);

                
                if(i<Basics||j<Basics
                 ||i>=Basics+size||j>=Basics+size)
                {
                    tip[i, j].wall(true);
                }


                tip[i, j].Placement(i * UIsize - (set * UIsize / 2) + 2 * UIsize, j * UIsize - (set * UIsize / 2) + UIsize / 2);

                /*
                if (size % 2 == 1)
                     
                    || i < Basics_i + size
                    || j < Basics_j + size)

                if (size % 2 == 0)
                    tip[i, j].Placement(i * UIsize - (set * UIsize / 2) + 2 * UIsize + 24, j * UIsize - (set * UIsize / 2) + UIsize/2);
                */
            }
        }

        puzzle();
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
        i = (size / 2) - 3;
        j = (size / 2) + 1;


        for (int s = 0; s < hoge; s++)
        {
            tip[i, j].Clear(part[s]);
            i++;
        }
        
        clear++;
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
            tip[i, j].Countpush(-1);


        }
        else
        {
            if (tip[i + seti, j].iswall == true)
                return;

            tip[i, j].player(false);
            i += seti;
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
        for (i = 0; i < set; i++)
        {
            for (j = 0; j < set; j++)
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

            ResetBool();

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

    void ResetBool()
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

            ResetBool();


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
        for (int s = 0; s < size; s++)
        {
            for (int c = 0; c < size; c++)
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
