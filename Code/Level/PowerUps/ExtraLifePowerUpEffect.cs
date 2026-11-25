namespace GA.GArkanoid
{
	public partial class ExtraLifePowerUpEffect : PowerUpEffect
	{
		public override PowerUpType PowerUpType => PowerUpType.ExtraLife;

		public override void Activate()
		{
			base.Activate();
			GameManager.Instance.AddLife();

			// This PowerUp is not timed, so it can be deactivated immediately.
			Deactivate();
		}
	}
}