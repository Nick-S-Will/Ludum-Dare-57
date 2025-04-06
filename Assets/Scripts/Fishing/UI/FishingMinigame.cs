using LudumDare57.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LudumDare57.Fishing.UI
{
    public class FishingMinigame : MonoBehaviour
    {
        private int RandomSpawnCount => Random.Range(minSpawnCount, maxSpawnCount + 1);
        private float HookDropSpeed => RectTransform.TransformVector(localHookDropSpeed * Vector3.right).x;
        private float ReelSpeed => RectTransform.TransformVector(localReelSpeed * Vector3.right).x;
        private float RandomSpawnInterval => Random.Range(minSpawnInterval, maxSpawnInterval);
        private Vector3 RandomSpawnPosition => RectTransform.position + RectTransform.TransformVector(Random.Range(minSpawnRange, maxSpawnRange) * Random.insideUnitCircle.normalized); // 
        private RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
                return rectTransform;
            }
        }

        [SerializeField] private FishingController fishingController;
        [SerializeField] private Image hook, fish, caughtFishPrefab;
        [SerializeField] private RectTransform caughtFishParent;
        [Header("Attributes")]
        [SerializeField][Min(1f)] private int minSpawnCount = 2;
        [SerializeField][Min(1f)] private int maxSpawnCount = 4;
        [SerializeField][Min(0f)] private float localHookDropSpeed = 300f, localReelSpeed = 600f, minSpawnInterval = 3f, maxSpawnInterval = 7f, minSpawnRange = 100f, maxSpawnRange = 300f, positionTolerance = .1f;

        private RectTransform rectTransform;
        private Coroutine moveHookRoutine, spawnFishRoutine;
        private Vector3 hookStartLocalPosition;
        private readonly List<Fish> caughtFish = new();
        private readonly List<Image> caughtFishIcons = new();
        private bool caught;

        private void Awake()
        {
            Assert.IsNotNull(fishingController);
            Assert.IsNotNull(hook);
            Assert.IsNotNull(fish);
            Assert.IsNotNull(caughtFishPrefab);
            Assert.IsNotNull(caughtFishParent);

            fishingController.FishingStarted.AddListener(StartMinigame);
            fishingController.FishingEnded.AddListener(EndMinigame);
            hookStartLocalPosition = hook.rectTransform.localPosition;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, RectTransform.TransformVector(minSpawnRange * Vector3.right).x);
            Gizmos.DrawWireSphere(transform.position, RectTransform.TransformVector(maxSpawnRange * Vector3.right).x);
        }

        private void StartMinigame(IList<Fish> fishOptions)
        {
            if (fishOptions.Count == 0) return;

            Show();
            fish.enabled = false;
            SpawnFish(fishOptions);
        }

        private void EndMinigame()
        {
            MoveHookToStart();
            Hide();
        }

        [ContextMenu(nameof(Show))]
        public void Show() => this.ShowGraphics();

        [ContextMenu(nameof(Hide))]
        public void Hide() => this.HideGraphics();

        #region Hook Movement
        private void MoveHookToStart(float moveSpeed = float.MaxValue) => MoveHook(RectTransform.position + hookStartLocalPosition, moveSpeed);

        private void MoveHookToCenter(float moveSpeed = float.MaxValue) => MoveHook(RectTransform.position, moveSpeed);

        private void MoveHook(Vector3 targetPosition, float moveSpeed = float.MaxValue)
        {
            Assert.IsTrue(moveSpeed >= 0f);

            if (moveHookRoutine != null) StopCoroutine(moveHookRoutine);

            moveHookRoutine = StartCoroutine(MoveHookRoutine(targetPosition, moveSpeed));
        }

        private IEnumerator MoveHookRoutine(Vector3 targetPosition, float moveSpeed)
        {
            while (Vector3.Distance(hook.rectTransform.position, targetPosition) > positionTolerance)
            {
                hook.rectTransform.position = Vector3.MoveTowards(hook.rectTransform.position, targetPosition, moveSpeed * Time.deltaTime);

                yield return null;
            }

            moveHookRoutine = null;
        }
        #endregion

        #region Spawning
        private void SpawnFish(IList<Fish> fishOptions)
        {
            if (spawnFishRoutine != null) StopCoroutine(spawnFishRoutine);

            spawnFishRoutine = StartCoroutine(SpawnFishRoutine(fishOptions));
        }

        private IEnumerator SpawnFishRoutine(IList<Fish> fishOptions)
        {
            int spawnCount = RandomSpawnCount;
            for (int i = 0; i < spawnCount; i++)
            {
                MoveHookToCenter(HookDropSpeed);
                yield return moveHookRoutine;

                yield return new WaitForSeconds(RandomSpawnInterval);

                Fish fishAsset = fishOptions[Random.Range(0, fishOptions.Count)];
                yield return StartCoroutine(SpawnFishQuickTimeRoutine(fishAsset));
            }

            fishingController.CompleteFishing(caughtFish);

            ClearCatches();
            spawnFishRoutine = null;
        }

        private IEnumerator SpawnFishQuickTimeRoutine(Fish fishAsset)
        {
            SetFish(fishAsset, RandomSpawnPosition);

            caught = false;
            float startTime = Time.time;
            yield return new WaitUntil(() => caught || Time.time >= startTime + fishAsset.CatchTime);

            if (caught) yield return StartCoroutine(CatchRoutine(fishAsset));

            SetFish(null, RectTransform.position);
        }

        private void SetFish(Fish fishAsset, Vector3 position)
        {
            fish.rectTransform.position = position;
            fish.sprite = fishAsset ? fishAsset.SilhouetteSprite : null;
            fish.enabled = fishAsset;
        }
        #endregion

        #region Catching
        public void Catch() => caught = true;

        private IEnumerator CatchRoutine(Fish fishAsset)
        {
            MoveHook(fish.rectTransform.position, ReelSpeed);
            yield return moveHookRoutine;

            fish.rectTransform.SetParent(hook.rectTransform, true);

            MoveHookToStart(ReelSpeed);
            yield return moveHookRoutine;

            fish.rectTransform.SetParent(RectTransform);

            caughtFish.Add(fishAsset);
            caughtFishIcons.Add(Instantiate(caughtFishPrefab, caughtFishParent));
        }

        private void ClearCatches()
        {
            caught = false;
            caughtFish.Clear();
            foreach (Image caughtFishIcon in caughtFishIcons) Destroy(caughtFishIcon.gameObject);
            caughtFishIcons.Clear();
        }
        #endregion
    }
}