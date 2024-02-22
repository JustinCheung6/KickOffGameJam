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
    public N_firstDay IsFirstRun
    {
        get => firstRun;
    }
    public N_bool SpatCouchEvent
    {
        get => spatCouchEvent;
    }
    public N_eventDialogue SceneryEvent
    {
        get => sceneryEvent;
    }
    public N_bool OpenedDrawerEvent
    {
        get => openedDrawer;
    }
    public N_eventDialogue OpenedDoorEvent
    {
        get => openedDoorEvent;
    }
    public N_eventDialogue DoorEvent
    {
        get => doorEvent;
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
    public void UpdateFirstRun(N_firstDay b)
    {
        firstRun = b;
    }
    public void UpdateSpatCouchEvent(N_bool e)
    {
        spatCouchEvent = e;
    }
    public void UpdateOpenedDrawerEvent(N_bool e)
    {
        openedDrawer = e;
    }
    public void UpdateOpenedDoorEvent(N_eventDialogue e)
    {
        openedDoorEvent = e;
    }
    public void UpdateSceneryEvent(N_eventDialogue e)
    {
        sceneryEvent = e;
    }
    public void UpdateDoorEvent(N_eventDialogue e)
    {
        doorEvent = e;
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

    //Used in GProgManager
    public bool Door2EventReady
    {
        get => openedDoorEvent == N_eventDialogue.Dialogue2nd;
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
        firstRun = (firstDay) ? N_firstDay.FirstDay : (firstRun == N_firstDay.FirstDay) ? N_firstDay.SecondDay : N_firstDay.False;

        spatCouchEvent = N_bool.False;
        openedDrawer = N_bool.False;

        openedDoorEvent = N_eventDialogue.Beginning;
        sceneryEvent = N_eventDialogue.Beginning;
        doorEvent = N_eventDialogue.Beginning;

        doorKey = N_keyItem.NotHave;
        fish = N_fishItem.NotHave;
        cupOfWater = N_keyItem.NotHave;
    }


    //Check if player has won
    public bool CheckWin()
    {
        return (fish == N_fishItem.Chucked);
    }

    //True if there is a null value
    private bool CheckNull()
    {
        //Make sure none of the Player ProgressList is null
        if (firstRun == N_firstDay.Null || doorKey == N_keyItem.Null || fish == N_fishItem.Null || cupOfWater == N_keyItem.Null)
        {
            Debug.LogError("PlayerProgTracker has Null values");
            return true;
        }
        return false;
    }
}
