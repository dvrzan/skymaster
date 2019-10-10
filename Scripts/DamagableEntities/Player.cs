using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;
using AssemblyCSharp.Assets.Scripts.ShootingControllers;

namespace AssemblyCSharp.Assets.Scripts.DamagableEntities
{
    public class Player: MonoBehaviour, IDamageable, IShowable, IGameManageable
    {
        public float fireRate;
        public bool invulnearable;
        public float speed;
        public readonly HashSet<string> powerUps = new HashSet<string>();
        public GameObject projectile;
        public GameObject explosion;

        private float lastDamage;
        [SerializeField]
        private int life;
        [SerializeField]
        private List<GameObject> enginesFailures;
        private readonly List<SpriteRenderer> renderers = new List<SpriteRenderer>();
        private Animator animatorComponent;
        private Rigidbody2D rbComponent;
        private IGameManager gameManager;

        public IShootingController ShootingController { get; set; }

        public void Awake()
        {
            animatorComponent = GetComponent<Animator>();
            rbComponent = GetComponent<Rigidbody2D>();

            enginesFailures.Add(transform.Find("LeftEngineFailure").gameObject);
            enginesFailures.Add(transform.Find("RightEngineFailure").gameObject);

            var spriteRenderer = GetComponent<SpriteRenderer>();
            renderers.Add(spriteRenderer);

            var thruster = transform.Find("Thruster").gameObject;
            renderers.Add(thruster.GetComponent<SpriteRenderer>());

            foreach(var engine in enginesFailures)
            {
                renderers.Add(engine.GetComponent<SpriteRenderer>());
            }

            var shield = transform.Find("Shield").gameObject;
            renderers.Add(shield.GetComponent<SpriteRenderer>());

            ShootingController = new DefaultShootingController();
        }

        public void Update()
        {
            Shoot();
            Move();
        }

        public virtual void AssignGameManager(IGameManager gm)
        {
            gameManager = gm;
        }

        public void Damage()
        {
            if (invulnearable == false && (Time.time) > lastDamage + 1.0f)
            {
                life--;
                if (life > 0)
                    enginesFailures[life - 1].SetActive(true);

                Instantiate(explosion, transform.position, Quaternion.identity);

                lastDamage = Time.time;
                gameManager.OnPlayerDamage(this);
            }
        }

        private void Shoot()
        {
            if (InputController.GetButtonPressed(gameObject.GetInstanceID(), "Fire"))
            {
                ShootingController.Shoot(projectile, transform.position);
            }
        }

        private void Move()
        {
            var direction = new Vector2(
                InputController.GetAxis(gameObject.GetInstanceID(), "Horizontal"),
                InputController.GetAxis(gameObject.GetInstanceID(), "Vertical")
            );
                
            rbComponent.velocity = direction * speed;

            Animate(direction);
        }

        private void Animate(Vector2 direction)
        {
            if (direction.x == 1.0f)
            {
                animatorComponent.SetBool("Turn_Right", true);
                animatorComponent.SetBool("Turn_Left", false);
            }
            else if (direction.x == -1.0f)
            {
                animatorComponent.SetBool("Turn_Right", false);
                animatorComponent.SetBool("Turn_Left", true);
            }
            else
            {
                animatorComponent.SetBool("Turn_Right", false);
                animatorComponent.SetBool("Turn_Left", false);
            }
        }

        public void Show(bool show)
        {
            foreach (var r in renderers)
            {
                r.enabled = show;
            }
        }
    }
}