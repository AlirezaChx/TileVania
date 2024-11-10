using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    private Rigidbody2D bulletRb;
    [SerializeField] private float bulletSpeed = 20f;
    private playerMovement _playerMovement;
    private float xSpeed;
    private float bulletRotation;
    [SerializeField] BossHp bossHp;
    void Start()
    {
        _playerMovement = FindObjectOfType<playerMovement>();
        bulletRb = GetComponent<Rigidbody2D>();
        xSpeed = _playerMovement.transform.localScale.x*bulletSpeed;
    }
    void Update()
    {
        bulletRb.velocity = new Vector2(xSpeed, 0f);
        BulletFlip();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "Boss")
        {
            bossHp = other.GetComponent<BossHp>();
            if (bossHp != null)
            {
                bossHp.TakeDamage(10);
            }
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void BulletFlip()
    {
        transform.localScale = new Vector2(Mathf.Sign(bulletRb.velocity.x), 1f);
    }
}
