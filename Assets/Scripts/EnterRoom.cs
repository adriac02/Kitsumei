using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference EnterRoomEvent;        // Use this in the Editor to select our Footsteps Event.
    [SerializeField] private string ParameterName;
    public int ParameterValue;
    private bool entered = false;

    public bool isRoom = false;
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
        if(other.gameObject.GetComponent<Footsteps>() != null)
        {
            if (!entered && isRoom)
            {
                Debug.Log("Entrado en habitacion");
                GameManager.instance.increaseRooms();
                entered = true;
            }
            FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(EnterRoomEvent);
            Footstep.setParameterByName(ParameterName, ParameterValue);
            Footstep.start();
            Footstep.release();
        }
    }
}
