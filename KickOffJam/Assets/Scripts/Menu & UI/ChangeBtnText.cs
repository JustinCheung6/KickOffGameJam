using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Change Button text when hovering over button (used w/ Event Trigger)
public class ChangeBtnText : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private string changedText = "";
    private string originalText = "";
    private bool txtChanged = false;

    //Object References
    private Text btnText;

    private void Awake()
    {
        btnText = GetComponentInChildren<Text>();
        if(btnText == null)
            enabled = false;

        originalText = btnText.text;
        txtChanged = false;

    }

    //Change text on hover
    public void MouseHover_EventTrigger()
    {
        btnText.text = changedText;

    }
    //Revert text on exit
    public void MouseExit_EventTrigger()
    {
        btnText.text = originalText;
    }
}
