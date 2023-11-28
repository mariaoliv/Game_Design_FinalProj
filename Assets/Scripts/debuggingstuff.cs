using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debuggingstuff : MonoBehaviour
{
    public QuestManager qm;
    // Start is called before the first frame update
    void Start()
    {
        print("plartformer level 1 quest state: " + qm.GetQuestState("CompletePlatformerLevel1Quest"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
