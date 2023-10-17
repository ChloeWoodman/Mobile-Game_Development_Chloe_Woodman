using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Diagnostics;

public class AttackButton : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public float attackRange = 2.0f; // Adjust this value to set the attack range

    private void Start()
    {
        // Try to find and assign the PlayerHealth component.
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            UnityEngine.Debug.LogError("PlayerHealth component not found on the same GameObject.");
        }
    }

    public void Attack()
    {
        // Find all ground-based enemies with the "GroundEnemy" tag and apply damage to them
        EnemyAI[] groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy")
            .Select(obj => obj.GetComponent<EnemyAI>())
            .ToArray();
        foreach (EnemyAI enemy in groundEnemies)
        {
            if (Vector3.Distance(playerHealth.transform.position, enemy.transform.position) <= attackRange)
            {
                int damage = enemy.damageAmount;
                enemy.TakeDamage(damage); // Apply damage to the enemy
                UnityEngine.Debug.Log("Player attacked a ground enemy. Health: " + enemy.health);
            }
        }

        // Find all flying enemies with the "FlyingEnemy" tag and apply damage to them
        FlyingEnemy[] flyingEnemies = GameObject.FindGameObjectsWithTag("FlyingEnemy")
            .Select(obj => obj.GetComponent<FlyingEnemy>())
            .ToArray();
        foreach (FlyingEnemy enemy in flyingEnemies)
        {
            if (Vector3.Distance(playerHealth.transform.position, enemy.transform.position) <= attackRange)
            {
                int damage = enemy.damageAmount;
                enemy.TakeDamage(damage); // Apply damage to the enemy
                UnityEngine.Debug.Log("Player attacked a flying enemy. Health: " + enemy.health);
            }
        }
    }
}