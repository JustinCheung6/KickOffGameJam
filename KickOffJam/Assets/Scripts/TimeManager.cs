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
    private bool isRunning = false;
    private float timer = 0f;
    private bool firstRun = true;

    [Header("Object References")]
    [SerializeField] Text timerUI = null;


    #region External Script Values (Getters/Setters)
    //Used in FungusChart
    public bool IsFirstRun
    {
        get => firstRun;
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

    public void Update()
    {
        if (!isRunning)
        {
            if (timerUI != null)
                timerUI.gameObject.SetActive(false);
            return;
        }

        //Update timerUI Display if available
        if(timerUI != null && !firstRun) 
        {
            timerUI.text = Mathf.FloorToInt(timer).ToString();
            timerUI.gameObject.SetActive(true);
        }

        timer += Time.deltaTime;
    }

    //Have timer start incrementing
    public void StartTimer()
    {
        isRunning = true;
    }
    //Reset timer value and stop it
    public void ResetTimer()
    {
        isRunning = false;
        timer = 0f;
    }
}
