using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform;
    public Rigidbody2D Rigidbody;

    public float Damage;
    public Player Player;
    void Awake()
    {
        _transform = transform;
    }

    public void Init(float damage, Player player)
    {
        Damage = damage;
        Player = player;
    }
}
