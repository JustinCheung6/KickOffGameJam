﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager singleton;

    private List<Scenarios> events = new List<Scenarios>();
    private Items inventory = Items.nothing;

    [SerializeField] private Flowchart FC;
    private string blockName;
    private int itemID;
    
    public enum Scenarios
    {
        nothing = 0,
        bed = 1,
        ceilingLeaky = 2,
        chair = 3,
        couch = 4,
        cupboard = 5,
        door = 6,
        drawer = 7,
        floor = 8,
        fridge = 9,
        housePlant = 10,
        microwave = 11,
        window = 12
    }
    public enum Items
    {
        nothing,
        fish,
        cupFish,
        key
    }

    public bool InDialogue { get => FC.HasExecutingBlocks(); }

    private void Start()
    {
        ProgressManager.singleton = this;
    }

    private void Update()
    {
        if(FC.GetIntegerVariable("rewards") != 0)
        {
            if (FC.GetIntegerVariable("rewards") == 1)
                inventory = (Items)FC.GetIntegerVariable("ItemID");
            else if (FC.GetIntegerVariable("rewards") == 2)
                Win();
            else
            {
                FC.SetIntegerVariable("rewards", 0);
                FC.SetIntegerVariable("ItemID", 0);
            }
        }
    }

    public void Activate(Scenarios s)
    {
        if (events.Count >= 3)
            return;

        events.Add(s);

        //Reset variable names
        blockName = "";
        itemID = -1;

        if (events.Count == 1)
            SetDialogue(events[0]);
        else if (events.Count == 2)
            SetDialogue(events[0], events[1]);
        else if (events.Count == 3)
            SetDialogue(events[0], events[1], events[2]);

        if(blockName != "" && itemID != -1)
        {
            FC.SetIntegerVariable("ItemID", itemID);
            FC.ExecuteBlock(blockName);
        }
    }

    //Set dialogue for beginning statements
    private void SetDialogue(Scenarios s1)
    {
        // 2 (Leaky Ceiling)
        if (s1 == Scenarios.ceilingLeaky)
        {
            blockName = "Ceiling";
            itemID = (int)Items.fish;
        }
            
    }
    //Set dialogue for 2nd stage statements
    private void SetDialogue(Scenarios s1, Scenarios s2)
    {
        // 2.4 (Leaky Ceiling, Couch)
        if (s1 == Scenarios.ceilingLeaky && s2 == Scenarios.couch)
        {
            blockName = "CeilingCouch";
            itemID = (int)Items.cupFish;
        }
    }
    //Set dialogue for final statements
    private void SetDialogue(Scenarios s1, Scenarios s2, Scenarios s3)
    {
        //2.4.12 (Leaky Ceiling, Couch, Window)
        if (s1 == Scenarios.ceilingLeaky && s2 == Scenarios.couch && s3 == Scenarios.window)
        {
            blockName = "CeilingCouchWindow";
            itemID = 0;
        }
    }

    //Set dialogue for beginning statements
    private void SetDialogue(Items i)
    {
        blockName = "";

        if(i == Items.cupFish)
        {
            blockName = "CupFishRemove";
        }
        else if(i == Items.fish)
        {
            blockName = "FishRemove";
        }

        if (blockName != "")
            FC.ExecuteBlock(blockName);
    }


    public void ClearProgress()
    {
        SetDialogue(inventory);
        events = new List<Scenarios>();
        inventory = Items.nothing;
}
    private void Win()
    {
        SceneManager.LoadScene(0);
    }
}
