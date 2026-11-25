using System;
using Godot;

namespace GA.GArkanoid
{
	/// <summary>
	/// Reprecents the PowerUp Node before collecting and activating it.
	/// It should be spawned randomly when a block is destroyed and it
	/// can be activated by collecting it.
	/// </summary>
	public partial class PowerUp : Area2D
	{
		[Signal] public delegate void PowerUpCollectedEventHandler(PowerUp powerUp);
		[Signal] public delegate void PowerUpExpiredEventHandler(PowerUp powerUp);

		[Export] private float _fallSpeed = 100f; // Pixels per second.

		// The time in secods after this Node is freed if not collected.
		[Export] private float _despawnTime = 5f;

		[Export]
		public PowerUpType PowerUpType { get; set; } = PowerUpType.None;

		private float _aliveTime = 0f;
		private bool _isCollected = false;

		public override void _EnterTree()
		{
			BodyEntered += OnBodyEntered;
		}

		public override void _ExitTree()
		{
			BodyEntered -= OnBodyEntered;
		}

		public override void _PhysicsProcess(double delta)
		{
			if (_isCollected)
			{
				return;
			}

			float deltaTime = (float)delta;

			// The PowerUp Node falls down.
			GlobalPosition += Vector2.Down * _fallSpeed * deltaTime;

			// Keep track of how long the PowerUp has been alive
			_aliveTime += deltaTime;

			// Despawn the PowerUp Node if it's not collected on time.
			if (_aliveTime >= _despawnTime)
			{
				EmitSignal(SignalName.PowerUpExpired, this);
				QueueFree();
			}
		}

		private void OnBodyEntered(Node2D body)
		{
			// Only the Paddle should be able to collect the PowerUp.
			if (!_isCollected && body is Paddle paddle)
			{
				CollectPowerUp();
			}
		}

		private void CollectPowerUp()
		{
			if (_isCollected)
			{
				return;
			}

			_isCollected = true;

			EmitSignal(SignalName.PowerUpCollected, this);

			QueueFree();
		}
	}
}