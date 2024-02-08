using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Track player progress list
public class PlayerProgTracker : ProgressList
{
    //Singleton
    private static PlayerProgTracker instance = null;
    public static PlayerProgTracker I
    {
        get
        {
            if(instance != null)
                return instance;

            foreach (PlayerProgTracker p in FindObjectsOfType<PlayerProgTracker>())
            {
                if (p.CheckNull())
                {
                    p.enabled = false;
                    continue;
                }

                instance = p;
                return instance;
            }

            return instance;
        }
    }

    #region External Script Values (Getters/Setters)
    //Used in DialogueProgChecker & DialogueProgUpdater
    public N_bool IsFirstRun
    {
        get => firstRun;
    }
    public N_event SpatCouchEvent
    {
        get => spatCouchEvent;
    }
    public N_eventDialogue SceneryEvent
    {
        get => sceneryEvent;
    }

    public N_keyItem HasDoorKey
    {
        get => doorKey;
    }
    public N_fishItem HasFish
    {
        get => fish;
    }
    public N_keyItem HasWaterCup
    {
        get => cupOfWater;
    }

    //Used in DialogueProgUpdater
    public void UpdateFirstRun(N_bool b)
    {
        firstRun = b;
    }
    public void UpdateSpatCouchEvent(N_event e)
    {
        spatCouchEvent = e;
    }
    public void UpdateSceneryEvent(N_eventDialogue e)
    {
        sceneryEvent = e;
    }
    public void UpdateDoorKey(N_keyItem k)
    {
        doorKey = k;
    }
    public void UpdateFish(N_fishItem k)
    {
        fish = k;
    }
    public void UpdateWaterCup(N_keyItem k)
    {
        cupOfWater = k;
    }
    #endregion

    private void OnEnable()
    {
        if (CheckNull())
        {
            enabled = false;
            return;
        }

        if (instance == null)
            instance = this;
        else if (instance != this)
            Debug.LogError($"Found multiple PlayerProgTracker.\n" +
                $"Adding object: {gameObject.name}\nExisting object: {instance.gameObject.name}");

    }

    public void ResetProgress(bool firstDay)
    {
        firstRun = (firstDay) ? N_bool.True : N_bool.False;
        spatCouchEvent = N_event.Null;
        sceneryEvent = N_eventDialogue.Null;
        doorKey = N_keyItem.NotHave;
        fish = N_fishItem.NotHave;
        cupOfWater = N_keyItem.NotHave;
    }

    //True if there is a null value
    private bool CheckNull()
    {
        //Make sure none of the Player ProgressList is null
        if (firstRun == N_bool.Null || doorKey == N_keyItem.Null || fish == N_fishItem.Null || cupOfWater == N_keyItem.Null)
        {
            Debug.LogError("PlayerProgTracker has Null values");
            return true;
        }
        return false;
    }
}
