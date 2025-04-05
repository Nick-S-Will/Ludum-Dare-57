using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LudumDare57.Resources.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class GasUI : MonoBehaviour
    {
        [SerializeField] private GasHandler gasHandler;
        [SerializeField] private Image backImage, fillImage;

        private RectTransform rectTransform;

        private void Awake()
        {
            Assert.IsNotNull(gasHandler);
            Assert.IsNotNull(backImage);
            Assert.IsNotNull(fillImage);

            rectTransform = GetComponent<RectTransform>();

            gasHandler.GasChanged.AddListener(UpdateUI);
            gasHandler.TankSizeChanged.AddListener(UpdateUI);
        }

        private void UpdateUI()
        {
            Vector3 localPosition = backImage.rectTransform.localPosition;

            float tankHeight = gasHandler.TankSize / gasHandler.MaxTankSize * rectTransform.rect.height;
            localPosition.y = -tankHeight;
            backImage.rectTransform.localPosition = localPosition;
            backImage.rectTransform.sizeDelta = new Vector2(backImage.rectTransform.sizeDelta.x, tankHeight);

            float gasHeight = gasHandler.Gas / gasHandler.TankSize * tankHeight;
            fillImage.rectTransform.localPosition = localPosition;
            fillImage.rectTransform.sizeDelta = new Vector2(backImage.rectTransform.sizeDelta.x, gasHeight);
        }
    }
}