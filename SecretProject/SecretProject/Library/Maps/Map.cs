using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using MonoGame.Extended;

namespace SecretProject.Library.Objects
{
	public class Map
	{
		public Tile[,] tiles;

		public List<Entity> entities = new List<Entity>();

		public Map()
		{
			tiles = MapBuilder.GenerateBaseMap(out entities);
		}

		public void Update(GameTime gameTime, Camera2D occlusionCam = null)
		{
			foreach(var e in entities)
			{
				e.Update(gameTime);
			}
		}

		public void PostUpdate(GameTime gameTime, Camera2D occlusionCam = null)
		{
			foreach(var e in entities)
			{
				e.PostUpdate(gameTime, entities);
			}
		}

		public void Draw(GameTime gameTime, Camera2D occlusionCam = null)
		{
			for (int x = 0; x < tiles.GetLength(0); x++)
			{
				for (int y = 0; y < tiles.GetLength(1); y++)
				{
					if (occlusionCam != null)
					{
						var occlude = occlusionCam.Contains(tiles[x, y].bounds);
						switch (occlude)
						{
							case ContainmentType.Intersects:
							case ContainmentType.Contains:
								tiles[x, y].Draw(gameTime);
								break;
						}
					}
					else tiles[x, y].Draw(gameTime);
				}
			}

			var sortedEntities = entities.OrderBy(e => e.collider.bounds.Bottom).ToList();

			foreach(var e in sortedEntities)
			{
				if (occlusionCam != null)
				{
					var occlude = occlusionCam.Contains(e.collider.bounds.Center);
					switch (occlude)
					{
						case ContainmentType.Intersects:
						case ContainmentType.Contains:
							e.Draw(gameTime);
							break;
					}
				}
				else e.Draw(gameTime);
			}
		}
	}
}
