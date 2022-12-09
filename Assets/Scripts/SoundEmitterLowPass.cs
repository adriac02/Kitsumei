using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SoundEmitterLowPass : MonoBehaviour
{
    public Transform player;
    private StudioEventEmitter sound;
    private bool blocked = false;
    private float lowpass = 0;
    // Start is called before the first frame update
    void Start()
    {
        sound= GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, player.position);
        if (Physics.Linecast(transform.position, player.position,3)){
            Debug.Log("Blocked");
            blocked= true;
        }
        else
        {
            blocked = false;
        }
        if (blocked && lowpass < 1.0f)
        {
            lowpass += 0.1f;
        }
        else if(!blocked && lowpass > 0.0f)
        {
            lowpass -= 0.1f;
        }
        sound.SetParameter("isBlocked", lowpass);
    }
}
