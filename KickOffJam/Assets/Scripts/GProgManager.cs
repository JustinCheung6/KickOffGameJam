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
    //Win Screen
    [SerializeField] private MovingColorChangingCoolScript winAnim = null;
    [SerializeField] private RectTransform winScreen = null;

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
        while (PlayerProgTracker.I == null)
        {
            yield return new WaitForEndOfFrame();
        }

        PlayerProgTracker.I.ResetProgress(TimeManager.I.IsFirstRun);

        //Wait until opening dialogue is ready
        while (!FungusChart.HasFungusChart(FChartID.OpeningDialogue))
        {
            yield return new WaitForEndOfFrame();
        }

        if (playerMove != null)
        {
            playerMove.ResetPlayerPos();
        }

        if (TimeManager.I.IsFirstRun)
        {
            //Have different transition when  its first day
            yield return new WaitForSeconds(0.5f);

            if (!FungusChart.StartDialogue(FChartID.OpeningDialogue))
            {
                Debug.LogError("OpeningDialogue failed");
                yield break;
            }

            yield return new WaitForSeconds(0.2f);
            BlackoutUI.FadeOutBlack();
        }
        else
        {
            //Wait Until game fades in
            BlackoutUI.FadeOutBlack();

            //Run Opening dialogue
            if (!FungusChart.StartDialogue(FChartID.OpeningDialogue))
            {
                Debug.LogError("OpeningDialogue failed");
                yield break;
            }
        }

        

        //Start timeMananger after opening dialogue finishes
        while (FungusChart.isRunningDialogue || BlackoutUI.IsRunning)
        {
            yield return new WaitForEndOfFrame();
        }

        TimeManager.I.StartTimer();
        StartCoroutine(WaitForNextState());
    }
    //Wait until Day Resets or Player Wins
    private IEnumerator WaitForNextState()
    {
        //When one of these values change, the player wins or time is up
        while (!TimeManager.I.TimedOut && !PlayerProgTracker.I.CheckWin())
        {
            yield return new WaitForEndOfFrame();
        }

        
        if (PlayerProgTracker.I.CheckWin())
        {
            StartCoroutine(WinGame());
        }
        else if (TimeManager.I.TimedOut)
            StartCoroutine(ResetDay());
    }

    //
    private IEnumerator WinGame()
    {
        //Wait until Fungus finished its current dialogue
        while (FungusChart.isRunningDialogue)
        {
            yield return new WaitForEndOfFrame();
        }

        //Start Chuckfish dialogue (to make sure timer does make the player lose
        FungusChart.StartDialogue(FChartID.ChuckFish);
        while (FungusChart.isRunningDialogue)
        {
            yield return new WaitForEndOfFrame();
        }

        //Start The finale dialogue
        BlackoutUI.FadeToBlack();
        FungusChart.StartDialogue(FChartID.WinGame);
        //Stop Timer if still going
        TimeManager.I.ResetTimer();

        //Wait until win finishes
        while (FungusChart.isRunningDialogue || BlackoutUI.IsRunning)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.2f);

        //Start Win Game Screen
        Debug.Log("GAME WON");
        winAnim.enabled = true;
        winScreen.gameObject.SetActive(true);
    }

    //Reset Day
    private IEnumerator ResetDay()
    {
        //Wait until Fungus finished its current dialogue
        while (FungusChart.isRunningDialogue)
        {
            yield return new WaitForEndOfFrame();
        }

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
