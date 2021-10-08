using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _maxHealth = 5.0f;

    public GameHandler _gameHandler;

    void Start()
    {
        _gameHandler = GameObject.FindObjectOfType<GameHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > _maxHealth)
        {
            Die();
        }
    }

    private void Die()
    {
        _gameHandler.UpdateScore();
        Destroy(this.gameObject);
    }
}
