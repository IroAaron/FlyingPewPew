using Hud;
using Ships;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public interface IFactory
    {
        Ship CreateShip(Ship ship, Vector3 position);
        ShipIndicator CreateShipIndicator(Ship ship, Transform parent);
    }
}