using UnityEngine;
using AssemblyCSharp.Assets.Scripts.DamagableEntities;

namespace AssemblyCSharp.Assets.Scripts.Projectiles
{
    public abstract class HostileProjectile : BaseProjectile
    {
        protected override void FixedUpdate()
        {
            rbComponent.velocity = Vector2.up * speed;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                var playerObject = other.GetComponent<Player>();
                OnPlayerHit(playerObject);
            }
        }

        protected abstract void OnPlayerHit(Player player);
    }
}
