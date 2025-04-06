using UnityEngine;

namespace LudumDare57.Fishing
{
    [CreateAssetMenu]
    public class Fish : ScriptableObject
    {
        public string Description => description;
        public int Price => price;
        public float CatchTime => catchTime;
        public Sprite Sprite => sprite;
        public Sprite SilhouetteSprite => silhouetteSprite;

        [SerializeField] private string description = "Yohohoho.";
        [SerializeField][Min(1f)] private int price = 10;
        [SerializeField][Min(1e-5f)] private float catchTime = 1f;
        [SerializeField] private Sprite sprite, silhouetteSprite;

        public override string ToString()
        {
            return $"A fish named {name}. {description} It sells for ${Price}";
        }
    }
}