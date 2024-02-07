using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureDirector : MonoBehaviour
{
    [SerializeField] private Furniture parent;

    public Furniture GetFurniture()
    {
        return parent;
    }
}
