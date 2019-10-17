using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using SecretProject.Library.Objects.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public class MapBuilder
	{
		protected static readonly string tiledMapDirectory = "TiledMaps";

		public static TiledMap LoadTiledMap(string mapName)
		{
			return GameInstance.Instance.Content.Load<TiledMap>($"{tiledMapDirectory}/{mapName}");
		}

		// Revised Map Building

		protected static readonly int mapDimensions = 100;

		protected static readonly float tileScale = 1f;

		protected static readonly Vector2 tileDimensions = new Vector2(64, 32);

		public static Tile[,] GenerateBaseMap(out List<Entity> entities)
		{
			Tile[,] map = new Tile[mapDimensions, mapDimensions];

			for (int i = 0; i < mapDimensions; i++)
			{
				for (int j = 0; j < mapDimensions; j++)
				{
					string tileName = "Floor/Grass";

					var z = mapDimensions / 2;
					var x = z - 1;
					var y = z + 1;
					var v = x - 1;
					var w = y + 1;

					if ((i == z || i == x || i == y || i == v || i == w) && (j == z || j == x || j == y || j == v || j == w)) tileName = "Floor/Asphalt";
					else if (j == x || j == y) tileName = "Floor/Asphalt";
					else if (j == v || j == w) tileName = "Floor/Cement";
					else if (j == z) tileName = "Floor/AsphaltRoadFlipped";
					else if (i == x || i == y) tileName = "Floor/Asphalt";
					else if (i == v || i == w) tileName = "Floor/Cement";
					else if (i == z) tileName = "Floor/AsphaltRoad";

					if ((i == v || i == w) && (j == w || j == v)) tileName = "Floor/Cement";

					map[i,j] = new Tile
					(
						tileName,
						new Vector2(i, j),
						GetTilePositionFromIndex(new Vector2(i, j))
					);

					map[i,j].free = (tileName.Equals("Floor/Grass"));
				}
			}

			var entityList = new List<Entity>();

			var resourceNoiseMap = Noise.CreateStaticMap(mapDimensions);
			var treeTileName = "Tree";

			for (int i = 0; i < resourceNoiseMap.GetLength(0); i++)
			{
				for (int j = 0; j < resourceNoiseMap.GetLength(0); j++)
				{
					if (resourceNoiseMap[i, j].R < 30)
					{
						if (map[i, j].free)
						{
							var tree = new Tree(treeTileName);
							tree.position = GetTilePositionFromIndex(new Vector2(i, j));
							entityList.Add(tree);
						}
					}
				}
			}

			entities = entityList;
			return map;
		}

		public static Vector2 GetTilePositionFromIndex (Vector2 index)
		{
			return new Vector2
			(
				index.X * ((tileDimensions.X / 2) * tileScale) - index.Y * ((tileDimensions.X / 2) * tileScale),
				index.X * ((tileDimensions.Y / 2) * tileScale) + index.Y * ((tileDimensions.Y / 2) * tileScale)
			);
		}
	}
}
