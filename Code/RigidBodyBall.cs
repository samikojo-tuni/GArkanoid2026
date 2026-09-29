using Godot;
using System;

namespace GA.GArkanoid
{
	public partial class RigidBodyBall : RigidBody2D
	{
		[Export] private float _force = 100;
		public override void _Ready()
		{
			// TODO: Explain continuous collision detection
			ApplyImpulse(new Vector2(1, -1).Normalized() * _force);
		}
	}
}