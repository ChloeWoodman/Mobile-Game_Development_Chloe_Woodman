using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Diagnostics;

public class AttackButton : MonoBehaviour
{
    public float attackRange = 2.0f; // Adjust this value to set the attack range

    public void Attack()
    {
        // Get the position of the player
        Vector3 playerPosition = transform.position; // Assuming the script is on the player GameObject

        // Find all ground-based enemies with the "GroundEnemy" tag and apply damage to them
        GameObject[] groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
        foreach (GameObject enemyObject in groundEnemies)
        {
            Vector3 enemyPosition = enemyObject.transform.position;
            if (Vector3.Distance(playerPosition, enemyPosition) <= attackRange)
            {
                EnemyAI enemy = enemyObject.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    int damage = enemy.damageAmount;
                    enemy.TakeDamage(damage); // Apply damage to the enemy
                    UnityEngine.Debug.Log("Player attacked a ground enemy. Health: " + enemy.health);
                }
            }
        }

        // Find all flying enemies with the "FlyingEnemy" tag and apply damage to them
        GameObject[] flyingEnemies = GameObject.FindGameObjectsWithTag("FlyingEnemy");
        foreach (GameObject enemyObject in flyingEnemies)
        {
            Vector3 enemyPosition = enemyObject.transform.position;
            if (Vector3.Distance(playerPosition, enemyPosition) <= attackRange)
            {
                FlyingEnemy enemy = enemyObject.GetComponent<FlyingEnemy>();
                if (enemy != null)
                {
                    int damage = enemy.damageAmount;
                    enemy.TakeDamage(damage); // Apply damage to the enemy
                    UnityEngine.Debug.Log("Player attacked a flying enemy. Health: " + enemy.health);
                }
            }
        }
    }
}
