using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggCode : MonoBehaviour
{
    public GameObject target;
    public QuestManager qm;
    bool following = false;
    bool finished = false;
    public Rigidbody2D eggRB;
    public nestScript nest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (following && !finished)
        {
            //transform.position = Vector2.MoveTowards(transform.position, target.transform.position + new Vector3(1,0,0), Time.deltaTime * 10);
            var dif = target.transform.position - transform.position;
            if(dif.magnitude > 0.5)
            {
                //eggRB.AddForce(dif * 100 * Time.deltaTime);
                eggRB.velocity = dif*5f;
            }
            if(dif.magnitude <= 0.5)
            {
                eggRB.velocity = Vector3.zero;
            }
        }
        else if(following)
        {
            eggRB.velocity = Vector3.zero;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 10);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collided");
        
        if (col.gameObject.CompareTag("Nest") && !finished){
            finished = true;
            target = col.gameObject;
            nest.eggCount++;
            GameEventsManager.instance.miscEvents.EggBroughtBack();
        }
        if (col.gameObject.CompareTag("Player") && !finished && (qm.GetQuestState("EggsQuest") == QuestState.IN_PROGRESS))
        {
            following = true;
            target = col.gameObject;
        }
    }
    public bool EggIsInNest()
    {
        return finished;
    }
}
