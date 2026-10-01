using Godot;

namespace GA.GArkanoid
{
	public static class NodeExtensions
	{
		public static Rect2 GetBoundingBox(this Sprite2D sprite)
		{
			if (sprite.Texture == null)
			{
				return new Rect2();
			}

			// Get the size of the texture
			Vector2 textureSize = sprite.Texture.GetSize();

			// Apply scale and offset
			Vector2 scaledSize = textureSize * sprite.Scale;
			Vector2 offset = sprite.Offset;

			// Calculate the top-left corner of the bounding box
			Vector2 topLeft = sprite.GlobalPosition - (scaledSize * (sprite.Centered ? 0.5f : 0f)) + offset;

			return new Rect2(topLeft, scaledSize);
		}

		public static Vector2 GetExtents(this Rect2 rectangle)
		{
			return rectangle.Size / 2;
		}
	}
}