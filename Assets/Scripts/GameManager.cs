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
    }
}
