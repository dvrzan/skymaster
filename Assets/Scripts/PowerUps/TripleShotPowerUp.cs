using AssemblyCSharp.Assets.Scripts.Interfaces;
using AssemblyCSharp.Assets.Scripts.ShootingControllers;

namespace AssemblyCSharp.Assets.Scripts.PowerUps
{
    public class TripleShotPowerUp: MovingPowerUp
    {
        private IShootingController oldShootingController;

        protected override void StartEffect()
        {
            if(playerObject != null)
            {
                oldShootingController = playerObject.ShootingController;
                playerObject.ShootingController = new TripleShotController();
            }
            base.StartEffect();
        }

        protected override void EndEffect()
        {
            if(playerObject != null)
            {
                playerObject.ShootingController = oldShootingController;
            }
            base.EndEffect();
        }
    }
}
