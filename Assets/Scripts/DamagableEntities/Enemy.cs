using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.DamagableEntities
{
    public class Enemy: MonoBehaviour, IDamageable, IShowable, IGameManageable
    {
        [SerializeField]
        private float speed = 4.0f;
        [SerializeField]
        private GameObject explosionPrefab;
        private IGameManager gameManager;
        private readonly List<SpriteRenderer> renderers = new List<SpriteRenderer>();

        private void Awake()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            renderers.Add(spriteRenderer);

            var thruster = transform.Find("ThrusterLeft").gameObject;
            renderers.Add(thruster.GetComponent<SpriteRenderer>());
            thruster = transform.Find("ThrusterRight").gameObject;
            renderers.Add(thruster.GetComponent<SpriteRenderer>());
        }


        public void FixedUpdate()
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                var playerObject = other.GetComponent<IDamageable>();
                playerObject.Damage();
                Damage();
            }
        }

        public virtual void AssignGameManager(IGameManager gm)
        {
            gameManager = gm;
        }

        public void Damage()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            gameManager.OnEnemyDamage(this);
        }

        public void Show(bool show)
        {
            foreach(var r in renderers)
            {
                r.enabled = show;
            }
        }
    }
}