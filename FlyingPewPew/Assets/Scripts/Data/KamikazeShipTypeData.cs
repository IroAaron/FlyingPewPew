using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "KamikazeShipTypeData", menuName = "Data/ShipTypeData/EnemyShips/KamikazeShipTypeData")]
    public class KamikazeShipTypeData : ScriptableObject
    {
        [SerializeField] private float _explodeRadius;

        public float ExplodeRadius => _explodeRadius;
    }
}