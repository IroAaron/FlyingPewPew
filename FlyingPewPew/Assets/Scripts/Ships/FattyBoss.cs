using Core;
using CreatedEventArgs;
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Ships
{
    public class FattyBoss : Ship
    {
        [Header("Boss Data")]
        [SerializeField] private FattyShipTypeData _bossTypeData;

        [Header("Boss Stats")]
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Ship _spawnShip;
        [SerializeField] private int _spawnShipChance;
        [SerializeField] private float _rotationSpeedUpgrade;
        [SerializeField] private float _shootSpeedUpgrade;
        [SerializeField] private float _immortalTime;
        [Header("Guns Transforms")]
        [SerializeField] private List<Transform> GunsTransforms;

        public float RotationSpeed => _rotationSpeed;
        public Ship SpawnShip => _spawnShip;
        public int SpawnShipChance => _spawnShipChance;
        public float RotationSpeedUpgrade => _rotationSpeedUpgrade;
        public float ShootSpeedUpgrade => _shootSpeedUpgrade;
        public float ImmortalTime => _immortalTime;

        private PlayerShip _playerShip;
        [SerializeField] private bool _isImmortal;

        public override void Start()
        {
            base.Start();

            _rotationSpeed = _bossTypeData.RotationSpeed;
            _spawnShip = _bossTypeData.SpawnShip;
            _spawnShipChance = _bossTypeData.BaseSpawnShipChance;
            _rotationSpeedUpgrade = _bossTypeData.RotationSpeedUpgrade;
            _shootSpeedUpgrade = _bossTypeData.ShootSpeedUpgrade;
            _immortalTime = _bossTypeData.ImmortalTime;

            _playerShip = FindFirstObjectByType<PlayerShip>();

            StartCoroutine(Shoot());
        }

        public void Update()
        {
            transform.RotateAround(transform.position, Vector3.forward, RotationSpeed * Time.deltaTime);
        }

        private void SpawnKamikadze()
        {
            int randomChance = UnityEngine.Random.Range(0, 100);

            if(randomChance <= SpawnShipChance)
            {
                var direction = _playerShip.transform.position - transform.position;
                direction.Normalize();
                var spawnPosition = (Vector2)transform.position + (Vector2)direction * 2;

                var ship = _factory.CreateShip(SpawnShip, spawnPosition);

                if(ship.TryGetComponent(out Kamikaze kamikazeShip))
                {
                    kamikazeShip.Init(_playerShip);
                }
            }
        }

        public override void ReceiveDamage(DefenceEventArgs defenceEventArgs)
        {
            if (_isImmortal)
                return;

            _rotationSpeed = _rotationSpeed + (1 - CurrentHP / _shipTypeData.MaxHP) * RotationSpeedUpgrade;
            _shootSpeed = _shootSpeed - (1 - CurrentHP / _shipTypeData.MaxHP) * (ShootSpeedUpgrade / 100f);

            if (CurrentHP * 100 / _shipTypeData.MaxHP  >= 25 && (CurrentHP - defenceEventArgs.Damage) * 100 / _shipTypeData.MaxHP < 25)
                _spawnShipChance *= 2;

            if (CurrentHP * 100 / _shipTypeData.MaxHP >= 6 && (CurrentHP - defenceEventArgs.Damage) * 100 / _shipTypeData.MaxHP < 6)
                StartCoroutine(ImmortalState());

            base.ReceiveDamage(defenceEventArgs);
        }

        private IEnumerator Shoot()
        {
            while(gameObject.activeSelf == true)
            {
                yield return new WaitForSeconds(ShootSpeed);

                foreach (var gunTransform in GunsTransforms)
                {
                    var direction = new Vector2(gunTransform.position.x - transform.position.x,
                        gunTransform.position.y - transform.position.y);
                    OnShipFired(gunTransform.position, direction);
                }

                if (CurrentHP * 100 / _shipTypeData.MaxHP < 50)
                {
                    SpawnKamikadze();
                }

            }
        }

        private IEnumerator ImmortalState()
        {
            _isImmortal = true;
            yield return new WaitForSeconds(ImmortalTime);
            _isImmortal = false;
        }

        private void OnDrawGizmos()
        {
            var playerShip = _playerShip;

            if (_playerShip == null)
            {
                playerShip = FindFirstObjectByType<PlayerShip>();
            }

            Gizmos.color = Color.green;

            var direction = playerShip.transform.position - transform.position;
            direction.Normalize();

            Gizmos.DrawWireSphere((Vector2)transform.position + (Vector2)direction * 2, 1);
        }
    }
}