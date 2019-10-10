using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.Projectiles
{
    public abstract class BaseProjectile: MonoBehaviour, IShowable
    {
        public float speed;

        protected SpriteRenderer spriteRenderer;
        protected Rigidbody2D rbComponent;

        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rbComponent = GetComponent<Rigidbody2D>();
        }

        protected abstract void FixedUpdate();
        protected abstract void OnTriggerEnter2D(Collider2D other);

        public virtual void Show(bool show)
        {
            spriteRenderer.enabled = false;
        }
    }
}
