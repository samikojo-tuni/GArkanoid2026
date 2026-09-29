using Godot;
using System;

namespace GA.GArkanoid
{
	public partial class Level : Node2D
	{
		#region Statics
		public static Level Current
		{
			get;
			private set;
		}
		#endregion Statics

		[Export] private Paddle _paddle;
		[Export] private Ball _ball;

		public Paddle Paddle
		{
			get { return _paddle; }
		}

		public Ball Ball
		{
			get { return _ball; }
		}

		public Vector2 WindowSize
		{
			get
			{
				return GetViewport().GetVisibleRect().Size;
			}
		}



		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Current = this;
		}
	}
}