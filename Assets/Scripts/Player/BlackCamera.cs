using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCamera : MonoBehaviour
{
    public bool activate;

    // Start is called before the first frame update
    void Start()
    {
        if(activate)gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
