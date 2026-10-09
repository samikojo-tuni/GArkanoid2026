using Godot;
using System;

namespace GA.GArkanoid
{
	public partial class GameManager : Node
	{
		#region Singleton
		public static GameManager Instance
		{
			get;
			private set;
		}

		public sealed override void _Ready()
		{
			GD.Print("Initializing Game Manager");
			if (Instance == null)
			{
				Instance = this;
			}
			else if (Instance != this)
			{
				// The one and only instance of this class already exist.
				QueueFree();
				return;
			}

			Initialize();
		}
		#endregion

		[Signal]
		public delegate void ScoreChangedEventHandler(int score);

		private int _score = 0;

		public int Score
		{
			get { return _score; }
			private set
			{
				// TODO: Validate the score value.
				_score = value;
				EmitSignal(SignalName.ScoreChanged, _score);
			}
		}


		protected virtual void Initialize()
		{
			GD.Print("Game Manager initialized!");
		}

		public void AddScore(int scoreToAdd)
		{
			if (scoreToAdd <= 0)
			{
				// scoreToAdd is not valid.
				return;
			}

			Score += scoreToAdd;

			// if (scoreAddedCallback != null)
			// {
			// 	scoreAddedCallback(scoreToAdd);
			// }
			// Same as above
			// scoreAddedCallback?.Invoke(scoreToAdd);
		}
	}
}