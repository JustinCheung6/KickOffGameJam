using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class TimeManager : MonoBehaviour
{
    public static TimeManager singleton;
    [SerializeField] private Flowchart FC;

    [SerializeField] private float time = 0f;
    [SerializeField] private Text timerText = null;

    public bool Paused { get => FC.GetBooleanVariable("paused"); }
    public float GetTime { get => time; }

    private void Start()
    {
        TimeManager.singleton = this;

        FC = GetComponent<Flowchart>();
    }

    private void Update()
    {
        if (!Paused)
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
