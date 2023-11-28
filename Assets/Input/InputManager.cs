using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitPressed();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuestLogTogglePressed();
        }
    }

    void SubmitPressed()
    {
        GameEventsManager.instance.inputEvents.SubmitPressed();
    }

    void QuestLogTogglePressed()
    {
        GameEventsManager.instance.inputEvents.QuestLogTogglePressed();
    }
    
}
