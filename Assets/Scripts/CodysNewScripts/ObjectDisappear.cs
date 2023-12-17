using UnityEngine;

public class ObjectDisappear : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public string requiredLevelName; // The name of the level that needs to be completed
    public float activationDistance = 5.0f; // Distance within which player can make the object disappear

    void Update()
    {
        if (LevelsManager.IsLevelCompleted(requiredLevelName) && 
            Vector3.Distance(player.transform.position, transform.position) <= activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
