using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ball : MonoBehaviour
{
    public float speed;
    public float boost;
    public GameObject prefRagdoll, cam, strikeText;
    public LevelManager levelManager;
    public ParticleSystem blood, strike;

    public Material red,blue;
         
    Vector3 mousePosition=Vector3.zero;
    public GameObject[] humans;
    float smooth = 5.0f;
    float rotx;
    float rotz;
    float backForce;
    public bool isFinished;
    bool onStage=true;
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

       
        
    }

     public void spwanRagdoll(Material color)
    {
        var newChar = Instantiate(prefRagdoll, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newChar.transform.parent = gameObject.transform;
        newChar.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = color;
    }
    void controlBall()
    {
        float horizontal = 0;

        if (!isFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 1.5f;
                mousePosition = Input.mousePosition;
            }

           
        }

        boost = Mathf.Clamp(boost + Time.deltaTime * 1f, 2, 1);
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * -speed * backForce + new Vector3(1, 0, 0) * horizontal * -2f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.2f, 1.2f), transform.position.y, transform.position.z);
        backForce = Mathf.Clamp(backForce + Time.deltaTime * 3.25f, -1, 1);
        if (onStage)
        {
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

    }
    
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "collectableHuman")
        {
            Destroy(other.gameObject);
            spwanRagdoll(red);
        }
        if (other.gameObject.tag == "makeItBigger")
        {
            transform.DOMoveY(transform.position.y + 0.1f, 1);
            transform.DOScale(transform.localScale.x + 0.2f, 1);
            Destroy(other.gameObject);
            spwanRagdoll(blue);
        }
        if (other.gameObject.tag == "obstacle")
        {
            blood.Play();
            transform.GetChild(transform.childCount-1).transform.parent = null;
            backForce = -3;
            transform.DOMoveY(transform.position.y - 0.025f, 1);
            transform.DOScale(transform.localScale.x - 0.05f, 1);

        }
        if (other.gameObject.tag == "flame")
        {
            blood.Play();
            transform.GetChild(transform.childCount - 1).transform.parent = null;
            transform.DOMoveY(transform.position.y - 0.025f, 1);
            transform.DOScale(transform.localScale.x - 0.005f, 1);
        }
        if (other.gameObject.tag == "Finish")
        {
            Destroy(cam.GetComponent<camFallow>());
            cam.transform.DOMove(new Vector3(0, 9, -80), 1f);
            cam.transform.DORotate(new Vector3(25, 180, 0), 1f);
            isFinished = true;
            speed = Mathf.Clamp(transform.childCount * 1.25f, 8, 12);

        }
        if (other.gameObject.tag == "labut")
        {
            yield return new WaitForSeconds(0.4f);
            if (other.GetComponentInParent<labuts>().isStrike)
            {
                
                strikeText.SetActive(true);
                strike.Play();
            }
            speed = 0f;
            onStage = false;
            levelManager.win();
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
