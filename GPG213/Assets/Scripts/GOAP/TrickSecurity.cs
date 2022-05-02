using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickSecurity : Actions
{
    public override void PerformTask()
    {
        int a = Random.Range(0, 10);
        if (a < 5)
        {
            Completed(completedMessage, effects);
        }
        else
            Failed(failedMessage);
    }
}
