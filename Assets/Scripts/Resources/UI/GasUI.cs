using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LudumDare57.Resources.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class GasUI : MonoBehaviour
    {
        private RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
                return rectTransform;
            }
        }

        [SerializeField] private GasHandler gasHandler;
        [SerializeField] private Image backImage, fillImage;

        private RectTransform rectTransform;

        private void Awake()
        {
            Assert.IsNotNull(gasHandler);
            Assert.IsNotNull(backImage);
            Assert.IsNotNull(fillImage);

            gasHandler.TankSizeChanged.AddListener(UpdateUI);
            gasHandler.GasChanged.AddListener(UpdateUI);
        }

        [ContextMenu(nameof(UpdateUI))]
        private void UpdateUI()
        {
            Vector3 localPosition = backImage.rectTransform.localPosition;
            float tankSize = gasHandler.TankSize > 0f ? gasHandler.TankSize : gasHandler.StartTankSize;
            float tankHeight = tankSize / gasHandler.MaxTankSize * RectTransform.rect.height;
            localPosition.y = -tankHeight;
            backImage.rectTransform.localPosition = localPosition;
            backImage.rectTransform.sizeDelta = new Vector2(backImage.rectTransform.sizeDelta.x, tankHeight);

            float gasHeight = gasHandler.Gas / tankSize * tankHeight;
            fillImage.rectTransform.localPosition = localPosition;
            fillImage.rectTransform.sizeDelta = new Vector2(backImage.rectTransform.sizeDelta.x, gasHeight);
        }
    }
}