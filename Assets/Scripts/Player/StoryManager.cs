using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private int storyStep;

    private void Start()
    {
        storyStep = 0;
    }

    public int GetStoryStep() { return storyStep; }
    public void IncreaseStoryStep(int i) { storyStep += i; }
}
