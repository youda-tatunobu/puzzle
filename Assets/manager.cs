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
                    
                    if (tip[i - 1, j].isblock == true)
                        t = false;
                    if (tip[i, j-1].isblock == true)
                        t = false;

                    if (tip[i+1, j].isblock == true)
                        t = false;
                    if (tip[i, j+1].isblock == true)
                        t = false;

                    if (t == true)
                    {
                        tip[i, j].block(true);
                        break;
                    }

                    t = true;
                }

            }

        }
        RandomWalk();
    }

    void RandomWalk()
    {
        walk = Random.Range(((size-2)^2)-Annoying, ((size - 2) ^ 3)-Annoying);
        while(true)
        {
            start_i = Random.Range(1, (size - 2) ^ 2);
            start_j = Random.Range(1, (size - 2) ^ 2);
            if (tip[i, j].isblock == false)
                break;
        }
        i = start_i;
        j = start_j;
        

        for (int f = 0; f < walk; f++)
        {
            while(true)
            {

            }

        }

    }

}
