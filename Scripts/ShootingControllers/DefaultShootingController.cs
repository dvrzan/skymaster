using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.ShootingControllers
{
    public class DefaultShootingController: IShootingController
    {
        public void Shoot(GameObject projectile, Vector2 position)
        {
            var projectilePosition = position + new Vector2(0, 0.97f);
            Object.Instantiate(projectile, projectilePosition, Quaternion.identity);
        }
    }
}
