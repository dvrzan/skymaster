using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.Managers
{
    public abstract class BaseSpawnManager : MonoBehaviour, ISpawnManager
    {
        [SerializeField]
        protected float enemySpawnRate;
        [SerializeField]
        protected float powerUpSpawnRate;
        protected bool spawning;
        [SerializeField]
        protected GameObject enemyPrefab;
        [SerializeField]
        protected List<GameObject> powerUpPrefabs;

        public abstract GameObject SpawnPlayer(IGameManager gm, Vector2 position);

        public void StartSpawn(IGameManager gm)
        {
            spawning = true;
            StartCoroutine(EnemySpawn(gm));
            StartCoroutine(PowerUpSpawn(gm));
        }

        public void StopSpawn()
        {
            StopAllCoroutines();
            spawning = false;
        }

        public IEnumerator EnemySpawn(IGameManager gm)
        {
            while (spawning)
            {
                // spawn enemy at a random X position from the top of the screen
                var go = Instantiate(enemyPrefab, new Vector2(Random.Range(-8.5f, 8.5f), 8), Quaternion.identity);
                var enemy = go.GetComponent<IGameManageable>();
                enemy.AssignGameManager(gm);

                yield return new WaitForSeconds(enemySpawnRate);
            }
        }

        public IEnumerator PowerUpSpawn(IGameManager gm)
        {
            while (spawning)
            {
                // spawn random power up at a random X position from the top of the screen
                int randomPowerUp = Random.Range(0, powerUpPrefabs.Count);
                var go = Instantiate(powerUpPrefabs[randomPowerUp], new Vector2(Random.Range(-8.5f, 8.5f), 8), Quaternion.identity);
                var powerUp = go.GetComponent<IGameManageable>();
                powerUp.AssignGameManager(gm);

                yield return new WaitForSeconds(powerUpSpawnRate);
            }
        }

    }
}