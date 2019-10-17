using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.IO;

namespace SecretProject.Library.Objects
{
	public static class Noise
	{
		public static Color[,] CreateStaticMap(int dimension)
		{
			Random rand = new Random();
			Color[,] noisyColors = new Color[dimension, dimension];

			for (int x = 0; x < dimension; x++)
				for (int y = 0; y < dimension; y++)
					noisyColors[x, y] = new Color(new Vector3((float)rand.Next(1000) / 1000.0f, 0, 0));

			return noisyColors;
		}
	}
}
