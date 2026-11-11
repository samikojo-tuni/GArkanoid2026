using Godot;

namespace GA.GArkanoid
{
	public enum EffectType
	{
		None = 0,
		Bounce, // Collision with a wall or the paddle
		Hit, // Hit and destruction of the block
		Death, // Destruction of the ball
	}

	public partial class EffectPlayer : Node2D
	{

	}
}