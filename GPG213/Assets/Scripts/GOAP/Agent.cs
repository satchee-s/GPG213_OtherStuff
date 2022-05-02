using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour //agent class based on what we did in class
{
    [SerializeField] List<Actions> actions;
    [SerializeField] List<Effect> inventory;
    [SerializeField] string Goal;

    private void Start()
    {
        FillRequired(new Effect(Goal));
    }

    bool FillRequired(Effect required)
    {
        List<Actions> actionsToPerform = new List<Actions>(); 
        for (int i = 0; i < actions.Count; i++)
        {
            for (int j = 0; j < actions[i].effects.Count; j++)
            {
                if (actions[i].effects[j].Name == required.Name)
                {
                    actionsToPerform.Add(actions[i]);
                }
            }
        }

        actionsToPerform.Sort();

        for (int i = 0; i < actionsToPerform.Count; i++)
        {
            if (actionsToPerform[i].requirements.Count > 0)
            {
                List<Effect> reqsNotAcquired;
                if (HasRequired(actionsToPerform[i].requirements, out reqsNotAcquired))
                {
                    List<Effect> effects = PerformAction(actionsToPerform[i]);
                    if (effects.Count > 0)
                        return true;
                }
                else
                {
                    for (int j = 0; j < reqsNotAcquired.Count; j++)
                    {
                        if (FillRequired(reqsNotAcquired[j]))
                        {
                            List<Effect> effects = PerformAction(actionsToPerform[j]);
                            if (effects.Count > 0)
                                return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    bool HasRequired(List<Effect> actionReqs, out List<Effect> reqsNotAcquired)
    {
        reqsNotAcquired = new List<Effect>();
        if (actionReqs.Count == 0)
            return true;
        for (int i = 0; actionReqs.Count > i; i++)
        {
            bool prereqAcquired = false;
            for (int j = 0; j < inventory.Count; j++)
            {
                if (actionReqs[i].Name == inventory[j].Name)
                {
                    prereqAcquired = true;
                    break;
                }
            }
            if (!prereqAcquired)
            {
                reqsNotAcquired.Add(actionReqs[i]);
            }
        }
        if (reqsNotAcquired.Count <= 0)
            return true;

        return false;
    }

    List<Effect> PerformAction(Actions action)
    {
        List <Effect> effectsAcquired = new List<Effect>();
        action.Completed += (string actionName, List<Effect> effects) =>
        {
            for (int i = 0; i < effects.Count; i++)
            {
                print(actionName);
                inventory.Add(new Effect(effects[i].Name));
            }
            effectsAcquired = effects;
        };
        action.Failed += (string message) =>
        {
            print(message);
        };
        action.PerformTask();
        action.Completed = null;
        action.Failed = null;
        return effectsAcquired;
    }
}
