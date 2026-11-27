using Godot;
using System;
using System.Collections.Generic;

namespace GA.GArkanoid
{
	public partial class PowerUpManager : Node2D
	{
		[Signal]
		public delegate void PowerUpActivatedEventHandler(PowerUpType powerUpType);

		[Signal]
		public delegate void PowerUpDeactivatedEventHandler(PowerUpType powerUpType);

		[Export]
		private PowerUpSceneMapping[] _powerUpSceneMapping = null;

		[Export] private PowerUpEffect[] _powerUpEffects = null;

		private List<PowerUpEffect> _activePowerUps = new List<PowerUpEffect>();

		public override void _Process(double delta)
		{
			for (int i = _activePowerUps.Count - 1; i >= 0; --i)
			{
				PowerUpEffect powerUpEffect = _activePowerUps[i];
				if (!powerUpEffect.Update((float)delta))
				{
					_activePowerUps.RemoveAt(i);
					EmitSignal(SignalName.PowerUpDeactivated, (int)powerUpEffect.PowerUpType);
				}
			}
		}

		public void ActivatePowerUp(PowerUpType powerUpType)
		{
			// Check if this type of effect is already active
			PowerUpEffect existingEffect = GetActiveEffect(powerUpType);
			if (existingEffect != null)
			{
				HandleStackingRules(existingEffect, powerUpType);
				return;
			}

			// Create and activate the new effect
			PowerUpEffect newEffect = CreatePowerUpEffect(powerUpType);
			if (newEffect != null)
			{
				newEffect.Activate();
				_activePowerUps.Add(newEffect);
				EmitSignal(SignalName.PowerUpActivated, (int)powerUpType);
			}
		}

		public void DeactivatePowerUp(PowerUpType powerUpType)
		{
			PowerUpEffect effect = GetActiveEffect(powerUpType);
			if (effect != null)
			{
				effect.Deactivate();
				_activePowerUps.Remove(effect);
				EmitSignal(SignalName.PowerUpDeactivated, (int)powerUpType);
			}
		}

		public void DeactivateAllPowerUps()
		{
			for (int i = _activePowerUps.Count - 1; i >= 0; i--)
			{
				PowerUpEffect effect = _activePowerUps[i];
				effect.Deactivate();
				EmitSignal(SignalName.PowerUpDeactivated, (int)effect.PowerUpType);
			}
			_activePowerUps.Clear();
		}

		public bool IsPowerUpActive(PowerUpType powerUpType)
		{
			return GetActiveEffect(powerUpType) != null;
		}

		public PowerUpEffect GetActiveEffect(PowerUpType powerUpType)
		{
			foreach (PowerUpEffect effect in _activePowerUps)
			{
				if (effect.PowerUpType == powerUpType)
				{
					return effect;
				}
			}
			return null;
		}

		public void SpawnPowerUp(Vector2 globalPosition, PowerUpType powerUpToSpawn)
		{
			PowerUp powerUp = null;
			foreach (PowerUpSceneMapping mapping in _powerUpSceneMapping)
			{
				if (mapping.PowerUpType == powerUpToSpawn)
				{
					powerUp = mapping.PowerUpScene.Instantiate<PowerUp>();
					break;
				}
			}

			if (powerUp != null)
			{
				powerUp.GlobalPosition = globalPosition;
				AddChild(powerUp);

				powerUp.PowerUpCollected += OnPowerUpCollected;
				powerUp.PowerUpExpired += OnPowerUpExpired;
			}
		}

		/// <summary>
		/// Handles what happens when the same power-up type is collected again.
		/// </summary>
		/// <param name="existingEffect">The currently active effect</param>
		/// <param name="newType">The type of the new power-up</param>
		private void HandleStackingRules(PowerUpEffect existingEffect, PowerUpType newType)
		{
			switch (newType)
			{
				// TODO: Expand paddle multiple times making sure it doesn't grow too big.
				case PowerUpType.ExpandPaddle:
				case PowerUpType.ThroughBall:
					// Reset the timer for these effects (refresh strategy)
					existingEffect.TimeRemaining += existingEffect.Duration;
					break;

				default:
					GD.Print($"Power-up {newType} is already active");
					break;
			}
		}

		/// <summary>
		/// Duplicates the PowerUpEffect resource based on the PowerUpType and returns it.
		/// </summary>
		/// <param name="type">The type of power-up effect to create</param>
		/// <returns>The created PowerUpEffect instance</returns>
		private PowerUpEffect CreatePowerUpEffect(PowerUpType type)
		{
			foreach (PowerUpEffect effectTemplate in _powerUpEffects)
			{
				if (effectTemplate.PowerUpType == type)
				{
					return effectTemplate.Duplicate() as PowerUpEffect;
				}
			}
			return null;
		}

		private void OnPowerUpCollected(PowerUp powerUp)
		{
			powerUp.PowerUpCollected -= OnPowerUpCollected;
			powerUp.PowerUpExpired -= OnPowerUpExpired;

			ActivatePowerUp(powerUp.PowerUpType);
		}

		private void OnPowerUpExpired(PowerUp powerUp)
		{
			powerUp.PowerUpCollected -= OnPowerUpCollected;
			powerUp.PowerUpExpired -= OnPowerUpExpired;
		}
	}
}