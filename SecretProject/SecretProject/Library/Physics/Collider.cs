using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SecretProject.Library.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public class Collider
	{
		public Vector2 position;

		public Vector2 dimensions;

		public Vector2 positionOffset;

		public RectangleF bounds => new RectangleF(
			(int)position.X + positionOffset.X,
			(int)position.Y + positionOffset.Y - dimensions.Y,
			dimensions.X,
			dimensions.Y
		);

		public void DrawBounds()
		{
			var corners = bounds.GetCorners();

			GameInstance.Instance.spriteBatch.DrawLine(corners[0], corners[1], Color.Magenta, 1);
			GameInstance.Instance.spriteBatch.DrawLine(corners[1], corners[2], Color.Magenta, 1);
			GameInstance.Instance.spriteBatch.DrawLine(corners[2], corners[3], Color.Magenta, 1);
			GameInstance.Instance.spriteBatch.DrawLine(corners[3], corners[0], Color.Magenta, 1);
		}
	}
}
