using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp.Assets.Scripts.UserInterface
{
    public class MainMenuView : MonoBehaviour
    {
        private Canvas mainMenuCanvas;
        private Canvas gameSelectCanvas;
        private AudioSource mainMenuMusic;
        private Slider audioSlider;

        private void Awake()
        {
            mainMenuMusic = GameObject.Find("MainMenuMusic").GetComponent<AudioSource>();

            var audioWindow = transform.Find("AudioWindow").gameObject;
            audioSlider = audioWindow.transform.Find("MusicVolumeSlider").gameObject.GetComponent<Slider>();

            mainMenuCanvas = transform.Find("MainMenuCanvas").gameObject.GetComponent<Canvas>();
            gameSelectCanvas = transform.Find("GameSelectCanvas").gameObject.GetComponent<Canvas>();

            gameSelectCanvas.enabled = false;
        }

        private void Update()
        {
            mainMenuMusic.volume = audioSlider.value;
        }

        public void ShowMainMenuWindow(bool show)
        {
            mainMenuCanvas.enabled = show;
        }

        public void ShowGameSelectWindow(bool show)
        {
            gameSelectCanvas.enabled = show;
        }
    }
}
