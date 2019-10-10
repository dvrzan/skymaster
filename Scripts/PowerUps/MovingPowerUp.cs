using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.PowerUps
{
    public class MovingPowerUp: PowerUp
    {
        public float speed;

        private Rigidbody2D rigidbodycomponent;

        protected override void StartEffect()
        {
            // prevent object from getting cleaned up by boundary if it is picked up
            speed = 0;
            base.StartEffect();
        }

        public void Awake()
        {
            rigidbodycomponent = GetComponent<Rigidbody2D>();
        }

        public virtual void FixedUpdate()
        {
            rigidbodycomponent.velocity = Vector2.down * speed;
        }
    }
}
