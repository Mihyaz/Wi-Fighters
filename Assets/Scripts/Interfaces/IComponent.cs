using UnityEngine;
public interface IComponent : IComposable
{
    Rigidbody2D RigidBody { get; set; }
    Transform Transform { get; set; }
    SpriteRenderer SpriteRenderer { get; set; }
    Animator Animator { get; set; }
    CircleCollider2D Collider { get; set; }
}