using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls player movement
//Player movement is stopped when FungusChart has a dialogue running or if the game is paused (WIP)
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;


    private void Update()
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
            

        //Moves Forward and back along y axis                           //Up/Down
        transform.Translate(Vector3.up * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
        //Moves Left and right along x Axis                               //Left/Right
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);
    }
    
    
}
