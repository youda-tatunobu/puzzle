using UnityEngine;

public class manager : MonoBehaviour
{
    const int set = 12;
    [SerializeField]
    Cell masu;

    Cell[,] tip = new Cell[set, set];

    int i = 0;
    int j = 0;

    bool t = true;

    int Annoying;
    int size;
    int start;

    // Start is called before the first frame update
    void Start()
    {



    }
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
        Debug.Log(size);
        Debug.Log(Annoying);
        puzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            i--;

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            i++;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            j++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            j--;
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
            while (true)
            {
                i = Random.Range(1, size);
                j = Random.Range(1, size);

                if (tip[i, j].iswall == false)
                {
                    
                    if (tip[i - 1, j].iswall == true)
                        t = false;
                    if (tip[i, j-1].iswall == true)
                        t = false;

                    if (tip[i+1, j].iswall == true)
                        t = false;
                    if (tip[i, j+1].iswall == true)
                        t = false;
                    

                    if (t == true)
                    {
                        tip[i, j].wall(true);
                        break;
                    }

                    t = true;
                }

            }

        }
        test();
    }


    void test()
    {

        //for (i = 0; i < size; i++)
        //{
        //   for (j = 0; j < size; j++)
        //   {
        //       
        //           Debug.Log(tip[i, j].);

        //  }
        //  }
    }

    /*
     * for (i = 0; i< 5; i++)
        {
            for (j = 0; j< 5; j++)
            {
                tip[i][j] = Instantiate(masu, new Vector3(-3 + j, 3 - i, 0), Quaternion.identity);
                
            }
        }*/
}
