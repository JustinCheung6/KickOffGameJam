using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Contains dictionary that checker and updater both use.
public class DialogueProgress : ProgressList
{
    //List all important progression items (makes checking and updating quicker if item pool increases 
    protected Dictionary<ProgItems, string> progItemValues = new Dictionary<ProgItems, string>();
    protected void SetupProgDict()
    {
        progItemValues.Clear();

        if(firstRun != N_firstDay.Null)
            progItemValues.Add(ProgItems.firstRun, firstRun.ToString());
        if(spatCouchEvent != N_bool.Null)
            progItemValues.Add(ProgItems.spatCouchEvent, spatCouchEvent.ToString());
        if(doorKey != N_keyItem.Null)
            progItemValues.Add(ProgItems.doorKey, doorKey.ToString());
        if(fish != N_fishItem.Null)
            progItemValues.Add(ProgItems.fish, fish.ToString());
        if(cupOfWater != N_keyItem.Null)
            progItemValues.Add(ProgItems.cupOfWater, cupOfWater.ToString());
        if (sceneryEvent != N_eventDialogue.Null)
            progItemValues.Add(ProgItems.sceneryEvent, sceneryEvent.ToString());
        if(doorEvent != N_eventDialogue.Null)
            progItemValues.Add(ProgItems.doorEvent, doorEvent.ToString());
        if(openedDoor != N_bool.Null)
            progItemValues.Add(ProgItems.openedDoorEvent, openedDoor.ToString());
        if(openedDrawer != N_bool.Null)
            progItemValues.Add(ProgItems.openedDrawerEvent, openedDrawer.ToString());
    }


    #region Updating progItemValues

    protected virtual void Awake()
    {
        SetupProgDict();
    }

    //Updates progItemValues if the variables are changed in the inspector (during playmode only

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!Application.isPlaying)
            return;

        SetupProgDict();
    }
#endif

    #endregion
}
