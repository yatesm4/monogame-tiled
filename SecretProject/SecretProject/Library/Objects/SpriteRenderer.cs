using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public struct SpriteRenderer
	{
		public Sprite sprite;

		public Vector2 renderOrigin => new Vector2 (sprite.width / 2, sprite.height);

		public Vector2 renderOriginCustom;

		public float renderScale;

		public float animRenderTime;

		public float layerDepth;

		public void LoadSprite(Sprite spr, int frameIndex = 0)
		{
			if (sprite == spr) return;

			sprite = spr;
			sprite.animFrameIndex = frameIndex;
			
			animRenderTime = 0;

			if (renderScale == 0f) renderScale = 1f;
		}

		public void PlayAnimatedSprite(bool loop = false)
		{
			if (sprite is null) return;

			if (sprite.isSpriteSheet)
			{
				sprite.animIsLooping = loop;
				sprite.animIsPaused = false;
			}
		}

		public void PauseAnimatedSprite()
		{
			if (sprite is null) return;

			if (sprite.isSpriteSheet)
			{
				sprite.animIsPaused = true;
			}
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 drawPos, SpriteEffects sprFx)
		{
			if (sprite is null) return;

			if (sprite.isSpriteSheet)
			{
				animRenderTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

				while (animRenderTime > sprite.animFrameTime)
				{
					animRenderTime -= sprite.animFrameTime;

					if (sprite.animIsPaused is false)
					{
						if (sprite.animIsLooping)
						{
							sprite.animFrameIndex = (sprite.animFrameIndex + 1) % sprite.animFrameCount;
						}
						else
						{
							sprite.animFrameIndex = Math.Min(sprite.animFrameIndex + 1, sprite.animFrameCount - 1);
						}
					}
				}
			}

			Rectangle renderRect = new Rectangle(sprite.animFrameIndex * sprite.width, 0, sprite.width, sprite.height);

			spriteBatch.Draw(sprite.texture, drawPos, renderRect, Color.White, 0f, (renderOriginCustom == null || renderOriginCustom == Vector2.Zero) ? renderOrigin : renderOriginCustom, renderScale, sprFx, layerDepth);
		}
	}
}
