using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Color dimColor = sr.color;
        dimColor.a = 0.5f;
        sr.color = dimColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("collided");
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            other.GetComponent<SalsaController>().SetRespawnPoint(transform.position);
        }
    }
}