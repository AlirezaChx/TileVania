using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class coinBehavior : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] private int pointsForCoinPickUp = 100;
    private bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<gameSession>().AddToScore(pointsForCoinPickUp);
            AudioSource.PlayClipAtPoint(coinPickUpSFX,Camera.main.transform.position);
            Destroy(gameObject);
        } 
    }
}
