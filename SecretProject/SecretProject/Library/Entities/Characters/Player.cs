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
	public class Player : Character
	{
		protected static Player instance = null;
		protected static readonly object instancePadlock = new object();

		public static Player Instance
		{
			get
			{
				lock(instancePadlock)
				{
					if (instance != null) return instance;
					else throw new Exception("No Player Instance available!");
				}
			}
		}

		protected Vector2 movementInput;

		public bool isMoving => movementInput != Vector2.Zero;

		protected bool lastMovedLeft = true;
		protected bool lastMovedNorth = false;

		protected List<Entity> knownEntities = new List<Entity>();

		protected override void InitializeEntity()
		{
			instance = this;

			headSprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Head/00"));
			headSprites.isSpriteSheet = true;

			faceSprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Face/00"));
			faceSprites.isSpriteSheet = true;

			hairSprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Hair/00"));
			hairSprites.isSpriteSheet = true;

			headAccessoriesSprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/HeadAccessories/00"));
			headAccessoriesSprites.isSpriteSheet = true;

			southEastBodySprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Body/02_SouthEast"), 0.15f);
			northEastBodySprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Body/02_NorthEast"), 0.15f);
			southWestBodySprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Body/02_SouthWest"), 0.15f);
			northWestBodySprites = new Sprite(GameInstance.Instance.Content.Load<Texture2D>("Sprites/Characters/Body/02_NorthWest"), 0.15f);

			headSpriteRenderer.LoadSprite(headSprites, 2);
			faceSpriteRenderer.LoadSprite(faceSprites, 2);
			hairSpriteRenderer.LoadSprite(hairSprites, 2);
			headAccessoriesSpriteRenderer.LoadSprite(headAccessoriesSprites, 2);
			bodySpriteRenderer.LoadSprite(southWestBodySprites);

			headSpriteRenderer.layerDepth = 1;
			faceSpriteRenderer.layerDepth = 1;
			hairSpriteRenderer.layerDepth = 1;
			headAccessoriesSpriteRenderer.layerDepth = 1;
			bodySpriteRenderer.layerDepth = 1;

			moveSpeed = 100;

			base.InitializeEntity();
		}

		public override void Update(GameTime gameTime)
		{
			var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

			HandleInput(deltaTime);
		}

		protected void HandleInput(float deltaTime)
		{
			var kbState = Keyboard.GetState();

			movementInput = new Vector2();

			// Check for horizontal movement input
			if (kbState.IsKeyDown(Keys.A))
			{
				movementInput.X = -moveSpeed;
				lastMovedLeft = true;
			}
			else if (kbState.IsKeyDown(Keys.D))
			{
				movementInput.X = moveSpeed;
				lastMovedLeft = false;
			}

			// Check for vertical movement input
			if (kbState.IsKeyDown(Keys.W))
			{
				movementInput.Y = -(moveSpeed * 0.5f);
				lastMovedNorth = true;
			}
			else if (kbState.IsKeyDown(Keys.S))
			{
				movementInput.Y = (moveSpeed * 0.5f);
				lastMovedNorth = false;
			}

			previousPosition = position;

			var newPositionInput = (movementInput * deltaTime);

			var positionValid = true;

			foreach(var e in knownEntities)
			{
				if (e == this) continue;

				var colliderPos = collider.bounds.Position + newPositionInput;
				if (e.collider.bounds.Intersects(new MonoGame.Extended.RectangleF((int)(colliderPos).X, (int)(colliderPos).Y, collider.bounds.Width, collider.bounds.Height)))
				{
					positionValid = false;
					break;
				}
			}

			if (positionValid) position += newPositionInput;
			else movementInput = new Vector2();

			DetermineSprite();

			if (isMoving && bodySpriteRenderer.sprite.animIsPaused)
			{
				bodySpriteRenderer.PlayAnimatedSprite(true);
			}
			else if (!isMoving && !bodySpriteRenderer.sprite.animIsPaused)
			{
				bodySpriteRenderer.PauseAnimatedSprite();
				bodySpriteRenderer.sprite.animFrameIndex = 0;
			}
		}

		public override void HandleCollisions(List<Entity> entities)
		{
			collider.position = position;

			knownEntities = entities;
		}

		public void CollidedWithProp(Prop p)
		{
		}

		public void PickupItem(Item i)
		{
			Console.WriteLine("Picked up item");
		}

		public override void Draw(GameTime gameTime)
		{
			var headPos = position + new Vector2(0, -(bodySpriteRenderer.sprite.height / 1.45f));

			if (lastMovedNorth)
			{
				headSpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, headPos, SpriteEffects.None);
				faceSpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, headPos, SpriteEffects.None);
				headAccessoriesSpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, headPos, SpriteEffects.None);
				bodySpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, position, SpriteEffects.None, isMoving ? 0 : -1);
			}
			else
			{
				bodySpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, position, SpriteEffects.None, isMoving ? 0 : -1);
				headSpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, headPos, SpriteEffects.None);
				faceSpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, headPos, SpriteEffects.None);
				headAccessoriesSpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, headPos, SpriteEffects.None);
			}

			hairSpriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, headPos, SpriteEffects.None);

			base.Draw(gameTime);
		}

		public void DetermineSprite()
		{
			var headFrameIndex = 0;
			var bodySprite = bodySpriteRenderer.sprite;

			if (lastMovedLeft)
			{
				if (lastMovedNorth)
				{
					headFrameIndex = 3;
					bodySprite = northWestBodySprites;
				}
				else
				{
					headFrameIndex = 2;
					bodySprite = southWestBodySprites;
				}
			}
			else
			{
				if (lastMovedNorth)
				{
					headFrameIndex = 0;
					bodySprite = northEastBodySprites;
				}
				else
				{
					headFrameIndex = 1;
					bodySprite = southEastBodySprites;
				}
			}

			headSpriteRenderer.sprite.animFrameIndex = headFrameIndex;
			faceSpriteRenderer.sprite.animFrameIndex = headFrameIndex;
			hairSpriteRenderer.sprite.animFrameIndex = headFrameIndex;
			headAccessoriesSpriteRenderer.sprite.animFrameIndex = headFrameIndex;
			bodySpriteRenderer.LoadSprite(bodySprite);
		}
	}
}
