using UnityEngine;
using AssemblyCSharp.Assets.Scripts.DamagableEntities;

namespace AssemblyCSharp.Assets.Scripts.Projectiles
{
    public abstract class FriendlyProjectile : BaseProjectile
    {
        protected override void FixedUpdate()
        {
            rbComponent.velocity = Vector2.up * speed;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "EnemyShip")
            {
                var enemy = other.GetComponent<Enemy>();
                OnEnemyHit(enemy);
                Destroy(this.gameObject);
            }
        }

        protected abstract void OnEnemyHit(Enemy enemy);
    }
}
