using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]

    [SerializeField] private GameObject requirementsNotMetStartIcon;
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject requirementsNotMetToFinishIcon;
    [SerializeField] private GameObject canFinishIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        requirementsNotMetStartIcon.SetActive(false);
        canStartIcon.SetActive(false);
        canFinishIcon.SetActive(false);
        requirementsNotMetToFinishIcon.SetActive(false);

        switch(newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) { requirementsNotMetStartIcon.SetActive(true); }
                break;
            case QuestState.CAN_START: 
                if (startPoint) {  canStartIcon.SetActive(true); }
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint) { requirementsNotMetToFinishIcon.SetActive(true); }
                break;
            case QuestState.CAN_FINISH:
                if (finishPoint) {
                    Debug.Log("CAAAANNNN FINISHHH");
                    canFinishIcon.SetActive(true); }
                break;
            case QuestState.FINISHED:
                break;
            default:
                Debug.LogWarning("Quest state not recognized by switch statement for quest icon: " + newState);
                break;
        }
    }
}
