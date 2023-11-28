using System;

public class PlayerEvents
{
    public Action<int> onExperienceGained;
    public void ExperienceGained(int xp)
    {
        if (onExperienceGained != null)
        {
            onExperienceGained(xp);
        }
    }

    public Action<int> onPlatformerLevelCompleted;
    public void PlatformerLevelCompleted(int level) 
    { 
        if (onPlatformerLevelCompleted != null)
        {
            onPlatformerLevelCompleted(level);
        }
    }

    public Action onFiresClear;
    public void FiresClear()
    {
        if (onFiresClear != null)
        {
            onFiresClear();
        }
    }
}
