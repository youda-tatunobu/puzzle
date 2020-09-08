using UnityEngine;

public class manager : MonoBehaviour
{
    const int set = 12;
    [SerializeField]
    Cell masu;

    Cell[,] tip = new Cell[set, set];

    int i = 0;
    int j = 0;

    bool[] t =new bool[4];
    [SerializeField]
    int[] dirs = {0,0,0,0};
    int dirCount = 0;

    int Annoying;
    int size;
    int walk;
    int start_i;
    int start_j;
    // Start is called before the first frame update

    void Awake()
    {
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            tip[i, j].player(false);
            j++;
            tip[i, j].Countpush(-1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tip[i, j].player(false);
            j--;
            tip[i, j].Countpush(-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tip[i, j].player(false);
            i++;
            tip[i, j].Countpush(-1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tip[i, j].player(false);
            i--;
            tip[i, j].Countpush(-1);
        }
        tip[i, j].player(true);

        if (Input.GetMouseButton(1))
        {
            RandomWalk();
        }
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

                
                    search();
                    for (int s = 0; s < 4; s++)
                    {

                        if (t[s] == false)
                        {
                            break;
                        }
                        tip[i, j].wall(true);
                    }


                ResetBool();

        }
        RandomWalk();
    }
    void search()
    {
        if (tip[i, j].iswall == false)
        {
            if (tip[i, j + 1].iswall == true)
                t[0] = false;

            if (tip[i, j - 1].iswall == true)
                t[1] = false;

            if (tip[i + 1, j].iswall == true)
                t[2] = false;

            if (tip[i - 1, j].iswall == true)
                t[3] = false;
        }

    }
    
    void ResetBool()
    {
        for (int s = 0; s < 4; s++)
            t[s] = true;

    }
    void RandomWalk()
    {
        
        walk = Random.Range((int)Mathf.Pow(size-2, 2), (int)Mathf.Pow(size - 2, 3));
        while(true)
        {
            start_i = Random.Range(1, size-1);
            start_j = Random.Range(1, size-1);

            if (tip[start_i, start_j].iswall == false)
                break;
            
        }

        i = start_i;
        j = start_j;

        tip[i, j].player(true);

        Debug.Log(dirCount);
        for (int f = 0; f < walk; f++)
        {
            search();
            for (int s = 0; s <4; s++)
            {
               
                if (t[s] == false)
                {
                    
                    dirs[dirCount] = s;
                    dirCount++;
                }
                    
            }
            dirCount = 0;
            ResetBool();

            var dir = Random.Range(0, dirCount);
            
            switch (dirs[dir])
            {
                case 0:
                    i++;
                    tip[i, j].Countpush(1);

                    break;
                case 1:
                    j++;
                    tip[i, j].Countpush(1);
                    break;
                case 2:
                    i--;
                    tip[i, j].Countpush(1);
                    break;
                case 3:
                    j--;
                    tip[i, j].Countpush(1);
                    break;
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
