using Godot;
using System;

namespace GA.GArkanoid
{
	public partial class Ball : Node2D
	{
		[Export] private float _speed = 10;

		public float Speed
		{
			get { return _speed; }
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			float deltaTime = (float)delta;

			Vector2 input = Input.GetVector("Left", "Right", "Up", "Down");

			Position += input * _speed * deltaTime;

			// GD.Print($"Input: {input}");
		}

		/// <summary>
		/// If physics engine is used, physics calculatons should be done here.
		/// </summary>
		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
		}

	}
}