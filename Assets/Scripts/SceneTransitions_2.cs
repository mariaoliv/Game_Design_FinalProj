using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitions_2 : MonoBehaviour
{
    public Animator transitionAnimator;

    private bool transitionStarted = false;

    public string SceneToLoad = "MainMenu";

    public QuestManager qm;

    public Window_QuestPointer pointer;

    public int levelNumber = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !transitionStarted)
        {
            transitionStarted = true;
            StartTransitionSequence();
        }
    }

    public PlayerTextDisplay playerTextDisplay; // Reference to PlayerTextDisplay script

    public void StartTransitionSequence()
    {
        if (levelNumber != -1)
        {
            GameEventsManager.instance.playerEvents.PlatformerLevelCompleted(levelNumber);
        }

        qm.SaveAllQuests();

        if (pointer != null)
        {
            pointer.SaveQuestId();
        }

        string[] transitionSentences = new string[]
        {
            "I see a factory...",
            "I need to find more!"
        };

        playerTextDisplay.SetSentences(transitionSentences);
        StartCoroutine(TransitionAfterText());
    }

    IEnumerator TransitionAfterText()
    {
        yield return new WaitForSeconds(playerTextDisplay.sentences.Length * playerTextDisplay.displayTime);

        // Trigger the FadeOut animation.
        transitionAnimator.SetTrigger("StartFadeOut");
        
        //SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene(SceneToLoad);
    }

    
}
