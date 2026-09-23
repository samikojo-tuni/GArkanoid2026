using Godot;
using System;

namespace GA.GArkanoid
{
	public partial class Paddle : CharacterBody2D
	{
		public enum ControlMode
		{
			Keyboard,
			Mouse
		}

		[Export] private float _speed = 300.0f;
		[Export] private Sprite2D _sprite;
		[Export] private ControlMode _controlMode = ControlMode.Keyboard;

		private float _horizontalInput = 0.0f;
		private float _mouseTargetX = 0.0f;

		public Vector2 Size
		{
			get
			{
				Vector2 size = _sprite.GetRect().Size;
				return size * _sprite.Scale;
			}
		}

		public override void _Process(double delta)
		{
			if (_controlMode == ControlMode.Keyboard)
			{
				_horizontalInput = Input.GetAxis("MoveLeft", "MoveRight");
			}
			else if (_controlMode == ControlMode.Mouse)
			{
				_mouseTargetX = GetGlobalMousePosition().X;
			}

			if (Input.IsActionJustPressed("LaunchBall"))
			{
				GD.Print("Launch");
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			Vector2 velocity = Velocity;
			float deltaTime = (float)delta;

			if (_controlMode == ControlMode.Mouse)
			{
				// Match the distance to the cursor exactly instead of moving at a fixed speed, so the paddle stops instead of oscillating around the cursor.
				float distance = _mouseTargetX - Position.X;
				velocity.X = Mathf.Clamp(distance / deltaTime, -_speed, _speed);
			}
			else
			{
				// "Consume" the horizontal input and move the paddle accordingly.
				float horizontaInput = _horizontalInput;
				_horizontalInput = 0.0f;

				if (!Mathf.IsZeroApprox(horizontaInput))
				{
					velocity.X = horizontaInput * _speed;
				}
				else
				{
					velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
				}
			}

			Velocity = velocity;
			MoveAndSlide();
			ClampToScreen();
		}


		private void ClampToScreen()
		{
			// Let's clamp the paddle to the screen bounds.
			// This solution assumes that the zero coortinate is at the top left of the screen.
			Vector2 position = Position;
			Vector2 windowSize = Level.Current.WindowSize;
			float minX = 0;
			float maxX = windowSize.X;
			float minPaddleX = minX + Size.X / 2;
			float maxPaddleX = maxX - Size.X / 2;
			position.X = Mathf.Clamp(position.X, minPaddleX, maxPaddleX);
			Position = position;
		}
	}
}