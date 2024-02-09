using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

//Internal game timer that calls GProgManager events at certain times
public class TimeManager : MonoBehaviour
{
    //Singleton
    private static TimeManager instance = null;
    public static TimeManager I
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TimeManager>();

            return instance;
        }
    }

    //Properties
    private bool firstRun = true;
    private bool isRunning = false;
    private bool timeOut = false;
    private float timer = 0f;

    [Header("Properties")]
    [Tooltip("How much time will pass before the day resets")]
    [SerializeField] private float maxTime = 60f;

    [Header("Object References")]
    [SerializeField] Text timerUI = null;


    #region External Script Values (Getters/Setters)
    //Used in FungusChart
    public bool IsFirstRun
    {
        get => firstRun;
    }

    //Used in GProgManager
    public bool TimedOut
    {
        get => timeOut;
    }
    //Used in PlayerMovement
    public bool IsRunning
    {
        get => isRunning;
    }


    #endregion

    private void Awake()
    {
        //Warning Case: Object Reference
        if (timerUI == null)
            Debug.Log("Warning: timerUI has not reference");

        //Setup singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Debug.LogError($"Found multiple instances of TimeManager:\nCurrent instance: {instance.gameObject.name},\nThis instance: {gameObject.name}");

        firstRun = true;
        ResetTimer();
    }

    public void LateUpdate()
    {
        if (!isRunning)
        {
            return;
        }

        //Update timerUI Display if available
        if(timerUI != null) 
        {
            timerUI.text = Mathf.FloorToInt(timer).ToString();
        }

        timer += Time.deltaTime;


        //Check if time limit reached (reset day)
        if(timer > maxTime)
        {
            timeOut = true;
            firstRun = false;
            ResetTimer();
        }
        
    }

    //Have timer start incrementing
    public void StartTimer()
    {
        Debug.Log("Starting Timer");

        if (timerUI != null)
            timerUI.gameObject.SetActive(true);
        timeOut = false;
        isRunning = true;
    }
    //Reset timer value and stop it
    public void ResetTimer()
    {
        isRunning = false;
        timer = 0f;
        if (timerUI != null)
            timerUI.gameObject.SetActive(false);
    }
}
