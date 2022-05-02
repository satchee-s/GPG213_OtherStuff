using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    public string Name;
    [HideInInspector] public List<Actions> actionsNeeded = new();
    public Effect (string name)
    {
        Name = name;
    }
}
