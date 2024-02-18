using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingColorChangingCoolScript : MonoBehaviour
{
    [Header("Properties")]
    #region Text Properties
    [SerializeField] private float txtSpeed = 80f;
    //Angle which the color and text will move
    private float txtAngle = 0;
    //How much can the angle shift after hitting hitting the border
    private float txtBounceRandom = 30f;
    #endregion

    #region Color Properties
    [SerializeField] private float colorSpeed = 3f;
    //Hue of the color in hsl value (ten thousandth) (Actual is 0 -> 360). 
    private float colorHue = 0f;
    //Direction of color change (-1 or 1)
    private int colorDir = 1;
    //Time before color bounces (increase or decrease)
    //Note: There's a 1/5 for color to bounce every second starting at 5 seconds before bounce timer hits (guarenteed bounce 5 seconds after timer time)
    private float colorBounceTime = 10f;


    //Trackers
    private float colorTimer = 0f;
    private bool changeColorDir = false;

    #endregion

    [Header("Object Reference")]
    [SerializeField] private Text winText;    
    private RectTransform txtRect;
    private RectTransform canvasRect;

    private void OnEnable()
    {
        if(winText == null)
        {
            Debug.LogError("winText has no reference: " + gameObject.name);
            enabled = false;
        }

        if(txtRect == null)
            txtRect = winText.rectTransform;
        if(canvasRect == null)
            canvasRect = winText.canvas.GetComponent<RectTransform>();

        //Get random value for angle (round nearest tenths)
        txtAngle = Mathf.Round(Random.value * 360f * 10f) * 0.1f;

        //Get random value for position (round nearest hundredth)
        Vector2 border = GetTxtBorderSize();
        txtRect.anchoredPosition = new Vector2
            (
                (Mathf.Round(border.x * Random.value * 100f) * 0.01f) - (border.x * 0.5f),
                (Mathf.Round(border.y * Random.value * 100f) * 0.01f) - (border.y * 0.5f)
            );

        winText.gameObject.SetActive(true);

        //Setup random value for color
        colorHue = Mathf.Round(10000 * Random.value) * 0.0001f;
        winText.color = Color.HSVToRGB(colorHue, 1, 1);

        if (Random.value > 0.5f)
            colorDir = 1;
        else
            colorDir = -1;
    }
    private void OnDisable()
    {
        if(winText != null)
            winText.gameObject.SetActive(false);
    }

    private void Update()
    {
        //Get information used for update
        Vector2 border = GetTxtBorderSize();
        Vector2 dir = ConvertAngleToDir(txtAngle);

        Vector2 newPos = txtRect.anchoredPosition;

        //Part 1: Move txt by speed in correct direction
        newPos += dir * txtSpeed * Time.deltaTime;

        //Check if text has passed the border
        bool moveAgain = (newPos.x > border.x * 0.5f || newPos.x < border.x * -0.5f || newPos.y > border.y * 0.5f || newPos.y < border.y * -0.5f);

        bool rotated = false;
        //Part 2: Move text again until used of movement (run a max of 3 times)
        for(int i = 0; i < 3; i++)
        {
            if (!moveAgain)
                break;

            //Stop movement at edge of border

            Vector2 updatedMove = new Vector2
            (
                Mathf.Clamp(newPos.x, border.x * -0.5f, border.x * 0.5f),
                Mathf.Clamp(newPos.y, border.y * -0.5f, border.y * 0.5f)
            );

            //Get remaining distance / speed text needs to move (round to nearest hundrendth)
            float remainingDistance = Mathf.Round( Vector2.Distance(newPos, updatedMove) * 100f ) * 0.01f;
            //Stop loop if movement is spent
            if(remainingDistance < 0.01f)
            {
                moveAgain = false;
                continue;
            }

            //Get new movement direction (flip axis if passed border)
            if(newPos.x >= (border.x * 0.5f) && dir.x > 0)
            {
                //Moving right and hit right border

                //Move left now
                dir.x = -1f * Mathf.Abs(dir.x);
                rotated = true;
            }
            else if(newPos.x <= (border.x * -0.5f) && dir.x < 0)
            {
                //Moving left and hit left border

                //Move right now
                dir.x = Mathf.Abs(dir.x);
                rotated = true;
            }
            if(newPos.y >= (border.y * 0.5f) && dir.y > 0)
            {
                //Moving up and hit ceiling border

                //Move down now
                dir.y = -1f * Mathf.Abs(dir.y);
                rotated = true;
            }
            else if(newPos.y <= (border.y * -0.5f) && dir.y < 0)
            {
                //Moving down and hit floor border

                //Move up now
                dir.y = Mathf.Abs(dir.y);
                rotated = true;
            }

            //Update position and check if need to run again
            newPos += remainingDistance * dir;
            moveAgain = (newPos.x > border.x * 0.5f || newPos.x < border.x * -0.5f || newPos.y > border.y * 0.5f || newPos.y < border.y * -0.5f);
        }

        //Part 3: Update rotation if direction changed (add random factor)
        if (rotated)
        {
            //Get new angle from direction (round to nearest tenth)
            float newAngle = ConvertDirToAngle(dir);
            float rndFactor = (Random.value * txtBounceRandom * 2) - txtBounceRandom;

            txtAngle = Mathf.Round((newAngle + rndFactor) * 10f) * 0.1f;
        }
        txtRect.anchoredPosition = newPos;

        #region Part 4 Color

        //Check if color direction timer has reached the limit
        if(changeColorDir)
        {
            //If color is 10 seconds past colorBounceTime, guarentee color direction change
            if (colorTimer >= colorBounceTime + 5)
            {
                colorTimer = 0;
                colorDir *= -1;
            }
            //Else have a 1/5 chance to change direction
            else if (Random.value <= 0.2f)
            {
                colorTimer = 0;
            }

            changeColorDir = false;
        }

        //Change color
        colorHue += colorSpeed * colorDir * Time.deltaTime;
        if (colorHue > 1)
            colorHue -= 1;
        else if (colorHue < 0)
            colorHue += 1;


        colorHue = Mathf.Round(colorHue * 10000f) * 0.0001f;

        winText.color = Color.HSVToRGB(colorHue, 1, 1);

        //Add onto color timer for next update
        if (Mathf.Floor(colorTimer + Time.deltaTime) > Mathf.Floor(colorTimer))
            changeColorDir = true;
        colorTimer += Time.deltaTime;
        #endregion

    }

    #region Math Properties & Methods
    //Get Size of Border for txt
    private Vector2 GetTxtBorderSize()
    {
        return canvasRect.sizeDelta - txtRect.sizeDelta;
    }


    //Convert from float angle (in degrees) to Vector2
    private Vector2 ConvertAngleToDir(float a)
    {
        //Alter angle to be within range (0 - 360)
        float angle = a;
        while(angle > 360 || angle < 0)
        {
            if (angle < 0)
                angle += 360;
            else if (angle > 360)
                angle -= 360;
        }

        //Convert angle to dir
        Vector2 dir = Vector2.zero;
        dir.x = Mathf.Cos(Mathf.Deg2Rad * angle);
        dir.y = Mathf.Sin(Mathf.Deg2Rad * angle);

        //Round value to the hundredths
        dir.x = Mathf.Round(dir.x * 100f) * 0.01f;
        dir.y = Mathf.Round(dir.y * 100f) * 0.01f;


        return dir;
    }
    //Convert from Vector2 direction to float angle in degrees
    private float ConvertDirToAngle(Vector2 dir)
    {
        //Convert dir to angle
        float angle = Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg;
        //Add to angle depending on which quadrant it's on
        if (dir.x < 0f)
            angle += 180;
        else if (dir.y < 0f)
            angle += 360;

        //Round value to the nearest tenth
        angle = Mathf.Round(angle * 10f) * 0.1f;

        //Alter angle to be within range (0 - 360)
        while(angle > 360 || angle < 0)
        {
            if (angle < 0)
                angle += 360;
            else if (angle > 360)
                angle -= 360;
        }

        return angle;
    }

    #endregion
}
