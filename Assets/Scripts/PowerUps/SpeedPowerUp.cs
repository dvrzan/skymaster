namespace AssemblyCSharp.Assets.Scripts.PowerUps
{
    public class SpeedPowerUp: MovingPowerUp
    {
        private float oldSpeed;

        protected override void StartEffect()
        {
            if(playerObject != null)
            {
                if(playerObject != null)
                {
                    oldSpeed = playerObject.speed;
                    playerObject.speed *= 1.5f;
                }
                base.StartEffect();
            }
        }

        protected override void EndEffect()
        {
            if(playerObject != null)
                playerObject.speed = oldSpeed;
            base.EndEffect();
        }
    }
}
