using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TaskManager_fires : MonoBehaviour
{
    public int totalFires = 7; // Total number of fires to put out
    private int firesPutOut = 0; // Current number of fires put out
    private Text taskStatusText; // Text UI element for task status
    public Font textFont;

    void Start()
    {
        // Create the task status Text UI element
        GameObject canvasGameObject = GameObject.Find("TaskStatusCanvas");
        if (canvasGameObject == null) // Create a new canvas if it doesn't exist
        {
            canvasGameObject = new GameObject("TaskStatusCanvas");
            Canvas canvas = canvasGameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGameObject.AddComponent<CanvasScaler>();
            canvasGameObject.AddComponent<GraphicRaycaster>();
        }

        GameObject textGameObject = new GameObject("TaskStatusText");
        textGameObject.transform.SetParent(canvasGameObject.transform);
        taskStatusText = textGameObject.AddComponent<Text>();
        taskStatusText.font = textFont; // Use the assigned fonts
        taskStatusText.fontSize = 20;
        taskStatusText.color = Color.white;
        taskStatusText.alignment = TextAnchor.UpperRight;

        RectTransform rectTransform = textGameObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(1, 1);
        rectTransform.sizeDelta = new Vector2(300, 100);
        rectTransform.anchoredPosition = new Vector2(-10, -10);

        UpdateTaskStatus();
    }

    void UpdateTaskStatus()
    {
        taskStatusText.text = $"Put out fires: {firesPutOut}/{totalFires}";
        if (firesPutOut >= totalFires)
        {
            StartTransitionSequence();
        }
    }

    public void FirePutOut()
    {
        Debug.Log("FirePutOut called");
        firesPutOut++;
        UpdateTaskStatus();
    }

    public PlayerTextDisplay playerTextDisplay; // Reference to PlayerTextDisplay script

    public void StartTransitionSequence()
    {
        string[] transitionSentences = new string[]
        {
            "With the fires extinguished",
            "Pavo feels a change in the air...",
            "With a wave of dizziness",
            "Pava seemed to see Salsa..."
        };

        playerTextDisplay.SetSentences(transitionSentences);
        StartCoroutine(TransitionAfterText());
    }

    IEnumerator TransitionAfterText()
    {
        yield return new WaitForSeconds(playerTextDisplay.sentences.Length * playerTextDisplay.displayTime);
        SceneManager.LoadScene("Platformer_CJ");
    }
}
