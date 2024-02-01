using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProgChecker : ProgressList
{
    [Header("Not Important to Code")]
    [SerializeField] private string notes = "";


    /// <summary>Checks if player progress meets requirements of this checker
    /// (if Checker value is null, it's ignored)
    /// </summary>
    /// <param name="p">Player Progress Tracker</param>
    /// <returns>true if player progress meets the requirements</returns>
    public bool CheckProgress(PlayerProgTracker p)
    {
        if (firstRun != N_bool.Null && firstRun != p.IsFirstRun)
            return false;
        if(doorKey != N_keyItem.Null && doorKey != p.HasDoorKey)
            return false;
        if(fish != N_keyItem.Null && fish != p.HasFish)
            return false;
        if (cupOfWater != N_keyItem.Null && cupOfWater != p.HasWaterCup)
            return false;

        return true;
    }
}
