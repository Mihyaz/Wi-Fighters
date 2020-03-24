using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using OnurMihyaz;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IComposable
{

    #region TCP/IP Variables
    [HideInInspector]
    public string Command;
    [HideInInspector]
    public bool IsConnected;
    public string Name { get; set; }
    #endregion
    public event PlayerDelegate OnPlayerCreated;

    [Inject] private readonly Bullet _bullet;
    

    [HideInInspector] public Player Enemy;

    public Gun Gun;
    public Transform FirePoint;

    public ICommand CommandManager;
    public IAttack Attack;
    public IState State;
    public IComponent Component;
    public IStateEvent @Event;

    private PlayerUI UI;

    private void Awake()
    {
        Init();
        if (enabled)
            enabled = false;
    }
    private void Start()
    {
        OnPlayerCreated?.Invoke();
        @Event.OnPlayerDeath += () =>
        {
            UI.SetUI();
            Enemy.State.Score++;
            Enemy.UI.Score.text = Enemy.State.Score.ToString();
            GameManager.Instance.UI.RefreshKillFeed(Enemy.Name, Name);
        };
        @Event.OnPlayerRespawn += () => ResetThis();
        GameManager.Instance.OnGameFinish += () => ResetThis();
        ResetThis();
    }
    private void Update()
    {
        Shoot();
        Reload();
        Move();
        Rotate();
    }
    public void PickGunClass(GunClasses choice)
    {
        switch (choice)
        {
            case GunClasses.Rifle:
                Gun = new Rifle();
                break;
            case GunClasses.Shotgun:
                Gun = new Shotgun();
                break;
            case GunClasses.Handgun:
                Gun = new Handgun();
                break;
            case GunClasses.Laser:
                Gun = new Laser();
                break;
            default:
                Gun = new Rifle();
                break;
        }
        _bullet.Init(Gun.Damage, this);
        UI.Init(Gun.Ammo, Gun.ClipSize);
    }
    private void Rotate()
    {
        if (CommandManager.GetRotation().x == 0f && CommandManager.GetRotation().y == 0f)
        {
            // this statement allows it to recenter once the inputs are at zero 
            Vector3 currentRotation = Component.Transform.localEulerAngles; // the object you are rotating
            Vector3 homeRotation;
            if (currentRotation.z > 180f)
            { // this section determines the direction it returns home 
                homeRotation = new Vector3(0f, 0f, 359.999f); //it doesnt return to perfect zero 
            }
            else
            {                                                                      // otherwise it rotates wrong direction 
                homeRotation = Vector3.zero;
            }
            //_transform.localEulerAngles = Vector3.Slerp(currentRotation, homeRotation, Time.deltaTime);//Maybe change this
        }
        else
        {
            Component.Transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(
                CommandManager.GetRotation().x, CommandManager.GetRotation().y) * -Mathf.Rad2Deg); // this does the actual rotaion according to inputs
        }
    }

    private void Move()
    {
        Component.RigidBody.velocity = CommandManager.GetMovement();

        if (Component.RigidBody.velocity.magnitude > 0)
            Component.Animator.SetBool("isRunning", true);

        else
            Component.Animator.SetBool("isRunning", false);
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
            Component.Animator.SetBool("isShooting", true);

            Gun.NextFire = Time.time + Gun.FireRate;
            Gun.Fire(_bullet, transform, FirePoint);

            UI.CurrentAmmo = --Gun.Ammo;
        }
        if (!CommandManager.Shoot())
        {
            Component.Animator.SetBool("isShooting", false);
        }
    }

    private void Reload()
    {
        if (CommandManager.Reload() && Gun.Ammo != Gun.ClipSize)
        {
            Component.Animator.SetBool("isReloading", true);
            Attack.isReloading = true;
        }
    }
    private void ReloadWhenOutOfAmmo()
    {
        if (!Attack.isReloading && Gun.Ammo != Gun.ClipSize)
        {
            Component.Animator.SetBool("isReloading", true);
            Attack.isReloading = true;
        }
    }
    public void StopReloading()
    {
        Component.Animator.SetBool("isReloading", false);
        UI.CurrentAmmo = Gun.ResetAmmo();
        Attack.isReloading = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Enemy = collision.gameObject.GetComponent<Bullet>().Player; // The player who made shot became Enemy

            Component.SpriteRenderer.DOColor(Color.red, 0.25f).OnComplete(() =>
            {
                Component.SpriteRenderer.DOColor(Color.white, 0.15f);
            });

            Instantiate(Component.Blood, collision.gameObject.transform.position, collision.gameObject.transform.rotation.normalized);
            State.Health -= (int)Enemy.Gun.Damage;
            UI.Health = (Enemy.Gun.Damage / 100);
            Destroy(collision.gameObject);
        }
    }

    public void Init()
    {
        UI = GetComponentInChildren<PlayerUI>();
        Attack = GetComponent<IAttack>();
        State = GetComponent<IState>();
        CommandManager = GetComponent<ICommand>();
        Component = GetComponent<IComponent>();
        @Event = GetComponent<IStateEvent>();
    }

    public void ResetThis()
    {
        Gun.ResetAmmo();
        State.ResetThis();
        UI.ResetThis();
        Component.ResetThis();
    }
}
