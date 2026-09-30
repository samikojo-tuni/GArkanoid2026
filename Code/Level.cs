using Godot;
using System;

namespace GA.GArkanoid
{
	public partial class Level : Node2D
	{
		public static Level Current
		{
			get;
			private set;
		}

		[Export] private Paddle _paddle;
		[Export] private int _wallWidth;

		public Paddle Paddle { get { return _paddle; } }
		public int WallWidth { get { return _wallWidth; } }

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