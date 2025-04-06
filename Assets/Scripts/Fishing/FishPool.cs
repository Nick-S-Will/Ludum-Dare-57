using UnityEngine;

namespace LudumDare57.Fishing
{
    [RequireComponent(typeof(Collider))]
    public class FishPool : MonoBehaviour
    {
        public Fish[] Fishes => fishes;

        private Collider Collider
        {
            get
            {
                if (!Application.isPlaying || collider == null) collider = GetComponent<Collider>();
                return collider;
            }
        }

        [SerializeField] private Fish[] fishes;

        private new Collider collider;

        private void Awake()
        {
            Collider.isTrigger = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            if (Collider is BoxCollider boxCollider)
            {
                Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
            }
            else if (Collider is SphereCollider sphereCollider)
            {
                Gizmos.DrawWireSphere(transform.position + sphereCollider.center, sphereCollider.radius);
            }
        }
    }
}