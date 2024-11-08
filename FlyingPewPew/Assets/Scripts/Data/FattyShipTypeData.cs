using Ships;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "FattyShipTypeData", menuName = "Data/ShipTypeData/BossShips/FattyShipTypeData")]
    public class FattyShipTypeData : ScriptableObject
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Ship _spawnShip;
        [SerializeField] private int _baseSpawnShipChance;
        [SerializeField] private float _rotationSpeedUpgrade;
        [SerializeField] private float _shootSpeedUpgrade;
        [SerializeField] private float _immortalTime;

        public float RotationSpeed => _rotationSpeed;
        public Ship SpawnShip => _spawnShip;
        public int BaseSpawnShipChance => _baseSpawnShipChance;
        public float RotationSpeedUpgrade => _rotationSpeedUpgrade;
        public float ShootSpeedUpgrade => _shootSpeedUpgrade;
        public float ImmortalTime => _immortalTime;
    }
}