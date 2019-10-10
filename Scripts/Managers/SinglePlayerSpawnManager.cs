using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.Assets.Scripts.Interfaces;

namespace AssemblyCSharp.Assets.Scripts.Managers
{
    public class SinglePlayerSpawnManager : BaseSpawnManager
    {
        [SerializeField]
        private GameObject playerPrefab;

        public override GameObject SpawnPlayer(IGameManager gm, Vector2 position)
        {
            GameObject playerObject = Instantiate(playerPrefab, position, Quaternion.identity);
            playerObject.GetComponent<IGameManageable>().AssignGameManager(gm);

            return playerObject;
        }
    }
}