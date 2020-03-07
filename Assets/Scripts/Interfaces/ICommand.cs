using UnityEngine;

public interface ICommand
{
    void Executer(string clientMessage);
    Vector2 GetMovement();
    Vector2 GetRotation();
    bool Shoot();
    bool Reload();
    
}
