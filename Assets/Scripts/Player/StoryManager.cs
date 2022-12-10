using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference StoryEvent;

    private FMOD.Studio.EventInstance Story;

    private int storyStep;
    private bool stepCompleted = false;

    private void Start()
    {
        Story = FMODUnity.RuntimeManager.CreateInstance(StoryEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Story, transform, GetComponent<Rigidbody>());
        storyStep = 0;
    }

    private void Update()
    {
        
        if (!stepCompleted && storyStep == 0)
        {
            Story.setParameterByName("stepStory", storyStep);
            Story.start();

            stepCompleted = true;
        }
        if (!stepCompleted && storyStep == 1)
        {
            Story.setParameterByName("stepStory", storyStep);
            Story.start();

            stepCompleted = true;
        }
        if (!stepCompleted && storyStep == 2)
        {
            Story.setParameterByName("stepStory", storyStep);
            Story.start();

            stepCompleted = true;
        }
        if (!stepCompleted && storyStep == 3)
        {
            Story.setParameterByName("stepStory", storyStep);
            Story.start();

            stepCompleted = true;
        }
        if (!stepCompleted && storyStep == 6)
        {
            Story.setParameterByName("stepStory", storyStep);
            Story.start();

            stepCompleted = true;
        }
        if (!stepCompleted && storyStep == 7)
        {
            Story.setParameterByName("stepStory", storyStep);
            Story.start();

            stepCompleted = true;
        }
    }

    public int GetStoryStep() { return storyStep; }
    public void IncreaseStoryStep(int i) { 
        storyStep += i;
        stepCompleted = false;
    }

    public void setStoryStep(int i)
    {
        storyStep = i;
        stepCompleted = false;
    }
}
