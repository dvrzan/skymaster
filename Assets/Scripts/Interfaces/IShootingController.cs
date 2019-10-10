using System.Collections;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Interfaces
{
    public interface IShootingController
    {
        void Shoot(GameObject projectile, Vector2 position);
    }
}
