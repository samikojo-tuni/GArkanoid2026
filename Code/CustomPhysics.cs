using System;
using Godot;
using GA.Common;

namespace GA.GArkanoid
{
	public static class CustomPhysics
	{
		public class Hit
		{
			public Vector2 Point { get; set; }
			public Vector2 Normal { get; set; }
			public Vector2 Penetration { get; set; }
		}

		public static Hit Intersects(Rect2 rect, Vector2 point)
		{
			Vector2 center = rect.GetCenter();
			Vector2 extents = rect.GetExtents();
			Vector2 delta = point - center;

			// Negative penetration means the point is outside on that axis
			float penetrationX = extents.X - Mathf.Abs(delta.X);
			float penetrationY = extents.Y - Mathf.Abs(delta.Y);

			if (penetrationX < 0 || penetrationY < 0)
			{
				return null;
			}

			// The axis with the smallest penetration is the closest edge to push out of
			Vector2 normal;
			Vector2 penetrationVector;
			Vector2 collisionPoint;

			if (penetrationX < penetrationY)
			{
				// Collision on the vertical edge (left or right)
				float signX = Mathf.Sign(delta.X);
				normal = new Vector2(signX, 0);
				penetrationVector = new Vector2(penetrationX * signX, 0);
				collisionPoint = new Vector2(center.X + (extents.X * signX), point.Y);
			}
			else
			{
				// Collision on the horizontal edge (top or bottom)
				float signY = Mathf.Sign(delta.Y);
				normal = new Vector2(0, signY);
				penetrationVector = new Vector2(0, penetrationY * signY);
				collisionPoint = new Vector2(point.X, center.Y + (extents.Y * signY));
			}

			return new Hit
			{
				Point = collisionPoint,
				Normal = normal,
				Penetration = penetrationVector
			};
		}

		public static Hit Intersects(Rect2 rect, Vector2 point, float radius)
		{
			// Clamping the circle center to the rect finds the nearest point on it
			float closestX = Mathf.Clamp(point.X, rect.Position.X, rect.Position.X + rect.Size.X);
			float closestY = Mathf.Clamp(point.Y, rect.Position.Y, rect.Position.Y + rect.Size.Y);
			Vector2 closestPoint = new Vector2(closestX, closestY);

			Vector2 delta = point - closestPoint;
			float distanceSquared = delta.LengthSquared();

			if (distanceSquared > radius * radius)
			{
				return null;
			}

			Vector2 normal = delta.Normalized();
			float penetrationDepth = radius - Mathf.Sqrt(distanceSquared);
			Vector2 penetrationVector = normal * penetrationDepth;

			return new Hit
			{
				Point = closestPoint,
				Normal = normal,
				Penetration = penetrationVector
			};
		}

		public static Vector2 Bounce(Vector2 direction, Vector2 normal)
		{
			Vector2 u = direction.Dot(normal) * normal;
			Vector2 w = direction - u;
			return w - u;
		}
	}
}