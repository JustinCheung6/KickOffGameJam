using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls player movement
//Player movement is stopped when FungusChart has a dialogue running or if the game is paused
public class PlayerMovement : MonoBehaviour
{
    [Header("Propertiers")]
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private Vector2 startingPos = Vector2.zero;

    private void FixedUpdate()
    {
        //Stop movement if FungusChart has dialogue running
        if (FungusChart.isRunningDialogue)
        {
            return;
        }
        if(PauseMenuManager.I != null)
        {
            if (PauseMenuManager.I.GamePaused)
                return;
        }
        if(TimeManager.I != null)
        {
            if (!TimeManager.I.IsRunning)
                return;
        }   

        //Moves Forward and back along y axis                           //Up/Down
        transform.Translate(Vector3.up * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
        //Moves Left and right along x Axis                               //Left/Right
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);
    }
    
    public void ResetPlayerPos()
    {
        transform.position = startingPos;
    }
}
