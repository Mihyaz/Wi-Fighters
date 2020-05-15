using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnurMihyaz;

public class StateSystem : MonoBehaviour, IState, IStateEvent
{
    public PlayerDelegate PlayerDelegate { get; set; }
    public CreateTriggered Create { get; set; }
    public event PlayerDelegate OnPlayerDeath;
    public event PlayerDelegate OnPlayerRespawn;

    public int Score { get; set; }
    public int Death { get; set; }

    private int _health;
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
            {
                ResetHealth();
                OnPlayerDeath.Invoke();
            }
        }
    }


    private void Awake()
    {
        Init();
    }

    public void ResetHealth()
    {
        Health = 100;
    }

    public void Die()
    {
        Death++;
        Dissolve();
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(MihyazDelay.Delay(3, () => OnPlayerRespawn.Invoke()));
    }

    public void Respawn()
    {
        ResetHealth();
    }

    public void Kill()
    {
        Score++;
    }

    public void Dissolve()
    {
        float fade = 1f;
        var renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(MihyazDelay.WaitUntilThis(() =>
        {
            fade -= Time.deltaTime;
            if(fade <= 0f)
            {
                renderer.enabled = false;
                renderer.material.SetFloat("_Fade", 1);
                return true;
            }
            renderer.material.SetFloat("_Fade", fade);
            return false;
        }));
    }

    public void Init()
    {
        Health = 100;
        OnPlayerDeath += Die;
        OnPlayerRespawn += Respawn;
    }

    public void ResetThis()
    {
        Health = 100;
    }
}
