using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour
{
    public float transitionTime = 1.0f; // Duration of the transition
    public string sceneToLoad; // Name of the scene to load
    public QuestManager qm; // Reference to the QuestManager
    public string Level_Name; // Name of the current level
    public Window_QuestPointer pointer; // Reference to the QuestPointer
    public int levelNumber = -1; // Optional level number, if applicable

    private bool transitionStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !transitionStarted)
        {
            transitionStarted = true;
            StartTransitionSequence();
        }
    }

    public void StartTransitionSequence()
    {
        // Any additional logic for starting the transition
        StartCoroutine(TransitionAfterText());
    }

    IEnumerator TransitionAfterText()
    {
        // Placeholder for any pre-transition logic like displaying text
        // Example: yield return new WaitForSeconds(displayTime);

        // Trigger any transition animations here (if any)
        // Example: transitionAnimator.SetTrigger("StartFadeOut");

        // Handle any game events related to completing the level
        if (levelNumber != -1)
        {
            GameEventsManager.instance.playerEvents.PlatformerLevelCompleted(levelNumber);
        }

        // Save game state if necessary
        qm.SaveAllQuests();
        pointer?.SaveQuestId(); // Using null-conditional operator for safety

        // Wait for the animation or delay to finish
        yield return new WaitForSeconds(transitionTime);

        // Mark the level as completed
        LevelsManager.SetLevelCompleted(Level_Name, true);

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
