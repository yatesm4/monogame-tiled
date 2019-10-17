using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public abstract class Character : Entity
	{
		protected SpriteRenderer headSpriteRenderer;
		protected SpriteRenderer faceSpriteRenderer;
		protected SpriteRenderer hairSpriteRenderer;
		protected SpriteRenderer headAccessoriesSpriteRenderer;
		protected SpriteRenderer bodySpriteRenderer;

		protected Sprite headSprites;
		protected Sprite faceSprites;
		protected Sprite hairSprites;
		protected Sprite headAccessoriesSprites;
		protected Sprite southWestBodySprites;
		protected Sprite northWestBodySprites;
		protected Sprite southEastBodySprites;
		protected Sprite northEastBodySprites;

		protected float moveSpeed;

		public Character()
		{
			InitializeEntity();
		}

		protected override void InitializeEntity()
		{
			collider = new Collider();
			collider.dimensions = new Vector2(
				bodySpriteRenderer.sprite.width,
				bodySpriteRenderer.sprite.height * 0.4f
			);
		}
	}
}
