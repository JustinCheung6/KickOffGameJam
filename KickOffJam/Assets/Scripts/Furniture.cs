using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private SpriteRenderer sprite = null;

    [SerializeField] private ProgressManager.Scenarios id;
    private bool inRange = false;


    [Tooltip("Time when you can activate this furnature")]
    [SerializeField] private int timeFrame = -1;
    [Tooltip("How long does this event last for? (starting at timeFrame)")]
    [SerializeField] private int duration = 1;

    private void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
            sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Interact") && inRange && !ProgressManager.singleton.InDialogue && 
        TimeManager.singleton.GetTime >= timeFrame && TimeManager.singleton.GetTime <= timeFrame + duration)
            ProgressManager.singleton.Activate(id);

        if (TimeManager.singleton.GetTime >= timeFrame && TimeManager.singleton.GetTime <= timeFrame + duration)
            sprite.enabled = true;
        else
            sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        inRange = true;

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        inRange = false;
    }
}
