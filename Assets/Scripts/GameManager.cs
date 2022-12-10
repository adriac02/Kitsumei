using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Telefono;
    public StoryManager Player;
    public BoxCollider box;
    public GameObject Tv;
    public BoxCollider sofaCol;
    public GameObject sofa;
    public FloorMaterial suelo1;
    public FloorMaterial suelo2;

    public StudioEventEmitter aguaDucha;

    public GameObject triggerDucha;
    public GameObject pared1;
    public GameObject pared2;
    public GameObject finalTrigger;

    private int roomsSeen = 0;
    private bool updated = false;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(!updated && roomsSeen == 4)
        {
            Telefono.SetActive(true);
            Player.IncreaseStoryStep(1);
            updated= true;
        }
        if (Player.GetStoryStep() == 3 && !sofaCol.enabled)
        {
            Player.setStoryStep(6);
            box.gameObject.SetActive(false);
            escenaAgua();
            suelo1.MaterialValue = 4;
            suelo2.MaterialValue = 4;
        }
    }

    public void increaseRooms()
    {
        roomsSeen++;
    }
    public void increaseStoryStep()
    {
        Player.IncreaseStoryStep(1);
    }
    public void bloquearPuerta()
    {
        box.gameObject.SetActive(true);
        Tv.SetActive(true);
        sofa.SetActive(true);
    }
    public void desbloquearPuerta()
    {
        box.gameObject.SetActive(false);
    }
    public void escenaAgua()
    {
        triggerDucha.SetActive(true);
        pared1.SetActive(true);
        pared2.SetActive(true);
        aguaDucha.OverrideMaxDistance = 40.0f;
        aguaDucha.OverrideMinDistance = 20.0f;
        finalTrigger.SetActive(true);

    }
    public void final()
    {
        Player.gameObject.transform.position= new Vector3 (100,100,100);
    }
}
