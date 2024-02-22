using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls player movement
//Player movement is stopped when FungusChart has a dialogue running or if the game is paused
public class PlayerMovement : MonoBehaviour
{
    private enum PlayerDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    }

    [Header("Properties")]
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private Vector2 startingPos = Vector2.zero;
    [Header("Animation Properties")]
    [SerializeField] private PlayerDirection startingDirection = PlayerDirection.Down;

    [Header("Object References")]
    [SerializeField] private Animator playerAnimator = null;
    private void FixedUpdate()
    {
        //Stop movement if FungusChart has dialogue running
        if (FungusChart.isRunningDialogue)
        {
            playerAnimator.SetFloat("AnimSpeed", 0f);
            return;
        }
        if(PauseMenuManager.I != null)
        {
            playerAnimator.SetFloat("AnimSpeed", 0f);

            if (PauseMenuManager.I.GamePaused)
                return;
        }
        if(TimeManager.I != null)
        {
            playerAnimator.SetFloat("AnimSpeed", 0f);

            if (!TimeManager.I.IsRunning)
                return;
        }

        //Move player
        float verticalSpeed = Time.fixedDeltaTime * Input.GetAxis("Vertical") * moveSpeed;
        float horizontalSpeed = Time.fixedDeltaTime * Input.GetAxis("Horizontal") * moveSpeed;
        transform.position += new Vector3(horizontalSpeed, verticalSpeed, 0f);

        //Update Animation
        if(verticalSpeed != 0)
        {
            playerAnimator.SetFloat("AnimSpeed", moveSpeed);

            if (verticalSpeed > 0f)
                playerAnimator.SetInteger("Dir", (int)PlayerDirection.Up);
            else if (verticalSpeed < 0f)
                playerAnimator.SetInteger("Dir", (int)PlayerDirection.Down);
        }
        else if (horizontalSpeed != 0)
        {
            playerAnimator.SetFloat("AnimSpeed", moveSpeed);

            if (horizontalSpeed > 0f)
                playerAnimator.SetInteger("Dir", (int)PlayerDirection.Right);
            else if (horizontalSpeed < 0f)
                playerAnimator.SetInteger("Dir", (int)PlayerDirection.Left);
        }

    }
    
    public void ResetPlayerPos()
    {
        transform.position = startingPos;

        if(playerAnimator != null)
        {
            playerAnimator.SetFloat("AnimSpeed", 0f);
            playerAnimator.SetInteger("Dir", (int)startingDirection);
        }
    }
}
