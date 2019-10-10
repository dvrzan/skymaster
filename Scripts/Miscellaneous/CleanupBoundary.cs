using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Cleanup
{
    public class CleanupBoundary : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}
