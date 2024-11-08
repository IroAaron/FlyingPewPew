using Core;
using CreatedEventArgs;
using Data;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Ships
{
    public class Kamikaze : Ship
    {
        [Header("Enemy Data")]
        [SerializeField] private KamikazeShipTypeData _enemyTypeData;

        [Header("Enemy Stats")]
        [SerializeField] private float _explodeRadius;
        [SerializeField] private CircleCollider2D _collider;

        public float ExplodeRadius => _explodeRadius;

        private Ship _target;
        private List<IDamagable> _damagables = new List<IDamagable>();
        private bool _isTargetInRange
        {
            get
            {
                if (_target == null)
                {
                    return false;
                }
                else
                {
                    if(Vector3.Distance(_target.transform.position, transform.position) <= ExplodeRadius)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override void Start()
        {
            _explodeRadius = _enemyTypeData.ExplodeRadius;
            _collider.radius = ExplodeRadius;

            base.Start();
        }

        public void Init(PlayerShip target)
        {
            _target = target;
        }

        private void Update()
        {
            if (!_isTargetInRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, MoveSpeed * Time.deltaTime);
            }
        }

        private void Explode()
        {
            foreach (var damagable in _damagables)
            {
                damagable.ReceiveDamage(new DefenceEventArgs(this, damagable, Damage));
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                if (!_damagables.Contains(damagable))
                {
                    damagable.ObjectDied += DamagableDied;
                    _damagables.Add(damagable);
                }

                if (damagable == _target as IDamagable)
                {
                    Explode();
                }
            }
        }

        private void DamagableDied(object sender, IDamagable e)
        {
            e.ObjectDied -= DamagableDied;
            _damagables.Remove(e);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                if (_damagables.Contains(damagable))
                {
                    damagable.ObjectDied -= DamagableDied;
                    _damagables.Remove(damagable);
                }
            }
        }

        private void OnDestroy()
        {
            _damagables.RemoveAll(d => d == null);
            foreach (var damagable in _damagables)
            {
                damagable.ObjectDied -= DamagableDied;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, ExplodeRadius);
        }
    }
}