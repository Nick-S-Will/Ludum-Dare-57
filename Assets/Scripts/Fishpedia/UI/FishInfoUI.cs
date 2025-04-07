using LudumDare57.Extensions;
using LudumDare57.Fishing;
using LudumDare57.Fishing.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LudumDare57.Fishpedia.UI
{
    public class FishInfoUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText, descriptionText, locationText, priceText, totalCaughtText;
        [SerializeField] private Image spriteImage;
        [SerializeField] private string unknownFishMessage = "Catch to learn about.";

        private string locationTextFormat, priceTextFormat, totalCaughtTextFormat;

        private void Awake()
        {
            Assert.IsNotNull(nameText);
            Assert.IsNotNull(descriptionText);
            Assert.IsNotNull(priceText);
            Assert.IsNotNull(spriteImage);

            locationTextFormat = locationText.text;
            priceTextFormat = priceText.text;
            totalCaughtTextFormat = totalCaughtText.text;
        }

        [ContextMenu(nameof(Show))]
        public void Show() => this.ShowGraphics();

        [ContextMenu(nameof(Hide))]
        public void Hide() => this.HideGraphics();

        public void Display(Fish fish)
        {
            Assert.IsNotNull(fish);

            nameText.text = fish.name;
            int caughtCount = CatchStatTracker.Instance.GetCatchCount(fish);
            descriptionText.text = caughtCount > 0 ? fish.Description : unknownFishMessage;
            locationText.text = string.Format(locationTextFormat, fish.Location);
            priceText.text = string.Format(priceTextFormat, fish.Price);
            totalCaughtText.text = string.Format(totalCaughtTextFormat, caughtCount);
            spriteImage.sprite = CatchStatTracker.Instance.HasBeenCaught(fish) ? fish.Sprite : fish.SilhouetteSprite;

            Show();
        }
    }
}