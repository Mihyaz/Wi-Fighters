using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour, IMove
{
    [SerializeField] private float _speed;
    public FloatingJoystick MovementJoystick;
    public FixedJoystick RotationJoystick;

    public Vector2 Move()
    {
        Vector2 direction = Vector2.up * MovementJoystick.Vertical + Vector2.right * MovementJoystick.Horizontal;
        return direction * _speed * Time.deltaTime;
    }

    public Vector2 Rotate()
    {
        Vector2 rotation = Vector2.up * RotationJoystick.Vertical + Vector2.right * RotationJoystick.Horizontal;
        return rotation * _speed;
    }
}
