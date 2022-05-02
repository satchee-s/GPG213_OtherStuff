using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class Actions : MonoBehaviour, IComparable
{
    public int Cost;
    public List<Effect> requirements = new List<Effect>();
    public List<Effect> effects = new List<Effect>();

    public delegate void CompletedEvent(string message, List<Effect> effects);
    public CompletedEvent Completed; 
    public delegate void FailedEvent(string message);
    public FailedEvent Failed;
    public string completedMessage;
    public string failedMessage;

    public abstract void PerformTask();

    public int CompareTo(object obj)
    {
        if (((Actions)obj).Cost < Cost)
            return 1;
        if (((Actions)obj).Cost > Cost)
            return -1;
        return 0;
    }

}
