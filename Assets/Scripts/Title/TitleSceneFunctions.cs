using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare57.Title
{
    public class TitleSceneFunctions : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour credits;
        [SerializeField] private string gameSceneName = "GameScene";
        [SerializeField] private string quitRedirectURL = "https://youtu.be/14FOPsSCIPs?si=qXa2XigqFUfiPRJ9";

        public void StartGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        public void ToggleCredits()
        {
            credits.enabled = !credits.enabled;
        }

        public void QuitGame()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer) Application.OpenURL(quitRedirectURL);
            else Application.Quit();
        }
    }
}