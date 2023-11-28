using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLvlQuestStep : QuestStep
{
    [SerializeField] private int levelToComplete; // want to reuse this code for all the diff platformer levels
    private bool completed;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlatformerLevelCompleted += LevelCompleted;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlatformerLevelCompleted -= LevelCompleted;
    }

    public void LevelCompleted(int level)
    {
        if (level == levelToComplete)
        {
            completed = true;
            UpdateState();
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = completed.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.levelToComplete = System.Int32.Parse(state);
        UpdateState();
    }
}
