using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private bool paused = false;
    [SerializeField] private float time = 0f;

    public float GetTime { get => time; }

    private void Update()
    {
        if (!paused)
            time += Time.deltaTime;

        if (time >= 60f)
            RestartDay();

    }

    public void RestartDay()
    {
        time = 0;
    }
}
