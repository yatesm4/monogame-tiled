using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.States
{
	public abstract class State
	{
		protected GraphicsDevice gd;

		protected ContentManager cm;

		public abstract void Update(GameTime gameTime);

		public abstract void PostUpdate(GameTime gameTime);

		public abstract void Draw(GameTime gameTime);

		public State(GraphicsDevice graphicsDevice, ContentManager contentManager)
		{
			gd = graphicsDevice;
			cm = contentManager;
		}
	}
}
