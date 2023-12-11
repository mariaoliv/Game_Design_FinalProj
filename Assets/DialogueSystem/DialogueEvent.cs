using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Make sure you have this using directive to access scene management

public class DialogueEvent : MonoBehaviour
{
    [SerializeField] DialogueAsset   dialogue; // Must be able to refer to incoming dialogue assets from events
    //private DialogueAsset dialogue;

    [SerializeField] GameObject      dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image           charPortrait;
    
    [SerializeField] float dialogueDelay = 0.05f;
    [SerializeField] float autoAdvance   = 0.0f; // 0 = disabled

    public Image Pavo_Angry;
    public Image Pavo_Confused;
    public Image Pavo_Happy;
    public Image Pavo_Neutral;
    public Image Pavo_Sad;
    public Image Salsa_Angry;
    public Image Salsa_Confused;
    public Image Salsa_Happy;
    public Image Salsa_Neutral;
    public Image Salsa_Sad;

    public AudioClip SalsaAudio;
    public AudioClip PavoAudio;
    
    private bool    dialogueActive;
    private bool    dialogueScroll;
    private int     currLine;
    private int     dialogueLength;
    private string  currText;
    private Image   currPortrait;
    //public string sceneToLoad = "overworld_CJ"; // The name of the scene you want to load

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueActive = false;
        dialogueScroll = false;
        currLine = 0;
        dialogueLength = dialogue.lines.Length;
        currText = "";
        currPortrait = null;
    }

    
    void Update()
    {

        if(Input.GetButtonDown("Jump") && dialogueActive)
        {
            ShowDialogue(dialogue.speakers, dialogue.lines, dialogue.emotion);
        }

        // Automatically advance dialogue if enough time passes
        if(dialogueActive && !dialogueScroll)
        {
            
        }
        // Check if the left mouse button was clicked
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // Load the specified scene
        //    SceneManager.LoadScene(sceneToLoad);
        //}
    }

    public void callDialogue()
    {
        ShowDialogue(dialogue.speakers, dialogue.lines, dialogue.emotion);
    }

    public void ShowDialogue(string[] speakers, string[] lines, string[] emotion)
    {
        if(dialogueActive == false)
        {
            dialoguePanel.SetActive(true);
            nameText.text = speakers[0];
            currText = lines[0];
            dialogueText.text = lines[0];
            dialogueActive = true;
        }
        else
        {
            currLine++;
            if(currLine >= dialogueLength)
            {
                HideDialogue();
                currLine = 0; // Reset once hidden
            }
            else
            {
                nameText.text = speakers[currLine];
                currText = lines[currLine];

                //Change portrait based on speaker and emotion
                switch(speakers[currLine])
                {
                    case("Pavo"):
                        switch(emotion[currLine])
                        {
                            case("Angry"):
                                currPortrait = Pavo_Angry;
                                break;

                            case("Confused"):
                                currPortrait = Pavo_Confused;
                                break;

                            case("Happy"):
                                currPortrait = Pavo_Happy;
                                break;

                            case("Neutral"):
                                currPortrait = Pavo_Neutral;
                                break;

                            case("Sad"):
                                currPortrait = Pavo_Sad;
                                break;

                            default:
                                currPortrait = null;
                                break;
                        }
                    
                        break;

                    case("Salsa"):
                        switch(emotion[currLine])
                        {
                            case("Angry"):
                                currPortrait = Salsa_Angry;
                                break;

                            case("Confused"):
                                currPortrait = Salsa_Confused;
                                break;

                            case("Happy"):
                                currPortrait = Salsa_Happy;
                                break;

                            case("Neutral"):
                                currPortrait = Salsa_Neutral;
                                break;

                            case("Sad"):
                                currPortrait = Salsa_Sad;
                                break;

                            default:
                                currPortrait = null;
                                break;
                        }

                        break;

                    default:
                        currPortrait = null;
                        break;
                }
            }

        charPortrait = currPortrait;

        }

        StartCoroutine(ScrollText());
    }
    
    public void HideDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;;
        dialoguePanel.SetActive(false);
        dialogueActive = false;
        currPortrait = null;
    }

    // Text fills window over time
    private IEnumerator ScrollText()
    {
        dialogueText.text = "";
        dialogueScroll = true;

        foreach(char c in currText.ToCharArray())
        {
            dialogueText.text += c;
            switch(c)
            {
                case('.'):
                    // Wait longer at end of sentence
                    break;

                default:
                    yield return new WaitForSecondsRealtime(dialogueDelay);
                    break;
            }
        }

        dialogueScroll = false;
        yield return null;
    }

    // Play audio clip at varying pitch while dialogue is scrolling
    private IEnumerator PlaySound()
    {
        yield return null;
    }

}