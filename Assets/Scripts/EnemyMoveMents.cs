using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveMents : MonoBehaviour
{
    Rigidbody2D enemyRb;
    private BoxCollider2D enemyDirectionSetter;
    [SerializeField] private float moveSpeed;
    
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyDirectionSetter = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        enemyRb.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRb.velocity.x)), 1f);
    }
}
