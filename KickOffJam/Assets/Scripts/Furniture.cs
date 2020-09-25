using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private static TimeManager timeSingleton;
    private static ProgressManager progressSingleton;
    
    [SerializeField] private ProgressManager.Scenarios id;

    [Tooltip("Time when you can activate this furnature")]
    [SerializeField] private int timeFrame = -1;
    [Tooltip("How long does this event last for? (starting at timeFrame)")]
    [SerializeField] private int duration = 1;

    private void Start()
    {
        if (timeSingleton == null)
            timeSingleton = GetComponent<TimeManager>();
        if (progressSingleton == null)
            progressSingleton = GetComponent<ProgressManager>();
    }

    virtual protected void Activate()
    {
        
    }

}
