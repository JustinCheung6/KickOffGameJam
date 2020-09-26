using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager singleton;

    [SerializeField] private bool paused = false;
    [SerializeField] private float time = 0f;
    [SerializeField] private Text timerText = null; 

    public float GetTime { get => time; }

    private void Start()
    {
        if (TimeManager.singleton == null)
            TimeManager.singleton = this;
    }

    private void Update()
    {
        if (!paused)
        {
            time += Time.deltaTime;
            if (timerText != null)
                timerText.text = "Time: " + (int)time;
        }

        if (time >= 60f)
            RestartDay();

    }

    public void RestartDay()
    {
        ProgressManager.singleton.ClearProgress();
        time = 0;
    }
}
