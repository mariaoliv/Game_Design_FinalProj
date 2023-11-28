using System;

public class MiscEvents
{
    public event Action onFirePutOut;
    public void FirePutOut()
    {
        if (onFirePutOut != null)
        {
            onFirePutOut();
        }
    }
    public event Action onEggBroughtBack;
    public void EggBroughtBack()
    {
        if (onEggBroughtBack != null)
        {
            onEggBroughtBack();
        }
    }
}
