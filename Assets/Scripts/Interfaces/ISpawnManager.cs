using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Interfaces
{
    public interface ISpawnManager
    {
        void StartSpawn(IGameManager gm);
        void StopSpawn();
        GameObject SpawnPlayer(IGameManager gm, Vector2 position);
    }
}
