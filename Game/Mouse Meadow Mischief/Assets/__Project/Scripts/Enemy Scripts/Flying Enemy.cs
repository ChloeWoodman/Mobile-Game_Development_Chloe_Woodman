using System;
using System.Diagnostics;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Speed of enemy movement
    public int damageAmount = 1;  // Amount of damage inflicted on the player
    public float timeBetweenAttacks = 1.0f; // Reduce this value to attack more frequently
    bool canAttack = true; // Controls whether the enemy can attack

    public int health = 3;

    private Transform player;       // Reference to the player's transform
    private Vector3 wanderTarget;   // Current target position for wandering
    private bool isWandering = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        SetNewWanderTarget();
    }

    void Update()
    {
        if (isWandering)
        {
            // Move towards the current wander target
            transform.Translate((wanderTarget - transform.position).normalized * moveSpeed * Time.deltaTime);

            // If the enemy is close to the current target position, set a new target
            if (Vector3.Distance(transform.position, wanderTarget) < 0.2f)
            {
                SetNewWanderTarget();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (canAttack)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                UnityEngine.Debug.Log("Collision");
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    UnityEngine.Debug.Log("Collision");

                    // Destroy the enemy object after damaging the player
                    DestroyEnemy();
                }
            }
        }

        canAttack = false; // Set the cooldown
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    void SetNewWanderTarget()
    {
        // Generate a random point within a specified range
        float wanderRadius = 10.0f;
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized * wanderRadius;
        wanderTarget = new Vector3(randomDirection.x, 0, randomDirection.y) + transform.position;

        isWandering = true;
    }

    private void ResetAttack()
    {
        canAttack = true; // Reset the cooldown, allowing the enemy to attack again
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UnityEngine.Debug.Log("Enemy took damage. Remaining health: " + health);

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
