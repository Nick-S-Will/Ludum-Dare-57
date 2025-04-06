using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Terrain
{
    [RequireComponent(typeof(MeshRenderer))]
    public class WaterAnimation : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField][Min(1e-5f)] private float frameInterval = 1f, panInterval = .2f;

        private MeshRenderer meshRenderer;
        private float lastFrameTime, lastPanTime;
        private int frameIndex;

        private void Awake()
        {
            Assert.IsTrue(sprites != null && sprites.Length > 0);

            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (Time.time >= lastFrameTime + frameInterval)
            {
                lastFrameTime = Time.time;
                frameIndex = (frameIndex + 1) % sprites.Length;

                meshRenderer.material.mainTexture = sprites[frameIndex].texture;
            }

            if (Time.time >= lastPanTime + panInterval)
            {
                lastPanTime = Time.time;

                Vector2 spriteSize = sprites[frameIndex].rect.size;
                Vector2 pan = new(1f / spriteSize.x, 1f / spriteSize.y);
                meshRenderer.material.mainTextureOffset += pan;
            }
        }
    }
}