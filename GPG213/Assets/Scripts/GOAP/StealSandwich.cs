using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealSandwich : Actions
{
    public override void PerformTask()
    {
        int x = Random.Range(0, 5);
        if (x < 2)
            Completed(completedMessage, effects);
        else
            Failed(failedMessage);
    }
}
