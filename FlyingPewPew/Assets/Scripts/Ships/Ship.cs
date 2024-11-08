using Core;
using CreatedEventArgs;
using Data;
using EditorAttributes;
using Hud;
using System;
using UnityEngine;

namespace Ships
{
    public class Ship : MonoBehaviour, IDamagable, IMoverController
    {
        [Header("Data")]
        [SerializeField] protected ShipTypeData _shipTypeData;

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Transform _parent;

        [Header("Ship Stats")]
        [SerializeField] private int _currentHP;
        [SerializeField] protected float _shootSpeed;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _damage;
        [SerializeField] private int _projectileSpeedMultiplier;

        public int MaxHP => _shipTypeData.MaxHP;
        public int CurrentHP => _currentHP;
        public float ShootSpeed => _shootSpeed;
        public float MoveSpeed => _moveSpeed;
        public Projectile Projectile => _projectile;
        public int Damage => _damage;
        public int ProjectileSpeedMultiplier => _projectileSpeedMultiplier;
        public bool IsAlive => _isAlive;

        public event EventHandler<Ship> ShipDied;
        public event EventHandler<ShootEventArgs> ShipFired;
        public event EventHandler<DefenceEventArgs> ShipDamaged;
        public event EventHandler<MoveEventArgs> OnControllerMoved;
        public event EventHandler OnControllerStoped;
        public event EventHandler<IDamagable> ObjectDied;

        protected Mover _mover;
        protected Shooter _shooter;
        protected IFactory _factory;
        protected ShipIndicator _shipIndicator;
        protected bool _isAlive;

        public virtual void Start()
        {
            _isAlive= true;

            _currentHP = _shipTypeData.MaxHP;
            _shootSpeed = _shipTypeData.ShootSpeed;
            _moveSpeed = _shipTypeData.MoveSpeed;
            _projectile = _shipTypeData.Projectile;
            _damage = _shipTypeData.Damage;
            _projectileSpeedMultiplier = _shipTypeData.ProjectileSpeedMultiplier;

            _mover = new Mover(this, _rigidbody2D);
            _shooter = new Shooter(this, Projectile, Damage, ProjectileSpeedMultiplier, ShootSpeed);
            _factory = new Factory();

            _shipIndicator = _factory.CreateShipIndicator(this, _parent);
            _shipIndicator.Init(this);
        }

        public virtual void ReceiveDamage(DefenceEventArgs defenceEventArgs)
        {
            _currentHP = Mathf.Max(_currentHP - defenceEventArgs.Damage, 0);
            ShipDamaged?.Invoke(this, defenceEventArgs);

            if (_currentHP == 0)
            {
                _isAlive = false;
                ShipDied?.Invoke(this, this);
                ObjectDied?.Invoke(this, this);
                Destroy(gameObject);
                Destroy(_shipIndicator.gameObject);
            }
        }

        public virtual void OnShipMoved(MoveEventArgs moveEventArgs)
        {
            OnControllerMoved?.Invoke(this, moveEventArgs);
        }

        public virtual void OnShipStoped()
        {
            OnControllerStoped?.Invoke(this, null);
        }

        public virtual void OnShipFired(Vector2 shootPos, Vector2 direction)
        {
            ShipFired?.Invoke(this, new ShootEventArgs(this, Damage, shootPos, direction, ProjectileSpeedMultiplier));
        }
    }
}