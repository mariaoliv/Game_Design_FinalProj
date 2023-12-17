using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string sentence;
    public float typingSpeed = 0.1f;
    public float delayAfterText = 4.0f; // Delay after text is displayed
    public string nextSceneName; // Name of the next scene to load

    void Start()
    {
        StartCoroutine(DisplayTextAndTransition());
    }

    IEnumerator DisplayTextAndTransition()
    {
        // Display the sentence word by word
        textDisplay.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait for the specified delay time
        yield return new WaitForSeconds(delayAfterText);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
