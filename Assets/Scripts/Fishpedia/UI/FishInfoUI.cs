using LudumDare57.Extensions;
using LudumDare57.Fishing;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LudumDare57.Fishpedia.UI
{
    public class FishInfoUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText, descriptionText, priceText;
        [SerializeField] private Image spriteImage;

        private string priceTextFormat;

        private void Awake()
        {
            Assert.IsNotNull(nameText);
            Assert.IsNotNull(descriptionText);
            Assert.IsNotNull(priceText);
            Assert.IsNotNull(spriteImage);

            priceTextFormat = priceText.text;
        }

        [ContextMenu(nameof(Show))]
        public void Show() => this.ShowGraphics();

        [ContextMenu(nameof(Hide))]
        public void Hide() => this.HideGraphics();

        public void Display(Fish fish)
        {
            nameText.text = fish.name;
            descriptionText.text = fish.Description;
            priceText.text = string.Format(priceTextFormat, fish.Price);
            spriteImage.sprite = fish.Sprite;

            Show();
        }
    }
}