using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // if a fire has been put out, set player prefs so that it doesn't reappear after a scene change

    bool isPutOut = false;
    public QuestManager qm;

    public void putOut()
    {
        this.gameObject.SetActive(false);
        isPutOut = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (qm.GetQuestState("PutOutFiresQuest") == QuestState.REQUIREMENTS_NOT_MET)
        {
            isPutOut=false;
            this.gameObject.SetActive(true);
        }   
        if (qm.GetQuestState("PutOutFiresQuest") == QuestState.IN_PROGRESS && isPutOut)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
