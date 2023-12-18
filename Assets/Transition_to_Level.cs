using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Transition_to_Level : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private int index;

    public float wordSpeed;

    public float delayInSeconds = 5f;
    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
        nameText.text = "";
        dialogueText.text = "";
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        if (gameObject.name == "Level1Entry")
        {
            SceneManager.LoadScene("New_Level1");
        }
        else if (gameObject.name == "Level2Entry")
        {
            SceneManager.LoadScene("New_Level2");
        }
        else if (gameObject.name == "Level3Entry")
        {
            SceneManager.LoadScene("New_Level3");
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
     
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                Typing();
            }
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            zeroText();
        }
    }

    void Typing()
    {
        nameText.text = "Pavo";
        dialogueText.text = "I think I found someone!";
    }

    public void zeroText()
    {
        nameText.text = "";
        dialogueText.text = "";
        index = 0;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        
    }


}
