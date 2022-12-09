using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference FootstepsEventPath;        // Use this in the Editor to select our Footsteps Event.
    [SerializeField] private string MaterialParameterName;                      // Use this in the Editor to write the name of the parameter that contorls which material the player is currently walking on.

    [Header("Playback Settings")]
    [SerializeField] private float StepDistance = 2.0f;                         // Select how far the player must travel before they hear a footstep. This will then remain a constant and will not change.
    [SerializeField] private float RayDistance = 1.2f;                          // Select how far the raycast will travel down to when checking for a floor. This will then remain a constant and will not change.
    public string[] MaterialTypes;                                              // This is an array of strings. In the inspector we can decide how many Material types we have in FMOD by setting the size of this array. Depending on the size, the array will then create a certain amount of strings for us to fill in with the name of each of our footstep materials for our scripts to use. This will then remain a constant and will not change.
    [HideInInspector] public int DefulatMaterialValue;                          // This will be told by the 'FMODStudioFootstepsEditor' script which Material has been set as the defualt. It will then store the value of that Material for outhis script to use. This cannot be changed in the Editor, but a drop down menu created by the 'FMODStudioFootstepsEditor' script can.

    private float StepRandom;                                                   // This will be set as random number, which will later be added to the StepDistance to add a little variaiton to the length in steps.
    private Vector3 PrevPos;                                                    // This will old the co-ordinates of the previous postion the player was in during the last frame.
    private float DistanceTravelled;                                            // This will hold a value that how represnt how far the player has travlled since they last took a step.
    //These variables are used when checking the Material type the player is on top of.
    private RaycastHit hit;                                                     // Will holds information about the GameObject that the raycast hits.
    private int F_MaterialValue;                                                // We'll use this to set the value of our FMOD Material Parameter.

    // Start is called before the first frame update
    void Start()
    {
        StepRandom = Random.Range(0f, 0.5f);
        PrevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * RayDistance, Color.blue);

        DistanceTravelled += (transform.position - PrevPos).magnitude;       // Every frame, the co-ordinates that the player is currently at is reduced by the value of the co-ordinates they were at during the previous frame. This gives us the length between the two points as a new set of co-ordinates (AKA a vector). That length is then tunred into a single number by '.magnitude' and then finally added onto the value of the DistanceTravelled float. Now we know how far the player has travlled!
        if (DistanceTravelled >= StepDistance + StepRandom)                  // If the distance the player has travlled is greater than or equal to the StepDistance plus the StepRandom, then we can perform our methods.
        {
            MaterialCheck();                                                 // The MaterialCheck method is perfomred. This checks for a material value and updates the 'F_MaterialValue' variable with for our 'PlayFootstep()' method to use.
            PlayFootstep();                                                  // The PlayFootstep method is performed and a footstep audio file from FMOD is played!
            StepRandom = Random.Range(0f, 0.5f);                             // Now that our footstep has been played, this will reset 'StepRandom' and give it a new random value between 0 and 0.5, in order to make the distance the player has to travel to hear a footstep different from what it previously was.
            DistanceTravelled = 0f;                                          // Since the player has just taken a step, we need to set the 'DistanceTravelled' float back to 0.
        }
        PrevPos = transform.position;

    }

    void MaterialCheck() // This method when performed will find out what material our player is currenly on top of and will update the value of 'F_MaterialValue' accordingly, to represent that value.
    {
        //Debug.Log("Material Check");
        if (Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance))                                 // A raycast is fired down, from the position that the player is curenntly standing at, traveling as far as we decide to set the 'RayDistance' variable to. Infomration about the object it comes into contact with will then be stored inside the 'hit' variable for us to access.
        {
            if (hit.collider.gameObject.GetComponent<FloorMaterial>())                                    // Using the 'hit' varibale, we check to see if the raycast has hit a collider attached to a gameobject, that also has the 'FMODStudioMaterialSetter' script attached to it as a component...
            {
                F_MaterialValue = hit.collider.gameObject.GetComponent<FloorMaterial>().MaterialValue;    // ...and if it did, we then set our 'F_MaterialValue' varibale to match whatever value the 'MaterialValue' variable (which is inside the 'F_MaterialValue' varibale) is currently set to.
                //Debug.Log(F_MaterialValue);
            }
            else                                                                                                     // Else if however, the player is standing on an object that doesn't have a 'FMODStudioMaterialSetter' script component for our raycast to find...
                F_MaterialValue = DefulatMaterialValue;                                                              // ...we then set 'F_MaterialValue' to match the value of 'DefulatMaterialValue'. 'DefulatMaterialValue' is given a value by the 'FMODStudioFootstepsEditor' script. This value represents whatever material we have selected as our 'DefulatMaterial' in the Unity Inspector tab.
        }
        else                                                                                                         // Else if however, the raycast can't find a collider attached to the object at all...
            F_MaterialValue = DefulatMaterialValue;                                                                  // Then again, we set 'F_MaterialValue' to match the value of 'DefulatMaterialValue'.
    }

    void PlayFootstep() // When this method is performed, our footsteps event in FMOD will be told to play.
    {
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(FootstepsEventPath);        // If they are, we create an FMOD event instance. We use the event path inside the 'FootstepsEventPath' variable to find the event we want to play.
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());     // Next that event instance is told to play at the location that our player is currently at.
        Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);                                     // Before the event is played, we set the Material Parameter to match the value of the 'F_MaterialValue' variable.
        Footstep.start();                                                                                        // We then play a footstep!.
        Footstep.release();
    }
}
