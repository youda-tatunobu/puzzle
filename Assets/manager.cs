using UnityEngine;

public class manager : MonoBehaviour
{
    const int set = 12;
    [SerializeField]
    Cell masu;

    Cell[,] tip = new Cell[set, set];

    [SerializeField]
    Vector2[] undo = new Vector2[180];
    int undoCount = 0;
    int i = 0;
    int j = 0;

    bool[] t =new bool[4];
    bool startfrg;

    [SerializeField]
    int[] dirs = {0,0,0,0};
    int dirCount = 0;
    int clear = 0;


    int Annoying;
    int size;
    int walk;
    int start_i;
    int start_j;
    // Start is called before the first frame update

    void Awake()
    {
        startfrg = false;
        size = Random.Range(6, 13);
        Annoying = Random.Range(size-6, size-4);
        
        for (i = 0; i < size; i++)
        {
            for (j = 0; j < size; j++)
            {

                tip[i, j] = Instantiate(masu);

                if (i == 0)
                    tip[i, j].wall(true);
                if (j == 0)
                    tip[i, j].wall(true);

                if (i == size - 1)
                    tip[i, j].wall(true);

                if (j == size - 1)
                    tip[i, j].wall(true);

                if (size % 2 == 1)
                    tip[i, j].Placement(i - (size / 2), j - (size / 2));

                if (size % 2 == 0)
                    tip[i, j].Placement(i - (size / 2) + (float)0.5, j - (size / 2) + (float)0.5);

            }
        }
        
        puzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if(startfrg==false)
        {
            return;
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
        Debug.Log("a");
        tip[i, j].player(true);

        if(clear<0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Redo();
        }

    }
    void move(int seti,int setj)
    {
        tip[i, j].CheckZero();

        if (seti==0)
        {
            if (tip[i, j + setj].iswall == true)
                return;

            tip[i,j].player(false);
            j+=setj;
            tip[i, j].Countpush(-1);

            
        }
        else
        {
            if (tip[i+seti, j ].iswall == true)
                return;
            tip[i,j].player(false);
            i+=seti;
            tip[i, j].Countpush(-1);
        }
        undo[undoCount] = new Vector2(i, j);
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
            
                i = Random.Range(1, size);
                j = Random.Range(1, size);

            if (tip[i, j].iswall == false)
            {
                Debug.Log("A");
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
        
        walk = Random.Range((int)Mathf.Pow(size-2, 2), (int)Mathf.Pow(size - 2, 3)/(size/2));
        
        while(true)
        {
            start_i = Random.Range(1, size-1);
            start_j = Random.Range(1, size-1);

            if (tip[start_i, start_j].iswall == false)
                break;
            
        }

        i = start_i;
        j = start_j;

        

        
        for (int f = 0; f < walk; f++)
        {
            Debug.Log("B");

            search();
            for (int s = 0; s <4; s++)
            {
                Debug.Log(s);
                if (t[s] == false)
                {
                    
                    dirs[dirCount] = s;
                    dirCount++;
                }
                    
            }
            
            ResetBool();
            

            var dir = Random.Range(0, dirCount);
            Debug.Log(dir+" "+dirCount);
            switch (dirs[dir])
            {
                case 0:
                    j++;
                    tip[i, j].Countpush(1);

                    break;
                case 1:
                    j--;
                    tip[i, j].Countpush(1);
                    break;
                case 2:
                    i++;
                    tip[i, j].Countpush(1);
                    break;
                case 3:
                    i--;
                    tip[i, j].Countpush(1);
                    break;
            }
            dirCount = 0;
        }
        wall();
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
    //Debug.Log(size);
    //Debug.Log(Annoying);
    //Debug.Log(dirs);
    ///*//*/
    ///
    /*
    while(true)
            {
                for (int d = dirCount; d > 0; d--)
                {
                    var rand = Random.Range(0, d);
                    

                    for (int s = rand; s <= d - 2; s++)
                    {
                        //Debug.Log(s);
                        if(tip[])
                        dirs[s] = dirs[s + 1];
}

                    
                }

                switch(dirs[0])
                {
                    case 0:
                        i++;
                        break;
                    case 1:
                        j++;
                        break;
                    case 2:
                        i--;
                        break;
                    case 3:
                        j--;
                        break;
                }

            }
            
        }*/
}
