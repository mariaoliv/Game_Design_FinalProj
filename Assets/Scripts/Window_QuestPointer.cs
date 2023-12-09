using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;


// will need to edit this code to work with QuestManager to switch between active quests (by pressing A and D keys?)
public class Window_QuestPointer : MonoBehaviour
{
    //[SerializeField] private Camera uiCamera;

    private Vector3 targetPosition;
    [SerializeField] Transform target;
    [SerializeField] PlayerController player;
    private RectTransform pointerRectTransform;
    [SerializeField] QuestManager qm;
    string[] activeQuestIds = { }; // ids of quests that are "in progress" or "can finish"

    string currQuestId = "";

    public Transform firesQuestPoint; // make transforms for all the other quest points...
    public Transform eggsQuestPoint;
    public Transform platformerLvl1QuestPoint;
    public Transform platformerLvl2QuestPoint;
    public Transform platformerLvl3QuestPoint;
    public Transform lvl1Portal;
    public Transform lvl2Portal;
    public Transform lvl3Portal;

    private void Awake()
    {
        //targetPosition = new Vector3(200, 45);
        //targetPosition = target.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        currQuestId = PlayerPrefs.GetString("Current quest id", "");
    }
    private void Update()
    {
        if (qm.GetNumActiveQuests() == 0)
        {
            //GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
            //target = player.findNearestFire(fires).transform;
            target = player.findNearestObjWithTag("Quest Point").transform;
            targetPosition = target.position;
        }
        else
        {
            if (qm.GetQuestState(currQuestId) == QuestState.IN_PROGRESS)
            {
                if (currQuestId == "PutOutFiresQuest")
                {
                    target = player.findNearestObjWithTag("Fire").transform;
                    targetPosition = target.position;
                }
                if (currQuestId == "EggsQuest")
                {
                    target = player.findNearestObjWithTag("Egg").transform;
                    targetPosition = target.position;
                }
                if (currQuestId == "CompletePlatformerLevel1Quest")
                {
                    target = lvl1Portal;
                    targetPosition = target.position;
                }
                if (currQuestId == "CompletePlatformerLevel2Quest")
                {
                    target = lvl2Portal;
                    targetPosition = target.position;
                }
                if (currQuestId == "CompletePlatformerLvl3Quest")
                {
                    target = lvl3Portal;
                    targetPosition = target.position;
                }
            }
            else if (qm.GetQuestState(currQuestId) == QuestState.CAN_FINISH)
            {
                if (currQuestId == "PutOutFiresQuest")
                {
                    target = firesQuestPoint;
                    targetPosition = target.position;
                }
                if (currQuestId == "EggsQuest")
                {
                    target = eggsQuestPoint;
                    targetPosition = target.position;
                }
                if (currQuestId == "CompletePlatformerLevel1Quest")
                {
                    target = platformerLvl1QuestPoint;
                    targetPosition = target.position;
                }
                if (currQuestId == "CompletePlatformerLevel2Quest")
                {
                    target = platformerLvl2QuestPoint;
                    targetPosition = target.position;
                }
                if (currQuestId == "CompletePlatformerLvl3Quest")
                {
                    target = platformerLvl3QuestPoint;
                    targetPosition = target.position;
                }
            }
        }


        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        
        
        
        

    }

    public void SaveQuestId()
    {
        PlayerPrefs.SetString("Current quest id", currQuestId);
    }

    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetString("Current quest id", currQuestId);
        SaveQuestId();
    }

    private int findIndex(string id)
    {
        for (int i = 0; i < activeQuestIds.Length; i++) 
        {
            if (activeQuestIds[i] == id)
            {
                return i;
            }
        }
        return -1;
    }
    /*
    public void TrackNextQuest() //press 'D' or '-->'
    {
        if (qm.GetNumActiveQuests() == 0)
        {
            return;
        }
        int idx = findIndex(currQuestId);
        if (idx == activeQuestIds.Length - 1)
        {
            currQuestId = activeQuestIds[0];
        }
        else
        {
            currQuestId = activeQuestIds[idx + 1];
        }
    }
    public void TrackPrevQuest() //press 'A' or '<--'
    {
        if (qm.GetNumActiveQuests() == 0)
        {
            return;
        }
        int idx = findIndex(currQuestId);
        if (idx == 0)
        {
            currQuestId = activeQuestIds[activeQuestIds.Length - 1];
        }
        else
        {
            currQuestId = activeQuestIds[idx - 1];
        }
    }
    */
    public void SetActiveQuests(string[] quests)
    {
        activeQuestIds = quests;
        if (quests.Length > 0)
        {
            currQuestId = quests[0];
        }
        Debug.Log("curr quest id " + currQuestId);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        targetPosition = target.position;
    }
}
