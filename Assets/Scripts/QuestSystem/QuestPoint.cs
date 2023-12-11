using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;
    [SerializeField] DialogueEvent questDialogue;

    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;

    [SerializeField]private QuestIcon questIcon; 

    public QuestManager qm; //new code

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void Start()
    {
        //new code
        if (qm.GetQuestState(questId) == QuestState.IN_PROGRESS || qm.GetQuestState(questId) == QuestState.CAN_FINISH)
        {
            finishPoint = true;
            startPoint = false;
        }
        else if (qm.GetQuestState(questId) == QuestState.CAN_START || qm.GetQuestState(questId) == QuestState.REQUIREMENTS_NOT_MET)
        {
            startPoint = true;
            finishPoint = false;
        }
        //end of new code
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    public string GetQuestID()
    {
        return questId;
    }
    private void SubmitPressed()
    {
        if (!playerIsNear)
        {
            return;
        }

        // start or finish a quest
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
            questDialogue.callDialogue();
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint) {
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        // only update the quest state if this point has th corresponding quest
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;

            //EXPERIMENTAL CODE
            if(currentQuestState == QuestState.IN_PROGRESS && startPoint)
            {
                startPoint = false;
                finishPoint = true;
            }
            //END OF EXPERIMENTAL CODE


            questIcon.SetState(currentQuestState, startPoint, finishPoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
