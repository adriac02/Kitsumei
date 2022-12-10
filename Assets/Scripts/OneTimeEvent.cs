using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeEvent : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference RadiadorEvent;
    public string Parameter;
    public int ParameterValue;
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
        if (other.gameObject.GetComponent<StoryManager>() != null)
        {
            FMOD.Studio.EventInstance Rad = FMODUnity.RuntimeManager.CreateInstance(RadiadorEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Rad, transform, GetComponent<Rigidbody>());
            Rad.setParameterByName(Parameter, ParameterValue);
            Rad.start();
            Rad.release();
            GetComponent<BoxCollider>().enabled = false;
        }
        
    }
}
