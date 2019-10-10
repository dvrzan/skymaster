using AssemblyCSharp.Assets.Scripts.DamagableEntities;

namespace AssemblyCSharp.Assets.Scripts.Interfaces
{
    public interface IGameManager
    {
        void StartGame();
        void StopGame();
        void PauseGame();
        void ResumeGame();

        void OnEnemyDamage(IDamageable player);
        void OnPlayerDamage(IDamageable player);
    }
}
