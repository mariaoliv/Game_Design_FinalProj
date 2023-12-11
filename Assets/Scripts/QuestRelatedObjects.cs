using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRelatedObjects : MonoBehaviour
{
    [SerializeField] string questId;
    public QuestManager qm;

    // Start is called before the first frame update
    void Start()
    {
        if (questId == "EggsQuest" && (this.gameObject.CompareTag("Egg Guard")) && (qm.GetQuestState(questId) != QuestState.IN_PROGRESS))
        {
            Debug.Log("This is an egg guard " + qm.GetQuestState(questId));
            this.gameObject.SetActive(false);
        }
        else if (qm.GetQuestState(questId) == QuestState.FINISHED || qm.GetQuestState(questId) == QuestState.CAN_FINISH)
        {
            this.gameObject.SetActive(false);
        }
        /*
        if (questId == "PutOutFiresQuest" && this.gameObject.CompareTag("Fire") && qm.GetQuestState(questId) != QuestState.IN_PROGRESS)
        {
            this.gameObject.SetActive(false);
        } */
        else
        {
            this.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if ((this.gameObject.CompareTag("Egg Guard")) && qm.GetQuestState(questId) == QuestState.IN_PROGRESS)
        {
            this.gameObject.SetActive(true);
        }
        if (this.gameObject.CompareTag("Fire") && qm.GetQuestState(questId) == QuestState.IN_PROGRESS)
        {
            this.gameObject.SetActive(true);
        }
    }
}
