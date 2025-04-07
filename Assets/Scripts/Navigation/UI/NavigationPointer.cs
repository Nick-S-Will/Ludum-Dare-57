using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare57.Navigation.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class NavigationPointer : MonoBehaviour
    {
        [SerializeField] private Transform player, target;
        [SerializeField] private RectTransform pointer;

        private RectTransform rectTransform;

        private void Awake()
        {
            Assert.IsNotNull(player);
            Assert.IsNotNull(target);
            Assert.IsNotNull(pointer);

            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(player.position);
            Vector2 targetScreenPosition = Camera.main.WorldToScreenPoint(target.position);
            Vector2 direction = (targetScreenPosition - playerScreenPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            pointer.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}