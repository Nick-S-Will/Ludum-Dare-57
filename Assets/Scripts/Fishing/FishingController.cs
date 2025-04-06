using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace LudumDare57.Fishing
{
    public class FishingController : MonoBehaviour
    {
        public UnityEvent<IList<Fish>> FishingStarted => fishingStarted;
        public UnityEvent FishingEnded => fishingEnded;
        public List<Fish> CaughtFish => caughtFish;
        public bool IsFishing { get; private set; }

        [Header("Checks")]
        [SerializeField][Min(0f)] private float range = 1f;
        [SerializeField] private LayerMask fishingMask = 1;
        [Header("Events")]
        [SerializeField] private UnityEvent<IList<Fish>> fishingStarted;
        [SerializeField] private UnityEvent fishingEnded;

        private readonly List<Fish> caughtFish = new();

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }

        public void Fish()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, fishingMask, QueryTriggerInteraction.Collide);
            List<Fish> fishOptions = new();
            foreach (Collider collider in colliders)
            {
                if (!collider.TryGetComponent<FishPool>(out var fishPool)) continue;

                fishOptions.AddRange(fishPool.Fishes);
            }

            Assert.IsTrue(fishOptions.Count > 0);

            IsFishing = true;

            fishingStarted.Invoke(fishOptions);
        }

        public void CompleteFishing(IList<Fish> caughtFish)
        {
            this.caughtFish.AddRange(caughtFish);

            foreach (Fish fish in caughtFish) Debug.Log(fish);

            IsFishing = false;

            fishingEnded.Invoke();
        }
    }
}