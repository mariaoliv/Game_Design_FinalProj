using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }


    public QuestEvents questEvents;
    public MiscEvents miscEvents;
    public InputEvents inputEvents;
    public PlayerEvents playerEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize all events
        questEvents = new QuestEvents();
        miscEvents = new MiscEvents();
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
    }
}
