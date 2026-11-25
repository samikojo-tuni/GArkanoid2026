using Godot;

namespace GA.GArkanoid
{
	public abstract partial class PowerUpEffect : Resource
	{
		[Export]
		public float Duration { get; protected set; } = 0f;

		public abstract PowerUpType PowerUpType { get; }

		public float TimeRemaining { get; set; } = 0f;
		public bool IsActive { get; private set; } = false;

		/// <summary>
		/// Applies the power-up effect when first collected.
		/// </summary>
		public virtual void Activate()
		{
			IsActive = true;
			TimeRemaining = Duration;
		}

		/// <summary>
		/// Removes the power-up effect when it expires or is manually deactivated.
		/// </summary>
		public virtual void Deactivate()
		{
			IsActive = false;
		}

		/// <summary>
		/// Updates the power-up effect each frame (for timed effects).
		/// </summary>
		/// <param name="deltaTime">Time elapsed since last frame</param>
		/// <returns>True if the effect should continue, false if it should be removed</returns>
		public virtual bool Update(float deltaTime)
		{
			// Update applies only to those PowerUps that are timed.
			if (Duration > 0)
			{
				TimeRemaining -= deltaTime;
				if (TimeRemaining <= 0)
				{
					Deactivate();
				}
			}

			return IsActive;
		}
	}
}