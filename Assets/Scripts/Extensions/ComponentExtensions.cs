#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare57.Extensions
{
    public static class ComponentExtensions
    {
        public static void ShowGraphics(this Component component)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) Undo.RecordObjects(component.GetComponentsInChildren<Graphic>(), $"{nameof(ShowGraphics)} graphics");
#endif
            SetGraphicsVisible(component, true);
        }

        public static void HideGraphics(this Component component)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) Undo.RecordObjects(component.GetComponentsInChildren<Graphic>(), $"{nameof(HideGraphics)} graphics");
#endif
            SetGraphicsVisible(component, false);
        }

        public static void SetGraphicsVisible(this Component component, bool visible)
        {
            foreach (Graphic graphic in component.GetComponentsInChildren<Graphic>()) graphic.enabled = visible;
        }
    }
}