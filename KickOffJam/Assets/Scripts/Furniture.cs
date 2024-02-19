using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private static Dictionary<FChartID, Furniture> currentFurnitures = new Dictionary<FChartID, Furniture>();

    [Header("Properties")]
    [SerializeField] private FChartID furnitureFCID = FChartID.none;
    [Header("Object Reference")]
    [SerializeField] private Transform centerPos = null;
    [SerializeField] private FurnitureDirector dialogueTrigger = null;


    #region Used in External Scripts (Getters/Setters)
    public Vector2? GetCenterPos()
    {
        if (centerPos == null)
            return null;

        Vector2 p = centerPos.position;
        return p;
    }

    #endregion

    private void OnEnable()
    {
        if (currentFurnitures.ContainsKey(furnitureFCID))
        {
            Debug.LogError("Found multiple copies of furniture with id: " + $"\nExisting: {currentFurnitures[furnitureFCID].name}\nAttempted to Add: {gameObject.name}");
            return;
        }

        currentFurnitures.Add(furnitureFCID, this);
    }
    private void OnDisable()
    {
        currentFurnitures.Remove(furnitureFCID);
    }

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


    //Static Methods Used by Funguschart to update furniture

    //Hide furniture from view and deactivate its functions
    public static bool HideFurniture(FChartID id)
    {
        if (!currentFurnitures.ContainsKey(id))
            return false;

        currentFurnitures[id].gameObject.SetActive(false);
        return true;
    }
    public static bool DisableCollider(FChartID id)
    {
        if (!currentFurnitures.ContainsKey(id))
            return false;

        currentFurnitures[id].dialogueTrigger.gameObject.SetActive(false);
        return true;
    }
}
