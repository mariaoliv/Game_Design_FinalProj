using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelsManager
{
    private static Dictionary<string, bool> levelCompletionStatus = new Dictionary<string, bool>();

    public static void SetLevelCompleted(string levelName, bool completed)
    {
        levelCompletionStatus[levelName] = completed;
    }

    public static bool IsLevelCompleted(string levelName)
    {
        return levelCompletionStatus.ContainsKey(levelName) && levelCompletionStatus[levelName];
    }
}
