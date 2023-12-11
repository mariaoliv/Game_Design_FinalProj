using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // I'm thinking of making the questMap a public static varbiable
    // so if the state of PutOutFiresQuest is not IN_PROGRESS, you can't interact with the fires (for example)

    [SerializeField] private Dictionary<string, Quest> questMap;

    // quest start requirements

    private int currentPlayerXP;

    // the current player xp is publicvars.xp

    // new changes
    public TreeStumpManager tsm;

    public Window_QuestPointer pointer;

    //public GameObject fireparent;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void Start()
    {
        PublicVars.xp = PlayerPrefs.GetInt("XP", 0); // default value is 0
        Debug.Log("XP: " + PublicVars.xp);
        // broadcast initial state of quests on startup
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
                if ((quest.info.id == "PutOutFiresQuest" || quest.info.id == "EggsQuest") && pointer != null)
                {
                    pointer.gameObject.SetActive(false);
                }
            }

            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }

        if (pointer != null)
        {
            SetActiveQuestsForPointer();
        }
    }

    public int GetNumNotStartedQuests()
    {
        int count = 0;
        foreach (Quest q in questMap.Values)
        {
            if (q.state == QuestState.REQUIREMENTS_NOT_MET || q.state == QuestState.CAN_START)
            {
                count++;
            }
        }
        return count;
    }

    // return number of quests that are in the IN_PROGRESS or CAN_FINISH state -- to be used with quest pointer...
    public int GetNumActiveQuests()
    {
        int count = 0;
        foreach (Quest q in questMap.Values)
        {
            if (q.state == QuestState.IN_PROGRESS || q.state == QuestState.CAN_FINISH)
            {
                count++;
            }
        }
        return count;
    }

    public void SetActiveQuestsForPointer()
    {
        int activeQuests = GetNumActiveQuests();
        /*
        if (activeQuests == 0)
        {
            string[] questIds = { };
            pointer.SetActiveQuests(questIds);
        }
        else if (pointer != null)
        */
        if (pointer != null)
        {
            string[] questIds = new string[GetNumActiveQuests()];
            int idx = 0;
            foreach (string id in questMap.Keys)
            {
                if (questMap[id].state == QuestState.IN_PROGRESS || questMap[id].state == QuestState.CAN_FINISH)
                {
                    questIds[idx] = id;
                    idx++;
                }              
            }
            
            // call set active quests for the pointer
            pointer.SetActiveQuests(questIds);
        }
    }

    public float GetQuestMapLength()
    {
        return (float)questMap.Count;
    }

    public float GetNumQuestsFinished()
    {
        int count = 0;
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.FINISHED)
            {
                count++;
            }
        }
        return (float)(count);
    }

    

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }

    private void Update()
    {
        currentPlayerXP = PublicVars.xp;
        //Debug.Log("CURRENT PLAYER XP: " + currentPlayerXP);

        
        foreach (Quest quest in questMap.Values)
        {
            // if we're now meeting the requirements, switch to the CAN_START state
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
        

    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
        if (pointer != null)
        {
            SetActiveQuestsForPointer();
        }
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        if (pointer != null)
        {
            SetActiveQuestsForPointer();
        }
        /*
        if (id == "PutOutFiresQuest")
        {
            fireparent.SetActive(true);
        } */
        if (pointer != null && (id == "PutOutFiresQuest" || id == "EggsQuest"))
        {
            pointer.gameObject.SetActive(false);
        }
    }

    private void AdvanceQuest(string id)
    {
        Debug.Log("Advance quest");
        
        Quest quest = GetQuestByID(id);

        //move on to the next step
        quest.MoveToNextStep();

        // if there are more steps, instantiate the next one
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }

        //if there are no more steps, then we've finished all of them for this quest
        else
        {
            ChangeQuestState(id, QuestState.CAN_FINISH);
            if (pointer != null && (id == "PutOutFiresQuest" || id == "EggsQuest"))
            {
                //reenable the arrow so the player can find their way back to the quest point
                pointer.gameObject.SetActive(true);
            }
        }
        if (pointer != null)
        {
            SetActiveQuestsForPointer();
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);

        if (tsm != null)
        {
            tsm.CompleteQuest();
        }

        if (id == "PutOutFiresQuest")
        {
            PlayerPrefs.SetInt(id, 1); // 1 means quest has been completed
        }
        if (pointer != null)
        {
            SetActiveQuestsForPointer();
        }
    }

    private void ClaimRewards(Quest quest)
    {
        int xpReward = quest.info.xpReward;
        PublicVars.xp += xpReward;
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestByID(string id)
    {
        Debug.Log("Keys: ");

        foreach (string key in questMap.Keys)
        {
            Debug.Log(key + " ~~~~~ ");
        }

        Debug.Log("ID: " + id);

        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }
        return quest;
    }

    // for example, the player can only interact with the fires if PutOutFiresQuest is in progress
    public QuestState GetQuestState(string id)
    {
        Quest quest = questMap[id];
        return quest.state;
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;
        //check xp Requirements
        if (currentPlayerXP < quest.info.xpRequirement)
        {
            meetsRequirements = false;
        }
        //check prerequisites
        foreach(QuestInfoSO prereqQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestByID(prereqQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestByID(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private void OnApplicationQuit()
    {
        SaveAllQuests();
    }

    // making this public so i can use it for scene changes in addition to OnApplicationQuit
    public void SaveAllQuests()
    {
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);
            PlayerPrefs.SetInt("XP", PublicVars.xp);
        }
        /*
        if (pointer != null)
        {
            pointer.SaveQuestId();
        } */
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData data = quest.GetQuestData();
            string serializedData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(quest.info.id, serializedData);
            Debug.Log(serializedData);
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e);
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;
        try
        {
            if (PlayerPrefs.HasKey(questInfo.id))
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e);
        }
        return quest;
    }
}
