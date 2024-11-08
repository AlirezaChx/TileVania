using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Idle,
    Chase
}

public class SmartEnemy : MonoBehaviour
{
    public EnemyState currentState;
    public float chaseSpeed;
    public float detectionRange;

    [SerializeField] private Transform player;

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer <= detectionRange && HasLineOfSight())
                {
                    currentState = EnemyState.Chase;
                }
                break;

            case EnemyState.Chase:
                if (!HasLineOfSight())
                {
                    currentState = EnemyState.Idle;
                }
                else
                {
                    ChasePlayer();
                }
                break;
        }
    }

    bool HasLineOfSight()
    {
        if (player == null)
            return false;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange);
        return hit.collider == null || hit.collider.gameObject.CompareTag("Player");
    }

    void ChasePlayer()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * chaseSpeed;
    }
}