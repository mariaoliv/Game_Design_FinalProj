using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageQuestObjects : MonoBehaviour
{
    [SerializeField] GameObject[] questObjects;
    [SerializeField] QuestManager qm;

    Dictionary<string, string> tagToQuestId = new Dictionary<string, string>()
    {
        { "Egg Guard", "EggsQuest" },
        { "Egg", "EggsQuest" },
        { "Fire", "PutOutFiresQuest" }
    };

    void Start()
    {
        foreach (GameObject obj in questObjects)
        {
            string questId = tagToQuestId[obj.tag];

            if (questId == "EggsQuest" && obj.CompareTag("Egg Guard") && (qm.GetQuestState(questId) != QuestState.IN_PROGRESS))
            {
                //Debug.Log("This is an egg guard " + qm.GetQuestState(questId));
                obj.gameObject.SetActive(false);
            }
            else if (qm.GetQuestState(questId) == QuestState.FINISHED || qm.GetQuestState(questId) == QuestState.CAN_FINISH)
            {
                obj.gameObject.SetActive(false);
            }
            else
            {
                obj.gameObject.SetActive(true);
            }
        }
    }

    
    void Update()
    {
        foreach (GameObject obj in questObjects)
        {
            if (obj.CompareTag("Egg Guard") && obj != null && (qm.GetQuestState(tagToQuestId[obj.tag]) == QuestState.IN_PROGRESS))
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}
