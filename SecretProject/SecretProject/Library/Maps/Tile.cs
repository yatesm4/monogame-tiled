using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public class Tile
	{
		public Vector2 index;

		public Vector2 position;

		public Sprite sprite;

		public SpriteRenderer spriteRenderer;

		public Rectangle bounds => new Rectangle((int)position.X, (int)position.Y, spriteRenderer.sprite.width, spriteRenderer.sprite.height);

		public bool free = true;

		public Tile(string tileName = "Floor/Base64x128", Vector2 i = new Vector2(), Vector2 p = new Vector2())
		{
			index = i;
			position = p;

			sprite = new Sprite(GameInstance.Instance.Content.Load<Texture2D>($"Sprites/Tiles/{tileName}"));

			spriteRenderer.LoadSprite(sprite);
		}

		public void Draw(GameTime gameTime)
		{
			spriteRenderer.Draw(
				gameTime,
				GameInstance.Instance.spriteBatch,
				position,
				SpriteEffects.None
			);
		}
	}
}
