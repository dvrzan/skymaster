using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp.Assets.Scripts.UserInterface
{
    public class GameView: MonoBehaviour
    {
        private Text scoreCounterText;
        private Text bestScoreText;
        private Image lifeCounterImage;
        private Animator animator;
        [SerializeField]
        private List<Sprite> lifeSprites;

        void Awake()
        {
            animator = transform.Find("PauseMenuWindow").GetComponent<Animator>();

            var scoreWindow = transform.Find("ScoreWindow").gameObject;
            var scoreContainer = scoreWindow.transform.Find("ScoreContainer").gameObject;
            scoreCounterText = scoreContainer.transform.Find("ScoreCounterText").gameObject.GetComponent<Text>();

            var bestScoreContainer = scoreWindow.transform.Find("BestScoreContainer").gameObject;
            bestScoreText = bestScoreContainer.transform.Find("BestScoreCounterText").gameObject.GetComponent<Text>();

            lifeCounterImage = transform.Find("LifeCounter").gameObject.GetComponent<Image>();
        }

        public void ShowIntroWindow(bool show)
        {
            transform.Find("IntroWindow").gameObject.SetActive(show);
        }

        public void ShowLifeCounter(bool show)
        {
            transform.Find("LifeCounter").gameObject.SetActive(show);
        }

        public void ShowGameOverText(bool show)
        {
            transform.Find("GameOverWindow").gameObject.SetActive(show);
        }

        public void ShowScoreWindow(bool show)
        {
            transform.Find("ScoreWindow").gameObject.SetActive(show);
        }

        public void ShowPauseMenu(bool show)
        {
            var go = transform.Find("PauseMenuWindow").gameObject;
            go.SetActive(show);
        }

        public void ShowHighScore(string highscore)
        {
            bestScoreText.text = highscore;
        }

        public void SetScore(string score)
        {
            scoreCounterText.text = score;
        }

        public void SetLives(int lives)
        {
            lifeCounterImage.sprite = lifeSprites[lives];
        }
    }
}
