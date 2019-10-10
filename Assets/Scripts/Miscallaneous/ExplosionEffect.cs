using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.Miscallenous
{
    public class ExplosionEffect : MonoBehaviour, IShowable
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Start()
        {
            // destroy when the effect ends
            Destroy(gameObject, 4);
        }

        public void Show(bool show)
        {
            spriteRenderer.enabled = show;
        }
    }
}
