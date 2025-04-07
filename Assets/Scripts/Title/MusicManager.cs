using UnityEngine;

namespace LudumDare57.Title
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance != this) return;

            Instance = null;
        }
    }
}