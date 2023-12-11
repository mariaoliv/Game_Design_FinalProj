using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Queen_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private string[] dialogue = new string[5];
    private int index;

    public float wordSpeed;
    public bool playerIsClose;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = "";
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if(dialogueText.text == dialogue[index])
            {
                NextLine();
                //dialoguePanel.SetActive(true);
                //StartCoroutine(Typing());
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
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

    IEnumerator Typing()
    {
        if (gameObject.name == "queen")
        {
            nameText.text = "Queen Tango";
            dialogue[0] = "Hello Salsa! I want to thank you again for letting us move in so close to your nest.";
            dialogue[1] = "The King is really upset about having to leave our home tree. Those EcoCorp bots really did a number on our island. Thankfully, I think I found a solution. I've been conducting research on a family heirloom. ";
            dialogue[2] = "I've come to the conclusion that it is a communication device, it allows wearers from different points of time to talk to each other.";
            dialogue[3] = "Navigate to the different trees on this island and use the device to see if you can find an ancestor to destroy the EcoCorp bots in that area before they've caused too much damage.";
            dialogue[4] = "*Pavo receives the amulet*";

        }
        else if (gameObject.name == "king")
        {
            nameText.text = "King Salsa III";
            dialogue[0] = "Greetings Salsa. I'll be honest with you. I am not well.";
            dialogue[1] = "When Eco Corp first moved in, they promised my ancestors that they were 'environmentally conscious'.";
            dialogue[2] = "That was a lie. They have drained our trees of all their life, making them unlivable. ";
            dialogue[3] = "My Queen seems to have found a way to reverse the damage, but between me and you it sounds like a tall tale.";
            dialogue[4] = "Oh well, atleast we have our tent.";
        }
        else if (gameObject.name == "tophat")
        {
            nameText.text = "Pops";
            dialogue[0] = "Pavo, my compatriot. How are you doing this fine morning?";
            dialogue[1] = "What's that? How am I holding up? Well, things are dandy.";
            dialogue[2] = "The West Tree is still under renovation, but the new furnishes should be ready soon.";
            dialogue[3] = "Me and my son will be out of your hair in no time. Here's something for your troubles.";
            dialogue[4] = "*Pops hands Pavo a banana...it seems rotten.*";
        }
        else if (gameObject.name == "backwards_hat")
        {
            nameText.text = "Pops Jr.";
            dialogue[0] = "Yo.";
            dialogue[1] = "My dad seems to be in denial about this whole losing our home business.";
            dialogue[2] = "It'd be dope if you could do something about it.";
            dialogue[3] = "We used to live in the West Tree, until Eco corp forced us to dip. They drained every last drop of our tree.";
            dialogue[4] = "We used to be living lavish. Now we live in that tent.";
        }
        else if (gameObject.name == "pinky")
        {
            nameText.text = "Pinky";
            dialogue[0] = "Hi Salsa. I know I keep saying this, but thank you for letting us crash next to your nest.";
            dialogue[1] = "It seems to be one of the only places that Eco corp hasn't ruined.";
            dialogue[2] = "Me and my husband used to live in the East Tree, if only there was a way to bring back what once was.";
            dialogue[3] = "Despite all this, my husband seems to be a fan of Eco corp.";
            dialogue[4] = "He even wears their merch.";
        }
        else if (gameObject.name == "ecocorp_fan")
        {
            nameText.text = "Stan";
            dialogue[0] = "You know what? I think Eco corp gets a bad wrap. I don't think they're all that bad.";
            dialogue[1] = "Sure they came in and installed themselves on our trees without our permission.";
            dialogue[2] = "Sure they were dishonest with us about their motives.";
            dialogue[3] = "Sure they drained all the life out of our trees and forced us to move out.Sure they..... wait";
            dialogue[4] = "Those are all bad things.";
        }
        else if (gameObject.name == "skully")
        {
            nameText.text = "Skully";
            dialogue[0] = "Hey bird.";
            dialogue[1] = "What?";
            dialogue[2] = "Why isn't my tree dead like the rest?";
            dialogue[3] = "Because my grandaddy never let those darn robots move in.";
            dialogue[4] = "That's why bird.";
        }
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsClose = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsClose = false;
            zeroText();
        }
    }


}
