using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SecretProject.Library.Objects
{
	public abstract class Entity
	{
		protected SpriteRenderer headSpriteRenderer;
		protected SpriteRenderer bodySpriteRenderer;

		protected Sprite headSprites;
		protected Sprite southWestBodySprites;
		protected Sprite northWestBodySprites;
		protected Sprite southEastBodySprites;
		protected Sprite northEastBodySprites;

		public Vector2 position;

		protected float moveSpeed;

		public Entity()
		{
			InitializeEntity();
		}

		protected abstract void InitializeEntity();

		public abstract void Update(GameTime gameTime);

		public abstract void PostUpdate(GameTime gameTime);

		public abstract void Draw(GameTime gameTime);
	}
}
