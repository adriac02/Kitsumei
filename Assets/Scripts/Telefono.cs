using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telefono : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference TelefonoEvent;
    public Transform player;

    FMOD.Studio.EventInstance Telef;
    private bool blocked = false;
    private float lowpass = 0;

    bool descolgado = false;

    // Start is called before the first frame update
    void Start()
    {
        Telef = FMODUnity.RuntimeManager.CreateInstance(TelefonoEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Telef, transform, GetComponent<Rigidbody>());
        Telef.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, 50.0f);
        Telef.setParameterByName("step", 0);
        Telef.start();
        Telef.release();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Physics.Linecast(transform.position, player.position, 3))
        {
            blocked = true;
        }
        else
        {
            blocked = false;
        }
        if (blocked && lowpass < 1.0f)
        {
            lowpass += 0.1f;
        }
        else if (!blocked && lowpass > 0.0f)
        {
            lowpass -= 0.1f;
        }
        Telef.setParameterByName("isBlocked", lowpass);
    }

    public void useTel()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Telef, transform, GetComponent<Rigidbody>());
        if (!descolgado)
        {

            descolgado = true;
            Telef.setParameterByName("step", 2);
            GameManager.instance.increaseStoryStep();
        }
        else
        {
            
            Telef.setParameterByName("step", 3);
            GameManager.instance.increaseStoryStep();
            GameManager.instance.bloquearPuerta();
        }
        Telef.start();
        Telef.release();
    }

    public void PlayTelClick()
    {
        FMOD.Studio.EventInstance OpenTel = FMODUnity.RuntimeManager.CreateInstance(TelefonoEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(OpenTel, transform, GetComponent<Rigidbody>());
        OpenTel.setParameterByName("step", 1);
        OpenTel.start();
        OpenTel.release();
    }
}
