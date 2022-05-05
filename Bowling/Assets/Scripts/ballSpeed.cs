using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpeed : MonoBehaviour
{
    public float speed;
    public float boost;
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
        if(other.gameObject.tag == "collactableHuman")
        {
            Vector3 mousePosition = Vector3.zero;
            float horizontal = 0;
            horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 1.5f;
            mousePosition = Input.mousePosition;
            boost = Mathf.Clamp(boost + Time.deltaTime * 1f, 2, 1);
            transform.position += new Vector3(0, 0, 1) * Time.deltaTime * -speed * boost + new Vector3(1, 0, 0) * horizontal * -2f;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.2f, 1.2f), transform.position.y, transform.position.z);
        }
    }
}
