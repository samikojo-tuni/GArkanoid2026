using Godot;
using System;

namespace GA.GArkanoid
{
	public partial class CustomBall : Sprite2D
	{
		[Export] private Sprite2D[] _walls;
		[Export] private Vector2 _direction = Vector2.Zero;
		[Export] private float _speed = 0.0f;
		[Export] private bool _useRadius = false;

		private float _radius = 0.0f;

		public Vector2 Direction
		{
			get { return _direction; }
			set { _direction = value.Normalized(); } // Ensure the direction is always normalized
		}

		public float Speed
		{
			get { return _speed; }
			set { _speed = Mathf.Clamp(value, 0.0f, 500); }
		}

		public bool IsLaunched { get { return !Direction.IsEqualApprox(Vector2.Zero); } }

		public Vector2 Velocity
		{
			get { return Direction * Speed; }
		}

		override public void _Ready()
		{
			_radius = Texture.GetSize().X * Scale.X * 0.5f;
			Launch(_direction, _speed);
		}

		public void Launch(Vector2 direction, float speed)
		{
			Direction = direction;
			Speed = speed;
		}

		public override void _Process(double delta)
		{
			// Move the ball here. Since this ball doesn't use Physics, it can be moved in this method.
			if (!IsLaunched)
			{
				return;
			}

			Position = ResolveWallCollisions(Position + Velocity * (float)delta);
		}

		private Vector2 ResolveWallCollisions(Vector2 newPosition)
		{
			foreach (var wall in _walls)
			{
				CustomPhysics.Hit hit;
				if (_useRadius)
				{
					hit = CustomPhysics.Intersects(wall.GetBoundingBox(), newPosition, _radius);
				}
				else
				{
					hit = CustomPhysics.Intersects(wall.GetBoundingBox(), newPosition);
				}

				if (hit == null)
				{
					continue;
				}

				Direction = CustomPhysics.Bounce(Direction, hit.Normal).Normalized();
			}

			return newPosition;
		}
	}
}