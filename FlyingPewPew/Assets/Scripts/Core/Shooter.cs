using CreatedEventArgs;
using Ships;
using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class Shooter
    {
        private Ship _ship;
        private Projectile _projectile;
        private int _damage;
        private int _projectileVelocityMultiplier;
        private float _shootSpeed;

        public Shooter(Ship ship, Projectile projectile, int damage, int projectileVelocityMultiplier, 
            float shootSpeed)
        {
            _ship = ship;
            _projectile = projectile;
            _damage = damage;
            _projectileVelocityMultiplier = projectileVelocityMultiplier;
            _shootSpeed = shootSpeed;

            ship.ShipFired += OnShipFired;
        }

        private void OnShipFired(object sender, ShootEventArgs e)
        {
            var projectile = Object.Instantiate(_projectile, e.ShootPos, Quaternion.identity);
            projectile.Init(e.Attacker, e.Damage, e.ShootDirection, e.VelocityProjectileMultiplier);
        }
    }
}