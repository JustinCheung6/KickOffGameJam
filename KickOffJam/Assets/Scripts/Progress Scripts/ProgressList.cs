using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps tracks of player progress and dialogue progress requirements
public class ProgressList : MonoBehaviour
{
    [Header("Progression")]
    [SerializeField] protected N_bool firstRun = N_bool.Null;

    [SerializeField] protected N_event spatCouchEvent = N_event.Null;
    [SerializeField] protected N_eventDialogue sceneryEvent = N_eventDialogue.Null;

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
}

#region enumerations for inspector
//Nullable event trigger (2 states)
public enum N_event
{
    True = 0,

    Null = -1,
}
//Custom event triggers
public enum N_eventDialogue
{
    Dialogue2nd, 
    Dialogue3rd, 
    Dialogue4th,
    Dialogue5th,
    Dialogue6th,

    Finished,
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

//Custom nullable 4 state variable (5 states)
public enum N_fishItem
{
    HasItem = 0,
    NotHave = 1,
    LostItem = 2,
    InCouch = 3,

    Null = -1,
}
#endregion