using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
            if (gameObject.name == "queen" && !Manager_Script.amuletReceived)
            {

                Manager_Script.amuletReceived = true;
                SceneManager.LoadScene("New_Overworld");
            }
        }
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

    IEnumerator Typing()
    {
        if (gameObject.name == "queen")
        {
            if (Manager_Script.amuletReceived == false)
            {
                nameText.text = "Queen Tango";
                dialogue[0] = "Hello Pavo! I want to thank you again for letting us move in so close to your nest.";
                dialogue[1] = "The King is really upset about having to leave our home tree. Those EcoCorp bots really did a number on our island. Thankfully, I think I found a solution. I've been conducting research on a family heirloom. ";
                dialogue[2] = "I've come to the conclusion that it is a communication device, it allows wearers from different points of time to talk to each other.";
                dialogue[3] = "Navigate to the different trees on this island and use the device to see if you can find an ancestor to destroy the EcoCorp bots in that area before they've caused too much damage. I suggest starting at the West Tree, then East Tree, then the Royal Tree.";
                dialogue[4] = "*Pavo receives the amulet*";
            }
            else
            {
                nameText.text = "Queen Tango";
                dialogue[0] = "Hello again. Remember your mission!";
                dialogue[1] = "Save the Island! Navigate to the different trees on this island and use the device to see if you can find an ancestor to destroy the Ecocorp bots in that area before they've caused too much damage.";
                dialogue[2] = "I suggest starting at the West Tree, then East Tree, then the Royal Tree.";
                dialogue[3] = "Good Luck on your mission Pavo, I have the upmost confidence that you'll be successful.";
                dialogue[4] = "Godspeed.";
            }

        }
        else if (gameObject.name == "king")
        {
            nameText.text = "King Salsa III";
            dialogue[0] = "Greetings Pavo. I'll be honest with you. I am not well.";
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
            dialogue[0] = "Hi Pavo. I know I keep saying this, but thank you for letting us crash next to your nest.";
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
        else if (gameObject.name == "queen_2")
        {
            nameText.text = "Queen Tango";
            dialogue[0] = "Congrats Pavo! It seems as if you have saved the royal Tree from EcoCorp!";
            dialogue[1] = "The king is finally in good spirits again.";
            dialogue[2] = "Thank you for your help, know that you'll forever be a friend.";
            dialogue[3] = "In fact, I want to make you our very first non-monkey knight.";
            dialogue[4] = "You are now Sir Pavo.";
        }
        else if (gameObject.name == "king_2")
        {
            nameText.text = "King Salsa III";
            dialogue[0] = "Pavo my good friend, how are you?";
            dialogue[1] = "Our royal tree is back, somehow. I'm not sure what I did to fix it, but it must have been very clever.";
            dialogue[2] = "All that matters is that things are back to how they were.";
            dialogue[3] = "That's not to say I didn't enjoy your neighborship. Because I did.";
            dialogue[4] = "By the way, that's a nice amulet you got there. Can I have it?";
        }
        else if (gameObject.name == "tophat_2")
        {
            nameText.text = "Pops";
            dialogue[0] = "Pavo! I told you the West tree was going to be ready in no time!";
            dialogue[1] = "Me and my son thank you for your generosity while we were your guests.";
            dialogue[2] = "*Pops tips his top hat in respect*";
            dialogue[3] = "Here, something for your troubles.";
            dialogue[4] = "*Pops hands Pavo a banana...this one's fresh.*";
        }
        else if (gameObject.name == "backwards_hat_2")
        {
            nameText.text = "Pops Jr.";
            dialogue[0] = "Yo.";
            dialogue[1] = "I know you were the one to save the West Tree.";
            dialogue[2] = "I'm good at reading people, and I could tell you're visiting us to see how grateful we are.";
            dialogue[3] = "Kind of vain, but you deserve it I guess.";
            dialogue[4] = "Anyways, respect yo.";
        }
        else if (gameObject.name == "pinky_2")
        {
            nameText.text = "Pinky";
            dialogue[0] = "Pavo! I don't know what happened but the East Tree is alive again!";
            dialogue[1] = "For some reason, I want to thank you for this. Although I'm not sure why.";
            dialogue[2] = "Well, in any case, I still need to thank you for letting me and my husband stay with you.";
            dialogue[3] = "Speaking of my husband, I'm not sure what's going on with him?";
            dialogue[4] = "He seems to be sad now that Eco Corp is gone.";
        }
        else if (gameObject.name == "ecocorp_fan_2")
        {
            nameText.text = "Stan";
            dialogue[0] = "This is a travesty.";
            dialogue[1] = "What will we do now that Eco Corp is gone?";
            dialogue[2] = "They did alot of bad, I admit that.";
            dialogue[3] = "But they also did a lot of good, like this corporate merchandise that they gave me.";
            dialogue[4] = "Hopefully, they come back one day. A monkey can only hope.";
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
