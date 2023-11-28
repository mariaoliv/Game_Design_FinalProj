using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringEggsBackQuestStep : QuestStep
{
    private int eggsBroughtBack = 0;
    private int eggsToBringBack = 3;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onEggBroughtBack += BringBackEgg;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onEggBroughtBack -= BringBackEgg;
    }

    private void BringBackEgg()
    {
        if (eggsBroughtBack < eggsToBringBack)
        {
            eggsBroughtBack++;
            UpdateState();
        }
        if (eggsBroughtBack >= eggsToBringBack)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = eggsBroughtBack.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.eggsBroughtBack = System.Int32.Parse(state);
        UpdateState();
    }
}
