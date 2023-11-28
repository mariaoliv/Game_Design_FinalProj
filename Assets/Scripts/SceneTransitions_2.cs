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
        qm.SaveAllQuests();

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
