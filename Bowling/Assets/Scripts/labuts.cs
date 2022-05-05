using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labuts : MonoBehaviour
{
    public GameObject ball, text;
    public GameObject[] Labuts;

    public bool isStrike;
    bool again=true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ball.GetComponent<ball>().isFinished || again)
        {
            if (ball.transform.childCount < 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    Labuts[i].AddComponent<Rigidbody>();
                }
            }
            else if (ball.transform.childCount < 10)
            {
                for (int i = 0; i < 5; i++)
                {
                    Labuts[i].AddComponent<Rigidbody>();
                }
            }
            else if (ball.transform.childCount < 15)
            {
                for (int i = 0; i < 8; i++)
                {
                    Labuts[i].AddComponent<Rigidbody>();
                }
            }
            else if (ball.transform.childCount > 15)
            {
                for (int i = 0; i < 10; i++)
                {
                    Labuts[i].AddComponent<Rigidbody>();
                }
                isStrike = true;
            }
            again = false;
        }
    }
        
}
