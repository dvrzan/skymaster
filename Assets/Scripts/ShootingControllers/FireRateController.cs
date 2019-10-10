using System;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;
using AssemblyCSharp.Assets.Scripts.Projectiles;

namespace AssemblyCSharp.Assets.Scripts.ShootingControllers
{
    public abstract class FireRateController : IShootingController
    {
        private readonly float fireRate;
        private long lastShot;

        protected FireRateController(float fireRate)
        {
            this.fireRate = fireRate;
            lastShot = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public void Shoot(GameObject projectile, Vector2 position)
        {
        }

        protected abstract void ShootAtRate(GameObject projectile, Vector2 position);
    }
}
