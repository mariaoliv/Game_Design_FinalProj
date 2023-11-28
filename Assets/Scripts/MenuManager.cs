using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string startScene;
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("Loading level: " + startScene);
        SceneManager.LoadScene(startScene);
    }

    
}
