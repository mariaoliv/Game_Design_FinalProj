using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloor : MonoBehaviour
{
    public Sprite floorSprite;

    private void Start()
    {
        GameObject floor = new GameObject("Floor");

        SpriteRenderer renderer = floor.AddComponent<SpriteRenderer>();
        renderer.sprite = floorSprite;

        Vector2 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float screenWidth = upperRight.x - lowerLeft.x;
        float screenHeight = upperRight.y - lowerLeft.y;

        float floorHeight = renderer.bounds.size.y;
        float scaleHeight = (screenHeight * 0.1f) / floorHeight;

        float floorWidth = renderer.bounds.size.x;
        float scaleWidth = screenWidth / floorWidth;

        floor.transform.localScale = new Vector3(scaleWidth, scaleHeight, 1);

        Vector2 floorPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0));

        floor.transform.position = new Vector3(floorPosition.x, lowerLeft.y + (floorHeight * scaleHeight) / 2, 0);

        BoxCollider2D collider = floor.AddComponent<BoxCollider2D>();

        collider.size = new Vector2(floorWidth, floorHeight);
    }
}
