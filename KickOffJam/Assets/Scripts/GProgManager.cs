using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

//Game Progression Manager
//Controls all game events in the game. Most other managers calls methods here
public class GProgManager : MonoBehaviour
{
    //Singleton
    private static GProgManager instance;
    public static GProgManager I
    {
        get
        {
            if(instance == null)
                instance = FindObjectOfType<GProgManager>();

            return instance;
        }
    }

    //[Header("Object References")]

    private void Awake()
    {
        //Setup singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Debug.LogError($"Found multiple instances of ProgressMananger:\nCurrent instance: {instance.gameObject.name},\nThis instance: {gameObject.name}");
    }
    private void Start()
    {
        //Start the Game in a Coroutine
        StartCoroutine(StartGame());
        
    }

    #region Game Event Methods
    private IEnumerator StartGame()
    {
        //Reset Player Progress
        while(PlayerProgTracker.I == null) 
        {
            yield return new WaitForEndOfFrame();
        }
        PlayerProgTracker.I.ResetProgress(TimeManager.I.IsFirstRun);

       //Wait until opening dialogue is ready
       while (!FungusChart.HasFungusChart(FChartID.OpeningDialogue))
        {
            yield return new WaitForEndOfFrame();
        }
       //Run Opening dialogue
       if (!FungusChart.StartDialogue(FChartID.OpeningDialogue))
        {
            Debug.LogError("OpeningDialogue failed");
            yield break;
        }

        //Start timeMananger after opening dialogue finishes
        while (FungusChart.isRunningDialogue)
        {
            yield return new WaitForEndOfFrame();
        }


        TimeManager.I.StartTimer();
    }

    #endregion


}
