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