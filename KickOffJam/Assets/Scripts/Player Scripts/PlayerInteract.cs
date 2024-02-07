using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that manages player colisions and input interactions with furniture
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private List<Furniture> inRange = new List<Furniture>();


    //Waits for Interact input from player. On Press, the player calls interact method from closest furniture
    private void Update()
    {
        //Don't run while game is paused or dialogue is running
        if (FungusChart.isRunningDialogue)
            return;
        if(PauseMenuManager.I != null)
        {
            if (PauseMenuManager.I.GamePaused)
                return;
        }
        //Don't run if there are no furniture
        if (inRange.Count == 0)
            return;
        
        //Check if player interacts with furniture
        if (Input.GetButtonDown("Interact"))
        {
            //Call interact method to closest furniture
            Furniture f = FindClosestFurniture();
            if (f == null)
            {
                Debug.LogError("inRange contains null furniture");
                return;
            }
            f.Interact();
        }

    }
    private Furniture FindClosestFurniture()
    {
        if (inRange.Count == 0)
            return null;
        //If only 1 furniture is in range, return it
        if (inRange.Count == 1)
            return inRange[0];

        Vector2 playerPos = transform.position;


        Furniture closest = null;
        float closestDistance = 0f;

        for(int i = 0;i < inRange.Count;i++)
        {
            if (inRange[i] == null)
                continue;

            //Setup first Furniture if found
            if(closest == null)
            {
                closest = inRange[i];
                closestDistance = Vector2.Distance(playerPos, closest.GetCenterPos().Value);
                continue;
            }
            
            //Replace Furniture if their center is closer
            float distance = Vector2.Distance(playerPos, inRange[i].GetCenterPos().Value);
            if(distance < closestDistance)
            {
                closest = inRange[i];
                closestDistance = distance;
            }
        }

        return closest;
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag != "Furniture")
            return;

        //Check if collider is a furniture
        FurnitureDirector fd = c.gameObject.GetComponent<FurnitureDirector>();
        if (fd == null)
            return;

        //Get Furniture script from collider
        Furniture furniture = fd.GetFurniture();
        if (furniture == null) 
        {
            Debug.LogError("Furniture script not found: " + fd.gameObject.name);
            return; 
        }
        if (inRange.Contains(furniture))
        {
            Debug.LogError("Collision Enter not handled properly:" + $"\nPlayer: {gameObject.name}" + $"\nFurniture: {furniture.gameObject.name}");
            return;
        }

        //Add furniture to list
        inRange.Add(furniture);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag != "Furniture")
            return;

        //Check if collider is a furniture
        FurnitureDirector fd = c.gameObject.GetComponent<FurnitureDirector>();
        if (fd == null)
            return;

        //Get Furniture script from collider
        Furniture furniture = fd.GetFurniture();
        if (furniture == null)
        {
            Debug.LogError("Furniture script not found: " + fd.gameObject.name);
            return;
        }
        if (!inRange.Contains(furniture))
        {
            Debug.LogError("Collision Exit not handled properly:" + $"\nPlayer: {gameObject.name}" + $"\nFurniture: {furniture.gameObject.name}");
            return;
        }

        //Remove furniture from list
        inRange.Remove(furniture);
    }
}
