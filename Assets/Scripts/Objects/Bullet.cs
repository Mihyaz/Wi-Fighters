using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform;
    public Rigidbody2D Rigidbody;

    public float Damage;
    public Player Player;
    public SpriteRenderer SpriteRenderer;
    public TrailRenderer Trail;
    void Awake()
    {
        _transform = transform;
    }

    public void Init(float damage, Player player)
    {
        Damage = damage;
        Player = player;

        if (Player.Gun is Laser)
        {
            Trail.startColor = Color.red;
            Trail.endColor   = Color.red;
        }
    }
}
