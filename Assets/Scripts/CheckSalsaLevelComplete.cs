using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSalsaLevelComplete : MonoBehaviour
{
    public int levelNumber;
    public QuestManager qm;
    private void Start()
    {
        print("lvl 1 quest: " + qm.GetQuestState("CompletePlatformerLevel1Quest"));
        print("lvl 2 quest: " + qm.GetQuestState("CompletePlatformerLevel2Quest"));
    }
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Debug.Log("complete");
            GameEventsManager.instance.playerEvents.PlatformerLevelCompleted(levelNumber);
            print(qm.GetQuestState("CompletePlatformerLevel1Quest"));
        }

        //FOR DEBUG PURPOSES ONLY -- DELETE LATER
        /*
        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        } */

    }
}
