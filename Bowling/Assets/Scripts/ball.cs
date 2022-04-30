using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ball : MonoBehaviour
{
    public float speed;
    public GameObject prefRagdoll;
    public LevelManager levelManager;
    public ParticleSystem blood;
         
    Vector3 mousePosition=Vector3.zero;
    public GameObject[] humans;
    float smooth = 5.0f;
    float rotx;
    float rotz;
    float backForce;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controlBall();

        if (transform.localScale.x < 0.3)
        {

            StartCoroutine(dead());
        }

        int t = 0;
        for(int i=0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.tag=="prefRagdoll")
            {
                
                humans[t] = transform.GetChild(i).gameObject;
                t++;
            }
        }
        
    }

     public void spwanRagdoll()
    {
        var newChar = Instantiate(prefRagdoll, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newChar.transform.parent = gameObject.transform;
    }
    void controlBall()
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



        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * -speed * backForce + new Vector3(1, 0, 0) * horizontal * -2f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.2f, 1.2f), transform.position.y, transform.position.z);
        backForce = Mathf.Clamp(backForce + Time.deltaTime * 3.25f, -1, 1);

        if (horizontal > 0)
        {
            rotz += 2;
        }
        else if (horizontal < 0)
        {
            rotz += -2;
        }
        else
        {
            rotz = 0;
        }
        rotx -= 2;
        Quaternion target = Quaternion.Euler(rotx, 0, rotz);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "collectableHuman")
        {
            Destroy(other.gameObject);
            spwanRagdoll();
        }
        if (other.gameObject.tag == "makeItBigger")
        {
            transform.DOMoveY(transform.position.y + 0.1f, 1);
            transform.DOScale(transform.localScale.x + 0.2f, 1);
            Destroy(other.gameObject);
            spwanRagdoll();
        }
        if (other.gameObject.tag == "obstacle")
        {
            print(humans.Length);
            humans[humans.Length-1].transform.parent = null;
            backForce = -3;
            transform.DOMoveY(transform.position.y - 0.025f, 1);
            transform.DOScale(transform.localScale.x - 0.05f, 1);

        }
        if (other.gameObject.tag == "flame")
        {
            transform.DOMoveY(transform.position.y - 0.025f, 1);
            transform.DOScale(transform.localScale.x - 0.005f, 1);
        }
    }

    IEnumerator dead()
    {
        levelManager.fail();
        blood.Play();
        yield return new WaitForSeconds(.4f);
        Destroy(transform.gameObject);
    }
}
