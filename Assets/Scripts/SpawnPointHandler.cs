using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointHandler : MonoBehaviour, IComposable
{
    private Stack<Vector3> _spawnPoints = new Stack<Vector3>();
    private System.Random _random = new System.Random();

    void Start()
    {
        Init();
    }

    public Vector3 GetSpawnPoint()
    {
        if(_spawnPoints.Count == 0)
        {
            Init();
        }
        return _spawnPoints.Pop();
    }

    public void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPoints.Push(GetComponentsInChildren<Transform>()[i].position);
        }
        _spawnPoints = Shuffle(_spawnPoints);
    }

    public Stack<Vector3> Shuffle(Stack<Vector3> stack)
    {
        var values = stack.ToArray();
        stack.Clear();
        foreach (var value in values.OrderBy(x => _random.Next()))
            stack.Push(value);
        return stack;
    }

    public void ResetThis()
    {
        throw new System.NotImplementedException();
    }
}
