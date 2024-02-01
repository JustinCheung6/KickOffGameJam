﻿using System.Collections;
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
    //Used in DialogueProgChecker
    public N_bool IsFirstRun
    {
        get => firstRun;
    }
    public N_keyItem HasDoorKey
    {
        get => doorKey;
    }
    public N_keyItem HasFish
    {
        get => fish;
    }
    public N_keyItem HasWaterCup
    {
        get => cupOfWater;
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
        doorKey = N_keyItem.NotHave;
        fish = N_keyItem.NotHave;
        cupOfWater = N_keyItem.NotHave;
    }

    //True if there is a null value
    private bool CheckNull()
    {
        //Make sure none of the Player ProgressList is null
        if (firstRun == N_bool.Null || doorKey == N_keyItem.Null || fish == N_keyItem.Null || cupOfWater == N_keyItem.Null)
        {
            Debug.LogError("PlayerProgTracker has Null values");
            return true;
        }
        return false;
    }
}
