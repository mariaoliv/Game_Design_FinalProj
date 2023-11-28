using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    private Queue<Vector3> positions;
    public float recordTime = 3f;
    private float timer;

    void Start()
    {
        positions = new Queue<Vector3>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.1f) // Record position every 0.1 seconds
        {
            positions.Enqueue(transform.position);
            timer = 0f;
        }

        // Keep only the last 5 seconds of positions
        if (positions.Count > recordTime * 10)
        {
            positions.Dequeue();
        }

        // Check for time travel trigger (pressing 'T')
        if (Input.GetKeyDown(KeyCode.T))
        {
            TimeTravelBack();
        }
    }

    void TimeTravelBack()
    {
        if (positions.Count > 0)
        {
            Vector3 oldPosition = positions.Dequeue();
            transform.position = oldPosition;
        }
    }
}
