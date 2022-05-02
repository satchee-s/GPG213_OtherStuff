using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleSandwich : Actions
{
    public override void PerformTask()
    {
        int number = Random.Range(0, 2);
        if (number == 0)
            Completed(completedMessage, effects);
        else
            Failed(failedMessage);
    }
}
