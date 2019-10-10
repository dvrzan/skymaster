using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AssemblyCSharp.Assets.Scripts.Interfaces;
using AssemblyCSharp.Assets.Scripts.UserInterface;

namespace AssemblyCSharp.Assets.Scripts.Managers
{
    public class BaseGameManager : MonoBehaviour, IGameManager
    {
        protected bool gameOver = true;
        protected bool gamePaused;
        protected readonly List<string> entityTags = new List<string> { "Player", "EnemyShip", "PowerUp", "Effect", "Projectile" };
        protected GameView gameView;

        protected virtual void Awake()
        {
            gameView = GameObject.Find("Canvas").GetComponent<GameView>();
        }

        protected virtual void Update()
        {
            if (gameOver)
            {
                if (InputController.IsSubmitButtonPressed)
                {
                    StartGame();
                }
                else if (InputController.IsCancelButtonPressed)
                {
                    StopGame();
                }
            }
            else
            {
                if (InputController.IsPauseButtonPressed)
                {
                    if (gamePaused)
                        ResumeGame();
                    else
                        PauseGame();
                }
            }
        }

        private List<GameObject> FindEntitiesWithTags(ICollection<string> tags)
        {
            List<GameObject> entities = new List<GameObject>();
            foreach (var t in tags)
            {
                var objects = GameObject.FindGameObjectsWithTag(t);
                entities.AddRange(objects);
            }

            return entities;
        }

        public virtual void StartGame()
        {
            InputController.Enable();
            gameView.ShowIntroWindow(false);
            gameView.ShowGameOverText(false);

            gameView.ShowScoreWindow(true);
            gameView.ShowLifeCounter(true);

            gameOver = false;
        }

        public virtual void StopGame()
        {
            Time.timeScale = 1;
            gameOver = true;
            SceneManager.LoadScene("MainMenu");
        }

        public virtual void GameOver()
        {
            InputController.Disable();
            ShowWithTag(false, entityTags);

            gameView.ShowScoreWindow(false);
            gameView.ShowLifeCounter(false);

            gameView.ShowGameOverText(true);

            gameOver = true;
        }

        public virtual void PauseGame()
        {
            InputController.Disable();
            ShowWithTag(false, entityTags);

            gameView.ShowScoreWindow(false);
            gameView.ShowLifeCounter(false);

            gameView.ShowPauseMenu(true);
            gamePaused = true;
            Time.timeScale = 0;
        }

        public virtual void ResumeGame()
        {
            InputController.Enable();
            ShowWithTag(true, entityTags);

            gameView.ShowPauseMenu(false);

            gameView.ShowScoreWindow(true);
            gameView.ShowLifeCounter(true);
            gamePaused = false;
            Time.timeScale = 1;
        }

        public virtual void OnEnemyDamage(IDamageable enemy)
        {
        }

        public virtual void OnPlayerDamage(IDamageable player)
        {
        }

        protected void ShowWithTag(bool show, ICollection<string> tags)
        {
            var objects = FindEntitiesWithTags(tags);
            foreach(var o in objects)
            {
                var showable = o.GetComponent<IShowable>();
                if(showable != null)
                    showable.Show(show);
            }
        }
    }
}