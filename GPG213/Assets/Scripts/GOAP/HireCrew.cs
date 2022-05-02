using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireCrew : Actions
{
    [SerializeField] int crewMembers;
    public override void PerformTask()
    {
        if (crewMembers < 3)
            Failed(failedMessage);
        else
            Completed(completedMessage, effects);
    }
}
