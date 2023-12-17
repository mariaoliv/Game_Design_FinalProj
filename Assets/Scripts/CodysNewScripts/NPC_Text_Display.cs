using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_Text_Display : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public string[] sentences;
    public float displayTime = 3.0f;
    private int currentSentenceIndex = 0;
    private Text textDisplay;
    public Font textFont;
    public float activationDistance; // Distance within which player can activate text
    private GameObject canvasGameObject; // Reference to the canvas GameObject

    void Start()
    {
        // Create Text UI element
        canvasGameObject = new GameObject("NPCTextCanvas");
        Canvas canvas = canvasGameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        // Position the canvas as a child of the NPC
        canvasGameObject.transform.SetParent(transform, false);
        canvasGameObject.transform.localPosition = new Vector3(0, 2, 0);

        CanvasScaler cs = canvasGameObject.AddComponent<CanvasScaler>();
        cs.scaleFactor = 1.0f;
        cs.dynamicPixelsPerUnit = 100f;
        canvasGameObject.AddComponent<GraphicRaycaster>();

        RectTransform canvasRectTransform = canvasGameObject.GetComponent<RectTransform>();
        canvasRectTransform.sizeDelta = new Vector2(400, 200);

        GameObject textGameObject = new GameObject("NPCText");
        textGameObject.transform.SetParent(canvasGameObject.transform, false);
        textGameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        textDisplay = textGameObject.AddComponent<Text>();
        textDisplay.font = textFont; // Use the assigned font
        textDisplay.fontSize = 14; // Increased font size for visibility
        textDisplay.color = Color.white;
        textDisplay.alignment = TextAnchor.MiddleCenter;

        RectTransform textRectTransform = textGameObject.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(400, 200);
        textRectTransform.localPosition = Vector3.zero;

        canvasGameObject.SetActive(false); // Initially hide the canvas
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("ENTER pressed");
                StartCoroutine(ShowNextSentence());
            }
        }
    }

    IEnumerator ShowNextSentence()
    {
        if (currentSentenceIndex < sentences.Length)
        {
            canvasGameObject.SetActive(true);
            textDisplay.text = sentences[currentSentenceIndex];
            Debug.Log("Current sentence: " + sentences[currentSentenceIndex]);

            currentSentenceIndex++;
            yield return new WaitForSeconds(displayTime);
        }
        else
        {
            canvasGameObject.SetActive(false);
            currentSentenceIndex = 0;
        }
    }
}
