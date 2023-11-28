using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private int enemyCount;

    void Start()
    {
        // Initialize enemyCount with the number of enemies in the scene
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyDefeated()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            // Load next scene when all enemies are defeated
            SceneManager.LoadScene("Salsa_Level3"); // Replace with your next scene name
        }
    }
}
