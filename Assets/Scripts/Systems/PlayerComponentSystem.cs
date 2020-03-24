﻿using UnityEngine;
using Zenject;

public class PlayerComponentSystem : MonoBehaviour, IComponent
{
    [Inject] private readonly SpawnPointHandler _spawnPointHandler;
    public GameObject Blood { get; set; }
    public Rigidbody2D RigidBody { get; set; }
    public Transform Transform { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public Animator Animator { get; set; }
    public CircleCollider2D Collider { get; set; }

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        Blood = Resources.Load("Prefabs/BloodParticle") as GameObject;
        Animator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        Transform = GetComponent<Transform>();
        Collider = GetComponent<CircleCollider2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ResetThis()
    {
        SpriteRenderer.enabled = true;
        Collider.enabled = true;
        Transform.position = _spawnPointHandler.GetSpawnPoint();
    }
}
