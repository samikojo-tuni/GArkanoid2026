using Godot;

namespace GA.GArkanoid
{
	[GlobalClass]
	public partial class EffectMap : Resource
	{
		[Export]
		private EffectSceneEntry[] _entries = null;

		public PackedScene GetScene(EffectType effectType)
		{
			foreach (EffectSceneEntry effectSceneEntry in _entries)
			{
				if (effectSceneEntry.EffectType == effectType)
				{
					return effectSceneEntry.EffectScene;
				}
			}

			return null;
		}
	}
}