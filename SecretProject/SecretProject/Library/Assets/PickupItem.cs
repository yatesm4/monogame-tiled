using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SecretProject.Library.Objects
{
	public class PickupItem : Entity
	{
		public Item item;

		public SpriteRenderer spriteRenderer;

		public bool pickupEnabled = false;

		public PickupItem(Item i)
		{
			item = i;

			InitializeEntity();
		}

		protected override void InitializeEntity()
		{
			spriteRenderer.LoadSprite(item.iconSprite);

			collider = new Collider();
			collider.dimensions = new Vector2(spriteRenderer.sprite.width, spriteRenderer.sprite.height);
		}

		public override void Update(GameTime gameTime)
		{
		}

		public override void HandleCollisions(List<Entity> entities)
		{
			base.HandleCollisions(entities);

			Player player = Player.Instance;

			if (collider.bounds.Intersects(player.collider.bounds))
			{
				player.PickupItem(item);
			}
		}

		public override void Draw(GameTime gameTime)
		{
			spriteRenderer.Draw(gameTime, GameInstance.Instance.spriteBatch, position, SpriteEffects.None);

			base.Draw(gameTime);
		}
	}
}
