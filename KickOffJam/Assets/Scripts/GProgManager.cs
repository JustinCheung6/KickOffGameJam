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

    [Header("Object References")]
    [SerializeField] private PlayerMovement playerMove = null;

    private void Awake()
    {
        //Setup singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Debug.LogError($"Found multiple instances of ProgressMananger:\nCurrent instance: {instance.gameObject.name},\nThis instance: {gameObject.name}");

        StopAllCoroutines();
    }
    private void Start()
    {
        PrepareNewDay();
    }

    #region Game Event Methods

    //Game Start
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

        if(playerMove != null)
        {
            playerMove.ResetPlayerPos();
        }

        //Wait Until game fades in
        BlackoutUI.FadeOutBlack();
        while (BlackoutUI.IsRunning)
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
        StartCoroutine(WaitForNextState());
    }
    //Wait until Day Resets or Player Wins
    private IEnumerator WaitForNextState()
    {
        while (!TimeManager.I.TimedOut && !PlayerProgTracker.I.CheckWin())
        {
            yield return new WaitForEndOfFrame();
        }

        if(TimeManager.I.TimedOut)
            StartCoroutine(ResetDay());
        else if (PlayerProgTracker.I.CheckWin())
        {
            StartCoroutine(WinGame());
        }
    }

    //Game Win
    private IEnumerator WinGame()
    {
        while (FungusChart.isRunningDialogue)
        {
            yield return new WaitForEndOfFrame();
        }

        BlackoutUI.FadeToBlack();
        FungusChart.StartDialogue(FChartID.WinGame);
        while (FungusChart.isRunningDialogue || BlackoutUI.IsRunning)
        {
            yield return new WaitForEndOfFrame();
        }


        Debug.Log("YOU WIN");
    }

    //Reset Day
    private IEnumerator ResetDay()
    {
        BlackoutUI.FadeToBlack();
        FungusChart.StartDialogue(FChartID.ResetDay);

        while (FungusChart.isRunningDialogue || BlackoutUI.IsRunning)
        {
            yield return new WaitForEndOfFrame();
        }

        PrepareNewDay();
    }
    private void PrepareNewDay()
    {
        StopAllCoroutines();
        StartCoroutine(StartGame());
    }

    #endregion


}
