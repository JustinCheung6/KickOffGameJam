﻿using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a script for managing Fungus Flowcharts / Fungus dialogue
public class FungusChart : MonoBehaviour
{
    //Static properties
    private static Dictionary<FChartID, FungusChart> fungusDialogues = new Dictionary<FChartID, FungusChart>();
    private static FungusChart currentDialogue = null;

    //Properties

    [Header("Properties")]
    [SerializeField] private FChartID id = FChartID.none;

    [Header("Random Properties")]
    [Tooltip("Size: how many random 'rolls' flowchart needs. Value: max value of random range (min value = 1)")]
    [SerializeField] private int[] randDialogue;

    [Header("Object References")]
    [SerializeField] private Flowchart fungusFlowchart;

    #region External Script Values (Getters/Setters)
    
    //Used in GProgManager
    //Checks if any fungus flowchat is running dialogue (dialogue runs when fungus Flowchart is enabled)
    public static bool isRunningDialogue
    {
        get
        {
            return currentDialogue != null;
        }
    }
    
    //Used in GProgManager
    //Checks if FungusChart exists in dict
    public static bool HasFungusChart(FChartID fcID)
    {
        if(fcID == FChartID.none) 
            return false;

        return fungusDialogues.ContainsKey(fcID);
    }

    #endregion

    private void Awake()
    {
        #region Error Check
        bool errorShutdown = false;

        #region Error Case: Object References
        if (fungusFlowchart == null)
        {
            Debug.LogError($"fungusFlowchart has no reference: {gameObject.name}");
            errorShutdown = true;
        }

        #endregion

        //Error Case: Make sure flowchart is disabled On Scene Start
        if (fungusFlowchart.isActiveAndEnabled)
        {
            Debug.LogError($"Flowchart is activated on start: {gameObject.name}, {fungusFlowchart.name}");
            errorShutdown = true;
        }

        //Error Case: Check if Funguschart has an ID
        if (id == FChartID.none)
        {
            Debug.LogError($"Funguschat has no id: {gameObject.name}");
            errorShutdown = true;
        }
        else if(fungusDialogues.ContainsKey(id))
        {
            Debug.LogError($"Found multiple FungusCharts of ID: {id}.\nChart in Dict: {fungusDialogues[id].gameObject.name}\nChart Attempting to Add: {gameObject.name}");
            errorShutdown = true;
        }

        //Stop code if there's errors
        if (errorShutdown)
        {
            enabled = false;
            return;
        }
        #endregion

        //Add FungusChart to dictionary
        fungusDialogues.Add(id, this);
    }

    #region static Methods
    public static bool StartDialogue(FChartID fungusChart, bool overrideDialogue = false)
    {
        #region Error Check
        //Error Case: fungus chart is none
        if (fungusChart == FChartID.none)
        {
            Debug.LogError("No ID Given");
            return false;
        }
            

        //Error Case: fungus chart does not exist
        if (fungusDialogues[fungusChart] == null)
        {
            Debug.LogError($"fungusDialogues Dict does not have id: {fungusChart}");
            return false;
        }

        //Error Case: Dialogue already running
        if (currentDialogue != null)
            return false;

        #endregion

        return fungusDialogues[fungusChart].StartDialogue();
    }
    #endregion

    #region Dialogue Methods
    private bool StartDialogue()
    {
        //Setup flowchart to start running
        currentDialogue = this;
        SetupFungusVar();

        fungusFlowchart.enabled = true;

        StartCoroutine(WaitForDialogue());
        return true;
    }
    private IEnumerator WaitForDialogue()
    {
        //Wait until Dialogue ends (determined by Running variable)
        while (fungusFlowchart.GetBooleanVariable("Running"))
        {
            yield return new WaitForEndOfFrame();
        }

        //Disable flowchart running
        fungusFlowchart.enabled = false;
        currentDialogue = null;
    }

    //Update variables in fungus flowchart
    private void SetupFungusVar()
    {
        //Check if it's first run using TimeManager. 
        if (TimeManager.I != null && fungusFlowchart.HasVariable("FirstRun"))
            fungusFlowchart.SetBooleanVariable("FirstRun", TimeManager.I.IsFirstRun);

        //Setup random variables
        for (int i = 0; i < randDialogue.Length; i++)
        {
            string varName = $"Rand{i + 1}";

            if (!fungusFlowchart.HasVariable(varName))
            {
                Debug.LogError($"Fungus Flowchart does not have matching randDialogue: {gameObject.name}, id: {id}");
                continue;
            }
            if (randDialogue[i] < 2)
            {
                Debug.LogError($"randDialogue is too small at {i}: {gameObject.name}, id: {id}");
                continue;
            }

            int randValue = Random.Range(1, randDialogue[i]);

            fungusFlowchart.SetIntegerVariable(varName, randValue);
        }

        //Finish by updating Running
        fungusFlowchart.SetBooleanVariable("Running", true);
    }
    #endregion

}

//List of all FungusCharts present in the game
public enum FChartID
{
    OpeningDialogue,
    none = -1,
}
