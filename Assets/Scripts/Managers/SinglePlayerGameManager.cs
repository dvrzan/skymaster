using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;
using AssemblyCSharp.Assets.Scripts.Miscallenous;
using AssemblyCSharp.Assets.Scripts.DamagableEntities;

namespace AssemblyCSharp.Assets.Scripts.Managers
{
    public class SinglePlayerGameManager: BaseGameManager
    {
        protected ISpawnManager spawnManager;

        [SerializeField]
        private int playerLives;
        private const int DEFAULT_PLAYER_LIVES = 3;

        protected override void Awake()
        {
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SinglePlayerSpawnManager>();
            base.Awake();
        }

        public override void StartGame()
        {
            if (playerLives < 1)
                playerLives = DEFAULT_PLAYER_LIVES;

            var controls = new Dictionary<string, string>
            {
                {"Fire", "Fire1"},
                {"Horizontal", "Horizontal1"},
                {"Vertical", "Vertical1"}
            };

            var playerObject = spawnManager.SpawnPlayer(this, new Vector2(0, -3));
            InputController.AssignPlayerControls(playerObject.GetInstanceID(), controls);

            ResetStats();
            spawnManager.StartSpawn(this);
            gameView.ShowHighScore(GetHighscore().ToString());
            base.StartGame();
        }

        public int GetHighscore()
        {
            int score = 0;
            if (PlayerPrefs.HasKey("SinglePlayerHighscore"))
            {
                score = PlayerPrefs.GetInt("SinglePlayerHighscore");
            }

            return score;
        }

        public override void StopGame()
        {
            UpdateHighscore();
            spawnManager.StopSpawn();
            base.StopGame();
        }

        public override void GameOver()
        {
            spawnManager.StopSpawn();
            UpdateHighscore();
            base.GameOver();
        }

        public override void ResumeGame()
        {
            base.ResumeGame();
            spawnManager.StartSpawn(this);
        }

        public override void PauseGame()
        {
            base.PauseGame();
            spawnManager.StopSpawn();
        }

        private void UpdateHighscore()
        {
            int score = 0;
            int highscore = GetHighscore();

            score = Mathf.Max(ScoreModel.Score, highscore);
            PlayerPrefs.SetInt("SinglePlayerHighscore", score);
            PlayerPrefs.Save();
        }

        public void ResetStats()
        {
            ScoreModel.Score = 0;
            playerLives = DEFAULT_PLAYER_LIVES;

            gameView.SetScore(ScoreModel.Score.ToString());
            gameView.SetLives(playerLives);
        }

        public override void OnEnemyDamage(IDamageable enemy)
        {
            SetScore(ScoreModel.Score + 10);
        }

        public override void OnPlayerDamage(IDamageable player)
        {
            playerLives--;
            gameView.SetLives(playerLives);

            if(playerLives == 0)
            {
                Destroy(((Player)player).gameObject);
                GameOver();
            }
        }

        private void SetScore(int score)
        {
            ScoreModel.Score = score;
            gameView.SetScore(ScoreModel.Score.ToString());
        }
    }
}
