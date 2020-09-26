using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager singleton;

    [SerializeField] private bool paused = false;
    [SerializeField] private float time = 0f;

    public float GetTime { get => time; }

    private void Start()
    {
        if (TimeManager.singleton == null)
            TimeManager.singleton = this;
    }

    private void Update()
    {
        if (!paused)
            time += Time.deltaTime;

        if (time >= 60f)
            RestartDay();

    }

    public void RestartDay()
    {
        ProgressManager.singleton.ClearProgress();
        time = 0;
    }
}
