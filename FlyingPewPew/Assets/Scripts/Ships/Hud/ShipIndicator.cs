using CreatedEventArgs;
using Ships;
using System;
using System.Collections;
using UnityEngine;

namespace Hud
{
    public class ShipIndicator : MonoBehaviour
    {
        [SerializeField] private SliderBar _hpBar;

        private Ship _ship;
        
        public void Init(Ship ship)
        {
            _ship = ship;
            _hpBar.Init(_ship.MaxHP);
            _ship.ShipDamaged += OnShipDamaged;
        }

        private void OnShipDamaged(object sender, DefenceEventArgs e)
        {
            _hpBar.SetProgress(e.Damage);
        }

        private void OnDestroy()
        {
            _ship.ShipDamaged -= OnShipDamaged;
        }
    }
}