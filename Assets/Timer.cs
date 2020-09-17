using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TMPro.TextMeshProUGUI tm;
    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void display(string di)
    {
        tm.text = di;
    }
}
