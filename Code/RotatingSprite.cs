using System;
using Godot;

namespace GA.GArkanoid
{
	public partial class RotatingSprite : Sprite2D
	{
		[Export] private float _speed = 10;
		[Export] private float _angularSpeed = 2 * Mathf.Pi;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			GD.Print("Hello, World!");
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			float deltaTime = (float)delta;
			Rotation += _angularSpeed * deltaTime;
		}
	}
}