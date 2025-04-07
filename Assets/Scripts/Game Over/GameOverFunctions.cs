using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare57.GameOver
{
    public class GameOverFunctions : MonoBehaviour
    {
        [SerializeField] private string titleSceneSceneName = "TitleScene";

        public void GoToTitle()
        {
            SceneManager.LoadScene(titleSceneSceneName);
        }
    }
}