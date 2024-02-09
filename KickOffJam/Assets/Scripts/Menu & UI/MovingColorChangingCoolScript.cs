using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingColorChangingCoolScript : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float txtSpeed = 5f;
    [SerializeField] private float colorSpeed = 5f;

    //Angle which the color and text will move
    [SerializeField]private float txtAngle = 0;
    private float colorAngle = 0f;
    //How much can the angle shift after hitting hitting a 
    private float txtAngleDisplace = 10f;
    //Range from 0 to 100
    private Vector2 txtPos = Vector2.zero;
    private Vector3 colorPos = Vector3.zero;


    [Header("Object Reference")]
    [SerializeField] private TextMeshProUGUI winText;
    private RectTransform txtRect;

    private RectTransform canvasRect;

    private void OnEnable()
    {
        if(winText == null)
        {
            Debug.LogError("winText has no refereance: " + gameObject.name);
            enabled = false;
        }

        if(txtRect == null)
            txtRect = winText.rectTransform;
        if(canvasRect == null)
            canvasRect = winText.canvas.GetComponent<RectTransform>();

    }

    private void Start()
    {
        txtAngle = Mathf.Floor(Random.value * 360f);
        colorAngle = Mathf.Floor(Random.value * 360f);

        txtPos = new Vector2(Mathf.Floor(Random.value * 100f), Mathf.Floor(Random.value * 100f));
        colorPos = new Vector3(Mathf.Floor(Random.value * 100f), Mathf.Floor(Random.value * 100f), Mathf.Floor(Random.value * 100f));
    }


    private void Update()
    {
        float txtDistance = txtSpeed * Time.deltaTime;

        //Get txtPos Updated (and update txtAngle for next txtAngle)
        while(txtDistance > 0.001f)
        {
            Vector2 txtDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * txtAngle), Mathf.Sin(Mathf.Deg2Rad * txtAngle));
            txtPos += txtDir * txtDistance;

            Vector2 clampedtxtPos = new Vector2(Mathf.Clamp(txtPos.x, 0f, 100f), Mathf.Clamp(txtPos.y, 0f, 100f));

            if (clampedtxtPos == txtPos)
                break;

            //Update distance remaining
            txtDistance -= Vector3.Distance(clampedtxtPos, txtPos);
            txtPos = clampedtxtPos;

            //Update angle
            if (txtPos.x == 100f || txtPos.x == 0f)
                txtDir.x = -1f * txtDir.x;

            if(txtPos.y == 100f || txtPos.y == 0f)
                txtDir.y = -1f * txtDir.y;

            float oldAngle = txtAngle;
            txtAngle = Mathf.Atan(txtDir.y / txtDir.x) * Mathf.Rad2Deg;
            //Make sure angle range is correct depending on direction (checked on desmos)
            if(txtDir.x < 0f)
            {
                txtAngle += 180;
            }
            else if( txtDir.y < 0f)
            {
                txtAngle += 360;
            }

            //Debug.Log($"Change in angle. Old: {oldAngle} New: {txtAngle}");
        }

        float txtSpaceX = (canvasRect.sizeDelta.x - txtRect.sizeDelta.x) * 0.5f;
        float txtSpaceY = (canvasRect.sizeDelta.y - txtRect.sizeDelta.y) * 0.5f;

        txtRect.position = new Vector2(txtPos.normalized.x * txtSpaceX, txtPos.normalized.y * txtSpaceY);
        
    }
}
