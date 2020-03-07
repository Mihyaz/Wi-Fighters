using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnurMihyaz;

public class Bullet : MonoBehaviour
{
    [SerializeField] public Transform _transform;
    [SerializeField] public Rigidbody2D Rigidbody;

    public float Damage;
    public Player Player;
    void Start()
    {
        _transform = transform;
    }

    private void OnDestroy()
    {
        //Particle effect
    }

    public void Init(float damage, Player player)
    {
        Damage = damage;
        Player = player;
    }
}
