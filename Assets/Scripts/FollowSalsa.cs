using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSalsa : MonoBehaviour
{
    public Transform playerTransform;
    private float yOffset = 1.2f;

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, transform.position.z);
        }
    }
}
