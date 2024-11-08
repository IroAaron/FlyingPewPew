using AssetManagment;
using Hud;
using Ships;
using System.Data;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class Factory : IFactory
    {
        public Ship CreateShip(Ship ship, Vector3 position)
        {
            return Object.Instantiate(ship, position, Quaternion.identity);
        }

        public ShipIndicator CreateShipIndicator(Ship ship, Transform parent)
        {
            if (parent == null)
                parent = ship.transform;

            var shipIndicator = Object.Instantiate(Resources.Load(AssetPath.ShipIndicatorPrefab), parent)
                .GetComponent<ShipIndicator>();

            return shipIndicator;
        }
    }
}