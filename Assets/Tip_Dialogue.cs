using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tip_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private int index;

    public float wordSpeed;
    public bool playerIsClose;


    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
        nameText.text = "";
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }
    }

    public void zeroText()
    {
        nameText.text = "";
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    void Typing()
    {
        nameText.text = "Tip";
        dialogueText.text = "WASD to move. Press 'E' to interact with your environment.";
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            zeroText();
        }
    }


}
