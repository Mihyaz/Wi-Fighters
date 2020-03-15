using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointHandler : MonoBehaviour, IComposable
{
    public Stack<Vector3> SpawnPoints = new Stack<Vector3>();
    private System.Random _random = new System.Random();

    void Start()
    {
        Init();
    }

    public Vector3 GetSpawnPoint()
    {
        if(SpawnPoints.Count == 0)
        {
            Init();
        }
        return SpawnPoints.Pop();
    }

    public void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            SpawnPoints.Push(GetComponentsInChildren<Transform>()[i].position);
        }
        SpawnPoints = Shuffle(SpawnPoints);
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
