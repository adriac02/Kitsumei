using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;
    bool start = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            timer += Time.deltaTime;
        }
        if(timer > 10)
        {
            GameManager.instance.increaseStoryStep();
            GameManager.instance.final();
            start = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        timer = 0;
        start = true;
        
    }
}
