using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps tracks of player progress and dialogue progress requirements
public class ProgressList : MonoBehaviour
{
    [Header("Progression")]
    [SerializeField] protected N_firstDay firstRun = N_firstDay.Null;

    [SerializeField] protected N_bool spatCouchEvent = N_bool.Null;
    [SerializeField] protected N_bool openedDrawer = N_bool.Null;
    [SerializeField] protected N_eventDialogue openedDoorEvent = N_eventDialogue.Null;
    [SerializeField] protected N_eventDialogue sceneryEvent = N_eventDialogue.Null;
    [SerializeField] protected N_eventDialogue doorEvent = N_eventDialogue.Null;

    [SerializeField] protected N_keyItem doorKey = N_keyItem.Null;
    [SerializeField] protected N_fishItem fish = N_fishItem.Null;
    [SerializeField] protected N_keyItem cupOfWater = N_keyItem.Null;
}

//Used to get references of items
public enum ProgItems
{
    firstRun,
    spatCouchEvent,
    sceneryEvent,
    doorKey, 
    fish, 
    cupOfWater,
    openedDrawerEvent,
    openedDoorEvent,
    doorEvent,
}

#region enumerations for inspector
//Custom event triggers
public enum N_eventDialogue
{
    Beginning,
    Finished,

    Dialogue2nd, 
    Dialogue3rd, 
    Dialogue4th,
    Dialogue5th,
    Dialogue6th,
    Dialogue7th,
    Dialogue8th,
    Dialogue9th,
    Dialogue10th,
    Dialogue11th,

    Null = -1,
}

//Nullable boollean (3 states)
public enum N_bool
{
    True = 0,
    False = 1,

    Null = -1,
}

//Nullable 3 state variable (4 states)
public enum N_keyItem
{
    HasItem = 0,
    NotHave = 1,
    LostItem = 2,

    Null = -1,
}

//Custom nullable variables
public enum N_fishItem
{
    HasItem = 0,
    NotHave = 1,
    LostItem = 2,
    InCouch = 3,
    Chucked = 4,

    Null = -1,
}
public enum N_firstDay
{
    FirstDay,
    SecondDay,
    False,

    Null,
}
#endregion