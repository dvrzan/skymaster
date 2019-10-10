using System.Collections;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.DamagableEntities;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.PowerUps
{
    public class PowerUp: MonoBehaviour, IGameManageable, IShowable
    {
        public int duration;

        protected bool collected;
        protected SpriteRenderer spriteRenderer;
        protected Player playerObject { get; private set; }
        protected IGameManager gameManager;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public virtual void AssignGameManager(IGameManager gm)
        {
            gameManager = gm;
        }

        protected virtual void StartEffect()
        {
            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
                audioSource.Play();

            // hide sprite until the GameObject is destroyed by EndEffect
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(TickDuration());
        }

        protected virtual void EndEffect()
        {
            playerObject.powerUps.Remove(GetType().Name);
            Destroy(this.gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                collected = true;
                playerObject = other.GetComponent<Player>();
                var typeName = GetType().Name;
                if(!playerObject.powerUps.Contains(typeName))
                {
                    playerObject.powerUps.Add(typeName);
                    StartEffect();
                }

            }
        }

        private IEnumerator TickDuration()
        {
            while (duration > 0)
            {
                duration--;
                yield return new WaitForSeconds(1.0f);
            }

            StopCoroutine(TickDuration());
            EndEffect();
        }

        public virtual void Show(bool show)
        {
            // prevent power up getting shown after unpausing if it is collected
            show = show && !collected;
            spriteRenderer.enabled = show;
        }
    }
}
