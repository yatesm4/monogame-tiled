using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public static class Extensions
	{
		public static void DrawLine(this SpriteBatch spriteBatch, Vector2 begin, Vector2 end, Color color, int width = 1)
		{
			var blackDotTexture = GameInstance.Instance.Content.Load<Texture2D>("Sprites/BlackDot");

			Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length()+width, width);
			Vector2 v = Vector2.Normalize(begin - end);
			float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
			if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
			spriteBatch.Draw(blackDotTexture, r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
		}
	}
}
