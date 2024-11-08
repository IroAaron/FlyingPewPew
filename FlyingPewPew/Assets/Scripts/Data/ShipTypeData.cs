using Core;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ShipTypeData", menuName = "Data/ShipTypeData/BaseShip")]
    public class ShipTypeData : ScriptableObject
    {
        [SerializeField] private int _maxHp;
        [SerializeField] private float _shootSpeed;
        [SerializeField] private float _speed;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _damage;
        [SerializeField] private int _projectileSpeedMultiplier;

        public int MaxHP => _maxHp;
        public float ShootSpeed => _shootSpeed;
        public float MoveSpeed => _speed;
        public Projectile Projectile => _projectile;
        public int Damage => _damage;
        public int ProjectileSpeedMultiplier => _projectileSpeedMultiplier;
    }
}