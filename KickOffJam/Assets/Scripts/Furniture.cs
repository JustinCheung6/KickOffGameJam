using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{    
    [SerializeField] private ProgressManager.Scenarios id;
    private bool inRange = false;


    [Tooltip("Time when you can activate this furnature")]
    [SerializeField] private int timeFrame = -1;
    [Tooltip("How long does this event last for? (starting at timeFrame)")]
    [SerializeField] private int duration = 1;

    private void Update()
    {
        if(Input.GetButtonDown("Interact") && inRange && !ProgressManager.singleton.InDialogue)
            ProgressManager.singleton.Activate(id);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;

    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
