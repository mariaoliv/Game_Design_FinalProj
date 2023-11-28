using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOutFiresQuestStep : QuestStep
{
    // Goal : put out 4 forest fires

    private int firesPutOut = 0;
    private int firesToComplete = 7;


    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onFirePutOut += FirePutOut;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onFirePutOut -= FirePutOut;
    }

    private void FirePutOut()
    {
        if (firesPutOut < firesToComplete)
        {
            firesPutOut++;
            UpdateState();
        }
        if (firesPutOut >= firesToComplete)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = firesPutOut.ToString();
        ChangeState(state);
    }
    protected override void SetQuestStepState(string state)
    {
        this.firesPutOut = System.Int32.Parse(state);
        UpdateState();
    }
}
