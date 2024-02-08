﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProgChecker : DialogueProgress
{

    /// <summary>Checks if player progress meets requirements of this checker
    /// (if Checker value is null, it's ignored)
    /// </summary>
    /// <param name="p">Player Progress Tracker</param>
    /// <returns>true if player progress meets the requirements</returns>
    public bool CheckProgress(PlayerProgTracker p)
    {
        foreach (KeyValuePair<ProgItems, string> item in progItemValues)
        {
            switch (item.Key)
            {
                case ProgItems.firstRun:
                    if (firstRun != N_bool.Null && firstRun != p.IsFirstRun)
                        return false;
                    break;
                case ProgItems.spatCouchEvent:
                    if (spatCouchEvent != N_event.Null && spatCouchEvent != p.SpatCouchEvent)
                        return false;
                    break;
                case ProgItems.sceneryEvent:
                    if (sceneryEvent != N_eventDialogue.Null && sceneryEvent != p.SceneryEvent)
                        return false;
                    break;
                case ProgItems.doorKey:
                    if (doorKey != N_keyItem.Null && doorKey != p.HasDoorKey)
                        return false;
                    break;
                case ProgItems.fish:
                    if (fish != N_fishItem.Null && fish != p.HasFish)
                        return false;
                    break;
                case ProgItems.cupOfWater:
                    if (cupOfWater != N_keyItem.Null && cupOfWater != p.HasWaterCup)
                        return false;
                    break;

            }
        }

        return true;
    }
}