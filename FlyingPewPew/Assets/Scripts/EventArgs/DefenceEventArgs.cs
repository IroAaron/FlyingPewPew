using Core;
using Ships;
using System;
using System.Collections;
using UnityEngine;

namespace CreatedEventArgs
{
    public class ShootEventArgs : EventArgs
    {
        public readonly Ship Attacker;
        public readonly int Damage;
        public readonly Vector3 ShootPos;
        public readonly Vector2 ShootDirection;
        public int VelocityProjectileMultiplier;

        public ShootEventArgs(Ship attacker, int damage, Vector3 shootPos, Vector2 shootDirection, int velocityProjectileMultiplier)
        {
            Attacker = attacker;
            Damage = damage;
            ShootPos = shootPos;
            ShootDirection = shootDirection;
            VelocityProjectileMultiplier = velocityProjectileMultiplier;
        }
    }

    public class DefenceEventArgs : EventArgs
    {
        public readonly Ship Attacker;
        public readonly IDamagable Defender;
        public readonly int Damage;

        public DefenceEventArgs(Ship attacker, IDamagable defender, int damage)
        {
            Attacker = attacker;
            Defender = defender;
            Damage = damage;
        }
    }

    public class MoveEventArgs : EventArgs
    {
        public readonly Vector2 Direction;
        public readonly float MoveSpeed;

        public MoveEventArgs(Vector2 direction, float speed)
        {
            Direction = direction;
            MoveSpeed = speed;
        }
    }
}