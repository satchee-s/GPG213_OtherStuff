using UnityEngine;

public class BuySandwich : Actions
{
    public override void PerformTask()
    {
        int randomNumber = Random.Range(0, 10);
        if (randomNumber %3 == 0)
        {
            Failed(failedMessage);
        }
        else
        {
            Completed(completedMessage, effects);
        }
    }
}
