using MonoGame.Extended.Tiled;
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
	}
}
