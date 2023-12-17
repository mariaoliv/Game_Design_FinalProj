using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitions : MonoBehaviour
{

    //public Animator transitionAnimator;
    public float transitionTime;
    public string sceneToLoad; // Name of the scene to load
    public QuestManager qm;

    public Window_QuestPointer pointer;

    //public bool inPlatformer;
    public int levelNumber = -1;
    public string Level_Name; // Name of the current level

    //public Animator transitionAnimator;


    private bool transitionStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !transitionStarted)
        {
            transitionStarted = true;
            StartTransitionSequence();
        }
    }

    // COMMENT OUT
    //public PlayerTextDisplay playerTextDisplay; // Reference to PlayerTextDisplay script

    public void StartTransitionSequence()
    {
        string[] transitionSentences = new string[]
        {
            "An amulet?!",
            "Looks pretty!",
            "Wait..I hear something..",
            "Pavo, Is that you?"
        };

        // COMMENT OUT
        //playerTextDisplay.SetSentences(transitionSentences);
        StartCoroutine(TransitionAfterText());
    }

    IEnumerator TransitionAfterText()
    {
        //COMMENTED OUT:
        //yield return new WaitForSeconds(playerTextDisplay.sentences.Length * playerTextDisplay.displayTime);
       

        // Trigger the FadeOut animation.

        //transitionAnimator.SetTrigger("StartFadeOut");

        if (levelNumber != -1)
        {
            GameEventsManager.instance.playerEvents.PlatformerLevelCompleted(levelNumber);
        }

        // qm.SaveAllQuests(); // the if statement was deleted because the progress of completing the platformer level didn't get saved

        if (pointer != null)
        {
            pointer.SaveQuestId();
        }

        /*
        if (SceneManager.GetActiveScene().name == "Overworld")
        {
            qm.SaveAllQuests();
        } */

        SalsaController.respawnCount = 0;

        // Wait for the animation to finish.
        yield return new WaitForSeconds(transitionTime);

        // Mark the level as completed
        LevelsManager.SetLevelCompleted(Level_Name, true);

        //transitionAnimator.SetTrigger("StartFadeOut");
        // Load the new scene.
        SceneManager.LoadScene(sceneToLoad);

    }

    
}
