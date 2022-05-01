using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ballSize : MonoBehaviour
{
    public GameObject Ball;
    float ball_Size = 0.2f;
    float personValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "person")
        {
            personValue = other.gameObject.GetComponent<personValue>().operationValue;
            ball_Size += personValue;
            Ball.transform.DOScale((ball_Size), 1);
        }
    }
}
