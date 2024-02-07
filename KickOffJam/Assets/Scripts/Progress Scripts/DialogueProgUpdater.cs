using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProgUpdater : DialogueProgress
{
    [Header("Not Important to Code")]
    [SerializeField] private string notes = "";


    /// <summary>Changes values of PlayerProg Tracker (updates their progress)
    /// (if Updater value is null, it's ignored)
    /// </summary>
    /// <param name="p">Player Progress Tracker</param>
    /// <returns>true if player progress meets the requirements</returns>
    public void UpdateProgress(PlayerProgTracker p)
    {
        
        foreach (KeyValuePair<ProgItems, string> item in progItemValues)
        {
            switch (item.Key)
            {
                case ProgItems.firstRun:
                    if (firstRun != N_bool.Null)
                        p.UpdateFirstRun(firstRun);
                    break;
                case ProgItems.spatCouchEvent:
                    if (spatCouchEvent != N_bool.Null && spatCouchEvent != p.SpatCouchEvent)
                        p.UpdateSpatCouchEvent(spatCouchEvent);
                    break;
                case ProgItems.doorKey:
                    if (doorKey != N_keyItem.Null && doorKey != p.HasDoorKey)
                        p.UpdateDoorKey(doorKey);
                    break;
                case ProgItems.fish:
                    if (fish != N_fishItem.Null && fish != p.HasFish)
                        p.UpdateFish(fish);
                    break;
                case ProgItems.cupOfWater:
                    if (cupOfWater != N_keyItem.Null && cupOfWater != p.HasWaterCup)
                        p.UpdateWaterCup(cupOfWater);
                    break;

            }
        }
    }
}
