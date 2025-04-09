using System;
using UnityEngine;

namespace LudumDare57.Fishing
{
    [CreateAssetMenu]
    public class Fish : ScriptableObject
    {
        public string Description => description;
        public string Location => location;
        public int Price => price;
        public float CatchTime => catchTime;
        public Sprite Sprite => sprite;
        public Sprite SilhouetteSprite => silhouetteSprite;

        [SerializeField][TextArea] private string description = "Yohohoho.", location = "Shallows";
        [SerializeField][Min(1f)] private int price = 10;
        [SerializeField][Min(1e-5f)] private float catchTime = 1f;
        [SerializeField] private Sprite sprite, silhouetteSprite;

        public bool IsValid() => description != null && location != null && price > 0 && catchTime > 0f && sprite != null && silhouetteSprite != null;

        public override string ToString()
        {
            return $"A fish named {name}. {description} It sells for ${Price}";
        }
    }
}