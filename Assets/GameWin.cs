using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{ 
    public int ecoBotsKilled = 0;
    public float delayInSeconds = 3f;
    // Start is called before the first frame update

    private void Start()
    {
        ecoBotsKilled = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (ecoBotsKilled == 3)
        {
            if (SceneManager.GetActiveScene().name == "New_Level1")
            {
                Manager_Script.Level1Completed = true;
            }
            else if (SceneManager.GetActiveScene().name == "New_Level2")
            {
                Manager_Script.Level2Completed = true;
            }
            else if (SceneManager.GetActiveScene().name == "New_Level3")
            {
                Manager_Script.Level3Completed = true;
            }
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        SalsaController.respawnCount = 0;
       
        SceneManager.LoadScene("New_Overworld");
    }
}
