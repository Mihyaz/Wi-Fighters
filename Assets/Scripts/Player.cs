using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using OnurMihyaz;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    #region TCP/IP Variables
    [HideInInspector]
    public string Command;
    [HideInInspector]
    public bool IsConnected;
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
        }
    }

    #endregion

    [Inject]
    private readonly Bullet _bullet;
    [Inject]
    private readonly SpawnPointHandler _spawnPointHandler;
    [HideInInspector]
    public Player Enemy;

    public Gun Gun;
    public Transform FirePoint;

    public ICommand CommandManager;
    public IAttack Attack;
    public IState State;
    public IEvent @Event;

    private GameObject _blood;
    private Rigidbody2D _rigidBody;
    private Transform _transform;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerUI UI;
    [SerializeField] private int _choice;
    private void Awake()
    {
        _blood = Resources.Load("Prefabs/BloodParticle") as GameObject;
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UI = GetComponentInChildren<PlayerUI>();
        Attack = GetComponent<IAttack>();
        State = GetComponent<IState>();
        @Event = GetComponent<IEvent>();
        CommandManager = GetComponent<ICommand>();
    }
    private void Start()
    {
        Gun = PickGunClass(_choice);
        @Event.OnPlayerRespawn += () =>
        {
            Gun.ResetAmmo();
            State.ResetHealth();
            UI.ResetThis();
            _transform.position = _spawnPointHandler.GetSpawnPoint();
        };
        @Event.OnPlayerDeath += () =>
        {
            UI.SetUI();
            Enemy.State.Score++;
            Enemy.UI.ScoreText.text = Enemy.State.Score.ToString();
            GameManager.Instance.UI.RefreshKillFeed(Enemy.Name, Name);
        };
        GameManager.Instance.OnGameFinish += () => ResetPlayer();
        _transform.position = _spawnPointHandler.GetSpawnPoint();

    }
    private void Update()
    {
        Shoot();
        Reload();
        Move();
        Rotate();
    }

    private void Rotate()
    {
        if (CommandManager.GetRotation().x == 0f && CommandManager.GetRotation().y == 0f)
        {
            // this statement allows it to recenter once the inputs are at zero 
            Vector3 currentRotation = _transform.localEulerAngles; // the object you are rotating
            Vector3 homeRotation;
            if (currentRotation.z > 180f)
            { // this section determines the direction it returns home 
                homeRotation = new Vector3(0f, 0f, 359.999f); //it doesnt return to perfect zero 
            }
            else
            {                                                                      // otherwise it rotates wrong direction 
                homeRotation = Vector3.zero;
            }
            _transform.localEulerAngles = Vector3.Slerp(currentRotation, homeRotation, Time.deltaTime * -2);
        }
        else
        {
            _transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(
                CommandManager.GetRotation().x, CommandManager.GetRotation().y) * -Mathf.Rad2Deg); // this does the actual rotaion according to inputs
        }
    }

    private void Move()
    {
        _rigidBody.velocity = CommandManager.GetMovement();

        if (_rigidBody.velocity.magnitude > 0)
            _animator.SetBool("isRunning", true);

        else
            _animator.SetBool("isRunning", false);
    }

    private void Shoot()
    {

        if (Gun.Ammo <= 0)
        {
            ReloadWhenOutOfAmmo();
            return;
        }

        if (CommandManager.Shoot() && !Attack.isReloading && Time.time > Gun.NextFire)
        {
            Gun.NextFire = Time.time + Gun.FireRate;

            UI.Ammo = --Gun.Ammo;
            _animator.SetBool("isShooting", true);

            for (int i = 0; i < Gun.SpreadCount; i++)
            {
                Bullet bullet = Instantiate(_bullet, FirePoint.position, FirePoint.rotation);
                bullet.Rigidbody.velocity = Gun.Fire(bullet, transform);
            }

        }
        if (!CommandManager.Shoot())
        {
            _animator.SetBool("isShooting", false);
        }
    }

    private void Reload()
    {
        if (CommandManager.Reload() && Gun.Ammo != Gun.ClipSize)
        {
            _animator.SetBool("isReloading", true);
            Attack.isReloading = true;
        }
    }
    private void ReloadWhenOutOfAmmo()
    {
        if (!Attack.isReloading && Gun.Ammo != Gun.ClipSize)
        {
            _animator.SetBool("isReloading", true);
            Attack.isReloading = true;
        }
    }
    private void StopReloading()
    {
        _animator.SetBool("isReloading", false);
        UI.Ammo = Gun.ResetAmmo();
        Attack.isReloading = false;
    }

    private Gun PickGunClass(int choice)
    {
        Gun Gun;

        switch (choice)
        {
            case 0:
                Gun = new Rifle();
                break;
            case 1:
                Gun = new Handgun();
                break;
            case 2:
                Gun = new Shotgun();
                break;
            default:
                Gun = new Rifle();
                break;
        }

        UI.Init(Gun.Ammo, Gun.ClipSize);
        _bullet.Init(Gun.Damage, this);
        return Gun;
    }

    public void ResetPlayer()
    {
        Gun.ResetAmmo();
        State.ResetThis();
        UI.ResetThis();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Enemy = collision.gameObject.GetComponent<Bullet>().Player; // The player who made shot became Enemy

            _spriteRenderer.DOColor(Color.red, 0.25f).OnComplete(() =>
            {
                _spriteRenderer.DOColor(Color.white, 0.15f);
            });

            Instantiate(_blood, collision.gameObject.transform.position, collision.gameObject.transform.rotation.normalized);
            State.Health -= (int)Enemy.Gun.Damage;
            UI.Health = (Enemy.Gun.Damage / 100);
            Destroy(collision.gameObject);
        }
    }
}
