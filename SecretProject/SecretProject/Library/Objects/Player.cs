using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public class Player : Entity
	{
		protected override void InitializeEntity()
		{
			headSprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Head/03"));
			headSprites.isSpriteSheet = true;

			southEastBodySprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Body/02_SouthEast"), 0.15f);
			northEastBodySprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Body/02_NorthEast"), 0.15f);
			southWestBodySprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Body/02_SouthWest"), 0.15f);
			northWestBodySprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Body/02_NorthWest"), 0.15f);

			headSpriteRenderer.LoadSprite(headSprites, 2);
			bodySpriteRenderer.LoadSprite(southWestBodySprites);

			headSpriteRenderer.layerDepth = 1;
			bodySpriteRenderer.layerDepth = 1;

			moveSpeed = 100;
		}

		public override void Update(GameTime gameTime)
		{
			var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

			HandleInput(deltaTime);
		}

		protected void HandleInput(float deltaTime)
		{
			var kbState = Keyboard.GetState();

			if (kbState.IsKeyDown(Keys.W)) position += new Vector2(0, -moveSpeed) * deltaTime;
			else if (kbState.IsKeyDown(Keys.S)) position += new Vector2(0, moveSpeed) * deltaTime;
			else if (kbState.IsKeyDown(Keys.A)) position += new Vector2(-moveSpeed, 0) * deltaTime;
			else if (kbState.IsKeyDown(Keys.D)) position += new Vector2(moveSpeed, 0) * deltaTime;
		}

		public override void PostUpdate(GameTime gameTime)
		{
			
		}

		public override void Draw(GameTime gameTime)
		{
			bodySpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, position, SpriteEffects.None);
			headSpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, position + new Vector2(0, -(bodySpriteRenderer.sprite.height / 1.5f)), SpriteEffects.None);
		}
	}
}
