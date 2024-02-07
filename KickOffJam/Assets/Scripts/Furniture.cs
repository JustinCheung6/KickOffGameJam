using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [Header("Object Reference")]
    [SerializeField] private FChartID furnitureFCID = FChartID.none;
    [SerializeField] private Transform centerPos = null;


    #region Used in External Scripts (Getters/Setters)
    public Vector2? GetCenterPos()
    {
        if (centerPos == null)
            return null;

        Vector2 p = centerPos.position;
        return p;
    }

    #endregion

    //Start interaction w/ player (called by Player)
    public void Interact()
    {
        if(furnitureFCID == FChartID.none)
        {
            Debug.LogError("Furniture doesn't have an FChartID: " + gameObject.name);
            return;
        }

        Debug.Log("Started Dialogue: " + furnitureFCID);
        FungusChart.StartDialogue(furnitureFCID);
    }
}
