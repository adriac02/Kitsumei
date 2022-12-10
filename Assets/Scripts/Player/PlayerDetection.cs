using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float RayDistance = 3.2f;

    private RaycastHit hit;
    private GameObject last;
    private bool looking = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * RayDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, RayDistance)){
            if (!looking)
            {
                last = hit.collider.gameObject;
                checkLook();
                looking= true;
            }
            if(last != hit.collider.gameObject)
            {
                last = hit.collider.gameObject;
                checkLook();
            }
            
        }
        else
        {
            looking = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            checkClick();
        }


    }

    void checkLook()
    {
        if(last != null && last.GetComponent<Door>() != null) {
            last.GetComponent<Door>().PlayDoorClick();
        }
        else if(last != null && last.GetComponent<Telefono>() !=null) {
            last.GetComponent<Telefono>().PlayTelClick();
        }
    }

    void checkClick()
    {
        if (last != null && last.GetComponent<Door>() != null)
        {
            last.GetComponent<Door>().useDoor();
        }
        else if (last != null && last.GetComponent<Telefono>() != null)
        {
            last.GetComponent<Telefono>().useTel();
        }
    }
}
