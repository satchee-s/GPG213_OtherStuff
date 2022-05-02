using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteActions : MonoBehaviour
{
    //i tried to write my own class for goap based on the original agent class
    [SerializeField] List<Actions> allActions;
    [SerializeField] List<Effect> inventory;
    [SerializeField] Effect Goal;
    bool goalAchieved;

    private void Start()
    {
        Run(Goal);
    }

    void Run(Effect effect)
    {
        if (!goalAchieved)
        {
            FindWithEffect(effect);
            if (effect.actionsNeeded.Count <= 0)
                Debug.Log("Not possible to find action with " + effect);
            for (int i = 0; i < effect.actionsNeeded.Count; i++)
            {
                List<Effect> reqs;
                if (!HasReq(effect.actionsNeeded[i], out reqs))
                {
                    for (int j = 0; j < reqs.Count; j++)
                    {
                        Run(reqs[i]);
                    }
                }
                if (PerformAction(effect.actionsNeeded[i]))
                {
                    if (goalAchieved)
                    {
                        Debug.Log("Final goal reached");
                        break;
                    }
                }
            }
        }
    }

    void FindWithEffect(Effect effect)
    {
        for (int i = 0; i < allActions.Count; i++) //finds all actions with required effect
        {
            for (int j = 0; j < allActions[i].effects.Count; j++)
            {
                if (allActions[i].effects[j].Name == effect.Name)
                {
                    effect.actionsNeeded.Add(allActions[i]);
                }
            }
        }
        effect.actionsNeeded.Sort();
    }

    bool HasReq(Actions action, out List<Effect>reqsNeeded)
    {
        reqsNeeded = new List<Effect>();
        if (action.requirements.Count == 0) //sees if you have everything in your inventory
            return true;
        for (int i = 0; i < action.requirements.Count; i++)
        {
            bool hasReq = false;
            for (int j = 0; j < inventory.Count; j++)
            {
                if (inventory[j].Name == action.requirements[i].Name)
                {
                    hasReq = true;
                    break;
                }
            }
            if (!hasReq)
            {
                reqsNeeded.Add(action.requirements[i]);
            }
            if (reqsNeeded.Count > 0)
                return false;
        }
        return true;
    }

    bool PerformAction(Actions action)
    {
        bool successful = false;
        action.Completed += (string message, List<Effect> effects) =>
        {
            print(message);
            successful = true;
            for (int i = 0; i < effects.Count; i++)
            {
                inventory.Add(new Effect(effects[i].Name));
                if (effects[i].Name == Goal.Name)
                    goalAchieved = true;
            }
        };
        action.Failed += (string message) =>
        {
            successful = false;
            print(message);
        };
        action.PerformTask();
        action.Completed = null;
        action.Failed = null;
        return successful;
    }
}
