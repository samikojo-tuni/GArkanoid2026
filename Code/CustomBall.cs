using Godot;
using System;

namespace GA.GArkanoid
{
	public partial class CustomBall : Sprite2D
	{
		[Export] private Vector2 _direction = new Vector2(1, -1);
		[Export] private float _speed = 100f;

		// Contains references to all walls in the level.
		[Export] private Sprite2D[] _walls = null;

		public Vector2 Direction
		{
			get
			{
				return _direction.IsNormalized() ? _direction : _direction.Normalized();
				// The line above is exactly the same as the out-commented code block below.
				// if (_direction.IsNormalized())
				// {
				// 	return _direction;
				// }
				// else
				// {
				// 	return _direction.Normalized();
				// }
			}
		}

		public float Speed
		{
			get { return Mathf.Clamp(_speed, Config.MinSpeed, Config.MaxSpeed); }
		}

		public Vector2 Velocity
		{
			get { return Direction * Speed; }
		}

		public override void _Process(double delta)
		{
			float deltaTime = (float)delta;

			// Move the ball here. Since this ball doesn't use Physics, it can be moved in this method.
			Vector2 initialPosition = Position;
			Vector2 movement = Velocity * deltaTime;
			Position = ResolveWallCollisions(initialPosition + movement);
		}

		private Vector2 ResolveWallCollisions(Vector2 newPosition)
		{
			// TODO: Do collision checks with all walls here and bounce the ball if needed.
			// Bouncing here means that you calculate the new direction for the ball. Take into
			// account how far into the wall the ball ended up and use that distance in the
			// bounce vector.

			return newPosition;
		}
	}
}