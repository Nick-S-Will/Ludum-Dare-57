using System.Collections.Generic;
using UnityEngine;

namespace LudumDare57.Fishing
{
    public class FishingController : MonoBehaviour
    {
        [Header("Checks")]
        [SerializeField][Min(0f)] private float range = 1f;
        [SerializeField] private LayerMask fishingMask = 1;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }

        public void Fish()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, fishingMask, QueryTriggerInteraction.Collide);
            List<Fish> fishes = new();
            foreach (Collider collider in colliders)
            {
                FishPool fishPool = collider.GetComponent<FishPool>();
                if (fishPool == null) continue;

                fishes.AddRange(fishPool.Fishes);
            }

            if (fishes.Count > 0)
            {
                foreach (Fish fish in fishes) Debug.Log(fish); // TODO: Replace this fish pool testing with the fishing minigame
            }
            else Debug.Log("No fish");
        }
    }
}