using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FungusFunctionTesting : MonoBehaviour
{

    [SerializeField] private Flowchart FC = null;

    [SerializeField] private int score = 0;

    private void Update()
    {
        if(FC.GetBooleanVariable("Activate"))
        {
            score++;
            FC.SetBooleanVariable("Activate", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("We in");

        if (FC != null)
            FC.ExecuteBlock("The Start of Stuff");
    }
}
