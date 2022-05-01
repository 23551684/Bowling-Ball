using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class takedownLabuts : MonoBehaviour
{
    private int labutCount;
    private float movementSpeed = 5f;
    public Rigidbody rb;
    private bool isKinematic;
    private bool detectCollisions;
    float ballSize = 0.2f;
    float personValue;

    public Animator canvas;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

        //output to log the position change
        Debug.Log(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ballSize < 10 && labutCount < ballSize)
        {
            EnableRigidBody();
        }

        if (ballSize > 10 && labutCount < ballSize)
        {
            EnableRigidBody();
        }

        if (labutCount > ballSize)
        {
            canvas.SetTrigger("lose");
        }
    }

    void EnableRigidBody()
    {
        rb.isKinematic = true;
        rb.detectCollisions = true;
    }
    void DisableRigidBody()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }
}
