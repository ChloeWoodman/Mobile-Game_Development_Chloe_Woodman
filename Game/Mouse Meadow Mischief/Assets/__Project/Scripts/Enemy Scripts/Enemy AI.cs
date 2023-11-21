using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public int health = 3;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks = 1.0f; // Reduce this value to attack more frequently
    bool canAttack = true; // Controls whether the enemy can attack
    public int damageAmount = 1; // Amount of damage inflicted on the player

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Enemy speed
    public float enemySpeed = 5.0f; // Adjust this value to increase the enemy's speed

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemySpeed;
    }

    void Update()
    {
        // is player in sight and attack range?
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.0f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // calculate random point inside of range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2.0f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Make sure the enemy does not move
        agent.SetDestination(transform.position);

        // Look at the player
        transform.LookAt(player);

        if (canAttack)
        {
            // Perform the attack here
            UnityEngine.Debug.Log("Attack");
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // the player has a PlayerHealth component

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                UnityEngine.Debug.Log("Player Damaged");
            }

            canAttack = false; // Set the cooldown
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
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
