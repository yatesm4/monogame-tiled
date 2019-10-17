using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SecretProject.Library.Objects.Props
{
	public class Tree : Prop
	{
		protected PickupItem[] grownPickupItemInstances;

		public Tree(string textureName) : base(textureName)
		{
			// Do extra construction for tree here
		}

		protected override void InitializeEntity()
		{
			collider = new Collider();

			collider.dimensions = new Vector2(spriteRenderer.sprite.width / 3, 8);
			collider.positionOffset = new Vector2(
				spriteRenderer.sprite.width / 3,
				-(spriteRenderer.sprite.height / 8)
			);

			// Initialize dropped PickupItems

			grownPickupItemInstances = new PickupItem[]
			{
				new PickupItem(GameInstance.Instance.assets.items.Find(i => i.name.Equals("Apple"))),
				new PickupItem(GameInstance.Instance.assets.items.Find(i => i.name.Equals("Apple"))),
				new PickupItem(GameInstance.Instance.assets.items.Find(i => i.name.Equals("Apple")))
			};

			grownPickupItemInstances[0].position = position;
			grownPickupItemInstances[1].position = position + new Vector2(8,0);
			grownPickupItemInstances[2].position = position + new Vector2(16,0);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (grownPickupItemInstances != null)
			{
				foreach(var i in grownPickupItemInstances)
				{
					i.Update(gameTime);
				}
			}
		}

		public override void PostUpdate(GameTime gameTime, List<Entity> entities)
		{
			base.PostUpdate(gameTime, entities);

			if (grownPickupItemInstances != null)
			{
				foreach(var i in grownPickupItemInstances)
				{
					i.PostUpdate(gameTime, entities);
				}
			}
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			if (grownPickupItemInstances != null)
			{
				foreach(var i in grownPickupItemInstances)
				{
					i.Draw(gameTime);
				}
			}
		}
	}
}
