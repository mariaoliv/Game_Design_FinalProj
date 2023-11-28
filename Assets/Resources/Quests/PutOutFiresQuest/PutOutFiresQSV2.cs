using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOutFiresQSV2 : QuestStep
{
    // Goal : put out 4 forest fires

    private bool completed = false;
    [SerializeField] int numFires = 7;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onFiresClear += FiresClear;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onFiresClear -= FiresClear;
    }

    private void FiresClear()
    {
        completed = true;
        UpdateState();
        FinishQuestStep();
    }

    private void UpdateState()
    {
        string state = completed.ToString();
        ChangeState(state);
    }
    protected override void SetQuestStepState(string state)
    {
        this.numFires = System.Int32.Parse(state);
        UpdateState();
    }
}
