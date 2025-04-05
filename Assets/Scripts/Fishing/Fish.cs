using UnityEngine;

namespace LudumDare57.Fishing
{
    [CreateAssetMenu]
    public class Fish : ScriptableObject
    {
        public string Description => description;
        public int Price => price;
        public Sprite Sprite => sprite;

        [SerializeField] private string description = "Yohohoho.";
        [SerializeField][Min(1f)] private int price = 10;
        [SerializeField] private Sprite sprite;

        public override string ToString()
        {
            return $"A fish named {name}. {description} It sells for ${Price}";
        }
    }
}