using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float LOSDistance;
    public LayerMask obstacleLayer;

    private GameObject playerObject;
    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (playerObject!=null)
        {
            player = playerObject.transform;
            Vector2 direction = player.position - transform.position;

            // Check for LOS
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, LOSDistance, obstacleLayer);
            bool hasLOS = hit.collider == null || hit.collider.CompareTag("Player");

            if (hasLOS && direction.magnitude <= LOSDistance)
            {
                // Move towards the player
                rb.velocity = direction.normalized * moveSpeed;
            }
            else
            {
                // No LOS or too far, stop moving
                rb.velocity = Vector2.zero;
            }
        }
        
    }
}