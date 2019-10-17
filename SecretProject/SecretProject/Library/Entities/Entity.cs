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
		public Vector2 position;

		public Vector2 previousPosition;

		public Collider collider;

		protected abstract void InitializeEntity();

		public abstract void Update(GameTime gameTime);

		public virtual void PostUpdate(GameTime gameTime, List<Entity> entities)
		{
			HandleCollisions(entities);
		}

		public virtual void HandleCollisions(List<Entity> entities)
		{
			collider.position = position;
		}

		public virtual void Draw(GameTime gameTime)
		{
			collider.DrawBounds();
		}
	}
}
