using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.Managers
{
    public class MultiPlayerSpawnManager: BaseSpawnManager
    {
        private static int playersSpawned;
        [SerializeField]
        private List<GameObject> playerPrefabs;

        public override GameObject SpawnPlayer(IGameManager gm, Vector2 position)
        {
            if (playersSpawned == playerPrefabs.Count)
                playersSpawned = 0;

            GameObject playerObject = Instantiate(playerPrefabs[playersSpawned], position, Quaternion.identity);
            playerObject.GetComponent<IGameManageable>().AssignGameManager(gm);

            playersSpawned++;

            return playerObject;
        }
    }
}
