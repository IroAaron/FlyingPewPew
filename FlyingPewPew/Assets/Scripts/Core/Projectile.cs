using CreatedEventArgs;
using Ships;
using System;
using UnityEngine;

namespace Core
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _lifeTime;
        
        private Ship _shooterShip;
        private int _damage;

        public void Init(Ship shooterShip, int damage, Vector2 shootDirection, int velocityProjectileMultiplier)
        {
            _shooterShip = shooterShip;
            _damage = damage;
            _shooterShip.ShipDied += OnShooterDied;

            _rigidbody.AddForce(shootDirection * velocityProjectileMultiplier * _startSpeed, ForceMode2D.Impulse);
            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                if (_shooterShip as IDamagable != damagable && collision.gameObject.tag != _shooterShip.gameObject.tag)
                {
                    damagable.ReceiveDamage(new DefenceEventArgs(_shooterShip, damagable, _damage));
                    Destroy(gameObject);
                }
            }
        }

        private void OnShooterDied(object sender, Ship e)
        {
            e.ShipDied -= OnShooterDied;
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if(_shooterShip != null && _shooterShip.IsAlive)
                _shooterShip.ShipDied -= OnShooterDied;
        }
    }
}