using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerTextDisplay : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public string[] sentences;
    public float displayTime = 3.0f;
    private int currentSentenceIndex = 0;
    private Text textDisplay;
    public Font textFont;

    void Start()
    {
        // Create Text UI element
        GameObject canvasGameObject = new GameObject("PlayerTextCanvas");
        Canvas canvas = canvasGameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        // Position the canvas as a child of the player
        canvasGameObject.transform.SetParent(player.transform, false);
        canvasGameObject.transform.localPosition = new Vector3(0, 2, 0); // Adjust this as needed

        CanvasScaler cs = canvasGameObject.AddComponent<CanvasScaler>();
        cs.scaleFactor = 1.0f;
        cs.dynamicPixelsPerUnit = 100f;
        canvasGameObject.AddComponent<GraphicRaycaster>();

        RectTransform canvasRectTransform = canvasGameObject.GetComponent<RectTransform>();
        canvasRectTransform.sizeDelta = new Vector2(400, 200);

        GameObject textGameObject = new GameObject("PlayerText");
        textGameObject.transform.SetParent(canvasGameObject.transform, false);
        textGameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        textDisplay = textGameObject.AddComponent<Text>();
        textDisplay.font = textFont; // Use the assigned font
        textDisplay.text = sentences[0];
        textDisplay.fontSize = 1;
        textDisplay.color = Color.white;
        textDisplay.alignment = TextAnchor.MiddleCenter;

        RectTransform textRectTransform = textGameObject.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(400, 200);
        textRectTransform.localPosition = Vector3.zero;

        StartCoroutine(DisplaySentences());
    }


    public void SetSentences(string[] newSentences)
    {
        sentences = newSentences;
        currentSentenceIndex = 0;
        StartCoroutine(DisplaySentences());
    }

    IEnumerator DisplaySentences()
    {
        while (currentSentenceIndex < sentences.Length)
        {
            textDisplay.text = sentences[currentSentenceIndex];
            currentSentenceIndex++;
            yield return new WaitForSeconds(displayTime);
        }

        textDisplay.text = ""; // Optional: hide the text after all sentences are displayed
    }
}
