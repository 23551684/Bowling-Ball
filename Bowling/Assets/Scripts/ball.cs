using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float speed;
    public GameObject prefRagdoll;

    Vector3 mousePosition=Vector3.zero;

    float smooth = 5.0f;
    float rotx;
    float rotz;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = 0;


        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 1.5f;
            mousePosition = Input.mousePosition;
        }
        


        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * speed  + new Vector3(1, 0, 0) * horizontal * 4;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3f, 3f), transform.position.y, transform.position.z);

        if (horizontal > 0)
        {
            rotz += -2;
        }
        else if(horizontal<0)
        {
            rotz += 2;
        }
        else
        {
            rotz = 0;
        }
        rotx +=2;
        Quaternion target = Quaternion.Euler(rotx, 0, rotz);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

     public void spwanRagdoll()
    {
        var newChar = Instantiate(prefRagdoll, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newChar.transform.parent = gameObject.transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "collectableHuman")
        {
            Destroy(collision.gameObject);
            spwanRagdoll();
        }
    }
}
