using UnityEngine;
using UnityEngine.SceneManagement;
using AssemblyCSharp.Assets.Scripts.UserInterface;

namespace AssemblyCSharp.Assets.Scripts.Managers
{
    public class MainMenuManager: MonoBehaviour
    {
        private MainMenuView mainMenuView;

        private void Awake()
        {
            mainMenuView = GameObject.Find("Canvas").GetComponent<MainMenuView>();
        }

        public void StartSinglePlayer()
        {
            SceneManager.LoadScene("SinglePlayer");
        }

        public void StartMultiPlayer()
        {
            SceneManager.LoadScene("MultiPlayer");
        }

        public void ShowMainMenuWindow()
        {
            mainMenuView.ShowMainMenuWindow(true);
            mainMenuView.ShowGameSelectWindow(false);
        }

        public void ShowGameSelectWindow()
        {
            mainMenuView.ShowGameSelectWindow(true);
            mainMenuView.ShowMainMenuWindow(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
