using Godot;

namespace GA.Common
{
	public static class NodeExtensions
	{
		/// <summary>
		/// An extension method for calculating and returning Sprite2D's bounding box
		/// (axis aligned bounding box, AABB).
		/// </summary>
		/// <param name="sprite"></param>
		/// <returns></returns>
		public static Rect2 GetBoundingBox(this Sprite2D sprite)
		{
			if (sprite == null || sprite.Texture == null)
			{
				return default(Rect2);
			}

			// Get the size of the texture.
			Vector2 textureSize = sprite.Texture.GetSize();

			// Apply scaling and offset to the size
			Vector2 scaledSize = textureSize * sprite.Scale;
			Vector2 offset = sprite.Offset;

			// Calculate sprite's top-left coordinate (position)
			Vector2 topLeft = sprite.GlobalPosition - (scaledSize * (sprite.Centered ? 0.5f : 0f)) + offset;

			return new Rect2(topLeft, scaledSize);
		}

		public static Vector2 GetExtents(this Rect2 rectangle)
		{
			return rectangle.Size / 2f;
		}
	}
}