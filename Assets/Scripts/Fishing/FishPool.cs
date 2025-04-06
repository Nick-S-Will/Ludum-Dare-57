using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Fishing
{
    [RequireComponent(typeof(Collider))]
    public class FishPool : MonoBehaviour
    {
        public Fish[] Fishes => fishes;

        private Collider[] Colliders
        {
            get
            {
                if (!Application.isPlaying || colliders == null) colliders = GetComponents<Collider>();
                return colliders;
            }
        }

        [SerializeField] private Fish[] fishes;

        private Collider[] colliders;

        private void Awake()
        {
            Assert.IsTrue(fishes.All(fish => fish.IsValid()));

            foreach(Collider collider in Colliders) collider.isTrigger = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            foreach (Collider collider in Colliders)
            {
                if (collider is BoxCollider boxCollider)
                {
                    Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
                }
                else if (collider is SphereCollider sphereCollider)
                {
                    Gizmos.DrawWireSphere(transform.position + sphereCollider.center, sphereCollider.radius);
                }
            }
        }
    }
}