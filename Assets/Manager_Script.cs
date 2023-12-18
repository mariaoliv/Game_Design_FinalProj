using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Script : MonoBehaviour
{
    public GameObject dialoguePanel;
    public static bool Level1Completed { get; set; }
    public static bool Level2Completed { get; set; }
    public static bool Level3Completed { get; set; }
    public static bool amuletReceived { get; set; }

    public GameObject deadWestTilemap;
    public GameObject deadEastTilemap;
    public GameObject deadRoyalTilemap;

    public GameObject westTreeAlive;
    public GameObject eastTreeAlive;
    public GameObject royalTreeAlive;

    public GameObject westTreeDead;
    public GameObject eastTreeDead;
    public GameObject royalTreeDead;

    public GameObject westRobot1;
    public GameObject westRobot2;
    public GameObject westRobot3;

    public GameObject eastRobot1;
    public GameObject eastRobot2;
    public GameObject eastRobot3;

    public GameObject royalRobot1;
    public GameObject royalRobot2;
    public GameObject royalRobot3;

    public GameObject king;
    public GameObject king2;

    public GameObject queen;
    public GameObject queen2;

    public GameObject pops;
    public GameObject pops2;

    public GameObject popsjr;
    public GameObject popsjr2;

    public GameObject pinky;
    public GameObject pinky2;

    public GameObject stan;
    public GameObject stan2;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;


    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        if (Level1Completed)
        {
            deadWestTilemap.SetActive(false);
            westTreeDead.SetActive(false);
            westRobot1.SetActive(false);
            westRobot2.SetActive(false);
            westRobot3.SetActive(false);
            westTreeAlive.SetActive(true);
            pops.SetActive(false);
            popsjr.SetActive(false);
            pops2.SetActive(true);
            popsjr2.SetActive(true);
            star1.SetActive(false);
        }
        else
        {
            westTreeAlive.SetActive(false);
            deadWestTilemap.SetActive(true);
;           westTreeDead.SetActive(true);
            westRobot1.SetActive(true);
            westRobot2.SetActive(true);
            westRobot3.SetActive(true);
            pops.SetActive(true);
            popsjr.SetActive(true);
            pops2.SetActive(false);
            popsjr2.SetActive(false);
            if (amuletReceived)
            {
                star1.SetActive(true);
            }
        }

        if (Level2Completed)
        {
            deadEastTilemap.SetActive(false);
            eastTreeDead.SetActive(false);
            eastRobot1.SetActive(false);
            eastRobot2.SetActive(false);
            eastRobot3.SetActive(false);
            eastTreeAlive.SetActive(true);
            pinky.SetActive(false);
            stan.SetActive(false);
            pinky2.SetActive(true);
            stan2.SetActive(true);
            star2.SetActive(false);
        }
        else
        {
            eastTreeAlive.SetActive(false);
            deadEastTilemap.SetActive(true);
            eastTreeDead.SetActive(true);
            eastRobot1.SetActive(true);
            eastRobot2.SetActive(true);
            eastRobot3.SetActive(true);
            pinky.SetActive(true);
            stan.SetActive(true);
            pinky2.SetActive(false);
            stan2.SetActive(false);
            if (amuletReceived)
            {
                star2.SetActive(true);
            }
            
        }

        if (Level3Completed)
        {
            deadRoyalTilemap.SetActive(false);
            royalTreeDead.SetActive(false);
            royalRobot1.SetActive(false);
            royalRobot2.SetActive(false);
            royalRobot3.SetActive(false);
            royalTreeAlive.SetActive(true);
            king.SetActive(false);
            queen.SetActive(false);
            king2.SetActive(true);
            queen2.SetActive(true);
            star3.SetActive(false);
        }
        else
        {
            royalTreeAlive.SetActive(false);
            deadRoyalTilemap.SetActive(true);
            royalTreeDead.SetActive(true);
            royalRobot1.SetActive(true);
            royalRobot2.SetActive(true);
            royalRobot3.SetActive(true);
            king.SetActive(true);
            queen.SetActive(true);
            king2.SetActive(false);
            queen2.SetActive(false);
            if (amuletReceived)
            {
                star3.SetActive(true);
            }
           
        }
    }
 
}
