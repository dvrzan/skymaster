using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.PowerUps
{
    public class ShieldPowerUp: MovingPowerUp
    {
        protected override void StartEffect()
        {
            if(playerObject != null)
            {
                playerObject.invulnearable = true;

                var shieldSprite = playerObject.transform.Find("Shield").gameObject;
                shieldSprite.SetActive(true);
            }
            base.StartEffect();
        }

        protected override void EndEffect()
        {
            if(playerObject != null)
            {
                playerObject.invulnearable = false;

                var shieldSprite = playerObject.transform.Find("Shield").gameObject;
                shieldSprite.SetActive(false);
            }
            base.EndEffect();
        }
    }
}
