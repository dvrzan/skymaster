using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;
using AssemblyCSharp.Assets.Scripts.Miscallenous;

namespace AssemblyCSharp.Assets.Scripts.Managers
{
    public class MultiPlayerGameManager : BaseGameManager
    {
        [SerializeField]
        private int playerLives;
        private const int DEFAULT_PLAYER_LIVES = 1;
        private ISpawnManager spawnManager;

        protected override void Awake()
        {
            spawnManager = GameObject.Find("SpawnManager").GetComponent<MultiPlayerSpawnManager>();
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

            var playerObject = spawnManager.SpawnPlayer(this, new Vector2(-3, -3));
            InputController.AssignPlayerControls(playerObject.GetInstanceID(), controls);

            var colliders1 = playerObject.GetComponents<BoxCollider2D>();

            controls = new Dictionary<string, string>
            {
                {"Fire", "Fire2"},
                {"Horizontal", "Horizontal2"},
                {"Vertical", "Vertical2"}
            };

            playerObject = spawnManager.SpawnPlayer(this, new Vector2(3, -3));
            InputController.AssignPlayerControls(playerObject.GetInstanceID(), controls);

            var colliders2 = playerObject.GetComponents<BoxCollider2D>();

            foreach(var c1 in colliders1)
            {
                foreach(var c2 in colliders2)
                {
                    Physics2D.IgnoreCollision(c1, c2);
                }
            }

            ResetStats();
            spawnManager.StartSpawn(this);
            gameView.ShowHighScore(GetHighscore().ToString());
            base.StartGame();
        }

        public int GetHighscore()
        {
            int score = 0;
            if (PlayerPrefs.HasKey("MultiPlayerHighscore"))
            {
                score = PlayerPrefs.GetInt("MultiPlayerHighscore");
            }

            return score;
        }

        public void ResetStats()
        {
            ScoreModel.Score = 0;
            playerLives = DEFAULT_PLAYER_LIVES;

            gameView.SetScore(ScoreModel.Score.ToString());
            gameView.SetLives(playerLives);
        }

        public override void StopGame()
        {
            spawnManager.StopSpawn();
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
            spawnManager.StartSpawn(this);
            base.ResumeGame();
        }

        private void UpdateHighscore()
        {
            int score = 0;
            int highscore = GetHighscore();

            score = Mathf.Max(ScoreModel.Score, highscore);
            PlayerPrefs.SetInt("MultiPlayerHighscore", score);
        }

        public override void PauseGame()
        {
            spawnManager.StopSpawn();
            base.PauseGame();
        }

        public override void OnPlayerDamage(IDamageable player)
        {
            playerLives--;
            gameView.SetLives(playerLives);

            if (playerLives == 0)
            {
                var players = GameObject.FindGameObjectsWithTag("Player");
                foreach(var p in players)
                    Destroy(p);

                GameOver();
            }
        }

        public override void OnEnemyDamage(IDamageable enemy)
        {
            SetScore(ScoreModel.Score + 10);
        }

        private void SetScore(int score)
        {
            ScoreModel.Score = score;
            gameView.SetScore(ScoreModel.Score.ToString());
        }
    }
}
