using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ProgressManager : MonoBehaviour
{
    private List<Scenarios> events = new List<Scenarios>();
    private Items inventory = Items.nothing;

    private List<string> text;

    [SerializeField] private Flowchart FC;

    public enum Scenarios
    {
        notthing = 0,
        bed = 1,
        ceilingLeaky = 2,
        chair = 3,
        couch = 4,
        cupboard = 5,
        door = 6,
        drawer = 7,
        floor = 8,
        fridge = 9,
        housePlant = 10,
        microwave = 11,
        window = 12
    }
    public enum Items
    {
        nothing,
        fish,
        cupFish,
        key
    }

    private void Start()
    {
    }

    public void Activate(Scenarios s)
    {
        if (events.Count >= 3)
            return;

        events.Add(s);

        if (events.Count == 1)
        {

        }
        else if(events.Count == 2)
        {

        }
        else if (events.Count == 3)
        {

        }
    }
}
