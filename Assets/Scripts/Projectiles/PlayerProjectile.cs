using AssemblyCSharp.Assets.Scripts.DamagableEntities;

namespace AssemblyCSharp.Assets.Scripts.Projectiles
{
    public class PlayerProjectile: FriendlyProjectile
    {
        protected override void OnEnemyHit(Enemy enemy)
        {
            enemy.Damage();
        }
    }
}
