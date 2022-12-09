using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference DoorClickEvent;
    [SerializeField] private FMODUnity.EventReference DoorOpenEvent;

    private BoxCollider doorCol;

    // Start is called before the first frame update
    void Start()
    {
        doorCol= GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void useDoor() {
        
        FMOD.Studio.EventInstance DoorOpen = FMODUnity.RuntimeManager.CreateInstance(DoorOpenEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(DoorOpen, transform, GetComponent<Rigidbody>());
        if (doorCol.isTrigger)
        {
            doorCol.isTrigger = false;
            DoorOpen.setParameterByName("Opened", 1);
        }
        else
        {
            doorCol.isTrigger = true;
            DoorOpen.setParameterByName("Opened", 0);
        }
        DoorOpen.start();
        DoorOpen.release();
    }

    public void PlayDoorClick()
    {
        FMOD.Studio.EventInstance DoorClick = FMODUnity.RuntimeManager.CreateInstance(DoorClickEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(DoorClick, transform, GetComponent<Rigidbody>());
        DoorClick.start();
        DoorClick.release();
    }
}
