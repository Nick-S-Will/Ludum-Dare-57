using UnityEngine;

namespace LudumDare57.Terrain
{
    [RequireComponent(typeof(MeshRenderer))]
    public class TextureSlideAnimation : MonoBehaviour
    {
        [SerializeField] private Vector2 slideSpeed = Vector2.one;

        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            meshRenderer.material.mainTextureOffset += Time.deltaTime * slideSpeed;
        }
    }
}