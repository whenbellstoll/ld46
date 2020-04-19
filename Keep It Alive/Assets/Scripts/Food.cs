using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    bool carrying;
    GameObject carriedObject;
    public float distance;
    public float smooth;

    //For piano playing
    [SerializeField]
    Shadowman shadow;
    [SerializeField]
    AudioManagement am;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (carrying)
        {
            carry(carriedObject);
            checkDrop();
            //rotateObject();
        }
        else
        {
            pickup();
        }
    }

    void rotateObject()
    {
        carriedObject.transform.Rotate(5, 10, 15);
    }

    void carry(GameObject o)
    {
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
    }

    void pickup()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string p = hit.collider.tag;
                if (p == "Food")
                {
                    carrying = true;
                    carriedObject = hit.collider.gameObject;
                    carriedObject.GetComponent<Rigidbody>().isKinematic = true;
                }
                if(hit.collider.gameObject.name == "piano" )
                {
                    if(shadow.askFor == Shadowman.AskFor.Piano)
                    {
                        am.pianoPlay = true;
                    }
                }
            }
        }
    }

    void checkDrop()
    {
        if (Input.GetMouseButtonDown(1) || carriedObject == null)
        {
            dropObject();
        }
    }

    void dropObject()
    {
        carrying = false;
        if( carriedObject != null )
        {
            carriedObject.GetComponent<Rigidbody>().isKinematic = false;
            carriedObject = null;
        }
        
    }
}
