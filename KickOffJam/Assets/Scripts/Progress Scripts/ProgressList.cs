using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps tracks of player progress and dialogue progress requirements
public class ProgressList : MonoBehaviour
{
    [Header("Progression")]
    [SerializeField] protected N_bool firstRun = N_bool.Null;
    [SerializeField] protected N_keyItem doorKey = N_keyItem.Null;
    [SerializeField] protected N_keyItem fish = N_keyItem.Null;
    [SerializeField] protected N_keyItem cupOfWater = N_keyItem.Null;

}

#region enumerations for inspector
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
#endregion