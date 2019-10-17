using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public class Sprite
	{
		public Texture2D texture;

		public int width => isSpriteSheet ? texture.Height : texture.Width;

		public int height => texture.Height;

		public bool isSpriteSheet = false;

		public int animFrameCount => isSpriteSheet ? texture.Width / width : 0;

		public float animFrameTime = 1f;

		public float animPlayTime;

		public int animFrameIndex;

		public bool animIsLooping = true;

		public bool animIsPaused = true;

		public Sprite(Texture2D txtr)
		{
			texture = txtr;
		}

		public Sprite(Texture2D txtr, float time = 1f)
		{
			texture = txtr;

			isSpriteSheet = true;
			animFrameTime = time;
		}
	}
}
