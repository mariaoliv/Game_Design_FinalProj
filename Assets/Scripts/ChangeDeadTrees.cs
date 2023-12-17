using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDeadTrees : MonoBehaviour
{
    [SerializeField] GameObject deadTree1;
    [SerializeField] string lvl1;
    [SerializeField] GameObject deadTree2;
    [SerializeField] string lvl2;
    [SerializeField] GameObject deadTree3;
    [SerializeField] string lvl3;
    void Start()
    {

    }


    void Update()
    {
        if (LevelsManager.IsLevelCompleted(lvl1))
        {
            deadTree1.SetActive(false);
        }
        if (LevelsManager.IsLevelCompleted(lvl2))
        {
            deadTree2.SetActive(false);
        }
        if (LevelsManager.IsLevelCompleted(lvl3))
        {
            deadTree3.SetActive(false);
        }
    }
}
