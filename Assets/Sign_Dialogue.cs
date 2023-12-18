using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private string[] dialogue = new string[1];
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
        if (gameObject.name == "sign1")
        {
            dialogue[0] = "Press and hold the left mouse button to grab the vine. While grabbing the vine, use 'A' to swing left and 'D' to swing right. Release the left mouse button to let go of the vine.";
        }
        else if (gameObject.name == "sign2")
        {
            dialogue[0] = "While grabbing onto one vine, you can touch another to grab onto both. While grabbing two vines, you can pull down by holding down 'S'. Release hold of the vines to launch yourself upwards.";
        }
        else if (gameObject.name == "sign3")
        {
            dialogue[0] = "Spikes kill you.";
        }
        else if (gameObject.name == "sign4")
        {
            dialogue[0] = "Be cautious of Defense Robots, as they can eliminate you. To counter them, press 'E' to destroy them first. Upon their destruction, you will bounce off them.";
        }
        else if (gameObject.name == "sign5")
        {
            dialogue[0] = "Bananas are checkpoints.";
        }
        else if (gameObject.name == "amulet")
        {
            nameText.text = "Salsa";
            dialogue[0] = "An amulet?? I wonder what it does.";
        }
        dialogueText.text = dialogue[0];
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
