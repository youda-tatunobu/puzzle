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
    [SerializeField]
    int[] dirs = {0,1,2,3};
    int dirCount = 4;

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
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tip[i, j].player(false);
            j--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tip[i, j].player(false);
            i++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tip[i, j].player(false);
            i--;
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
            start_i = Random.Range(1, size-1);
            start_j = Random.Range(1, size-1);
            if (tip[start_i, start_j].isblock == false)
                break;
            
        }

        i = start_i;
        j = start_j;

        tip[i, j].player(true);

        return;

        for (int f = 0; f < 1; f++)
        {
            while(true)
            {
                for (int d = dirCount; d > 0; d--)
                {
                    var rand = Random.Range(0, d);

                    for (int s = rand; s <= d - 2; s++)
                    {
                        //Debug.Log(s);

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
            
        }
        
    }
    //Debug.Log(size);
    //Debug.Log(Annoying);
    //Debug.Log(dirs);
    ///*//*/
}
