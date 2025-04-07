using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace LudumDare57.Title
{
    public class TitleSceneFunctions : MonoBehaviour
    {
        [SerializeField] private GameObject creditsObject;
        [SerializeField] private string gameSceneName = "GameScene";
        [SerializeField] private string quitRedirectURL = "https://youtu.be/14FOPsSCIPs?si=qXa2XigqFUfiPRJ9";

        private void Awake()
        {
            Assert.IsNotNull(creditsObject);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        public void ToggleCredits()
        {
            creditsObject.SetActive(!creditsObject.activeSelf);
        }

        public void QuitGame()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer) Application.OpenURL(quitRedirectURL);
            else Application.Quit();
        }
    }
}