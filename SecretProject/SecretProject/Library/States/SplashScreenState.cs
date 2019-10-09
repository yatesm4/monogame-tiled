using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SecretProject.Library.Objects;

namespace SecretProject.Library.States
{
	public class SplashScreenState : State
	{
		protected Sprite sprYesDevLogo;
		protected SpriteRenderer sprRendLogo;

		protected int exitCountdown;

		public SplashScreenState(GraphicsDevice graphicsDevice, ContentManager contentManager) : base(graphicsDevice, contentManager)
		{
			sprYesDevLogo = new Sprite(contentManager.Load<Texture2D>("Sprites/Branding/YD_Logo"), 0.15f)
			{
				animIsLooping = false
			};

			sprRendLogo.renderScale = 3.0f;
			sprRendLogo.LoadSprite(sprYesDevLogo);
			sprRendLogo.PlayAnimatedSprite();
		}

		public override void Update(GameTime gameTime)
		{
			if (exitCountdown > 0)
				exitCountdown--;
			else if (exitCountdown == 0)
				GameInstance.Instance.ChangeState(new MainGameState(GameInstance.Instance.GraphicsDevice, GameInstance.Instance.Content));
		}

		public override void PostUpdate(GameTime gameTime)
		{
			
		}

		public override void Draw(GameTime gameTime)
		{
			gd.Clear(Color.Wheat);

			GameInstance.Instance.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

			sprRendLogo.Draw(gameTime, GameInstance.Instance.spriteBatch, new Vector2(gd.Viewport.Width / 2, (gd.Viewport.Height / 3) * 2), SpriteEffects.None);

			GameInstance.Instance.spriteBatch.End();
		}
	}
}
