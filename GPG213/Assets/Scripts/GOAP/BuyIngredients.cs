using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyIngredients : Actions
{
    public override void PerformTask()
    {
        int a = Random.Range(0, 10);
        int b = Random.Range(0, 10);
        int c = Random.Range(0, 10);
        if (a+b+c > 10)
        {
            Completed(completedMessage, effects);
        }
        else
        {
            Failed(failedMessage);
        }
    }
}
