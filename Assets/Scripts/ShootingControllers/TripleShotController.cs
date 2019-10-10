using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.ShootingControllers
{
    public class TripleShotController: IShootingController
    {
        public void Shoot(GameObject projectile, Vector2 position)
        {
            //Middle laser
            Object.Instantiate(projectile, position + new Vector2(0, 0.97f), Quaternion.identity);
            //Left laser
            Object.Instantiate(projectile, position + new Vector2(-0.55f, 0.04f), Quaternion.identity);
            //Right laser
            Object.Instantiate(projectile, position + new Vector2(0.55f, 0.04f), Quaternion.identity);
        }
    }
}
