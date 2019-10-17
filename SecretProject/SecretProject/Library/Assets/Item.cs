using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library.Objects
{
	public class Item
	{
		public string name;

		public string textureName;

		public int sellValue = 1;

		public int stackLimit = 99;

		[System.Xml.Serialization.XmlIgnore]
		public Sprite iconSprite;

		public void Initialize()
		{
			iconSprite = new Sprite(GameInstance.Instance.Content.Load<Texture2D>($"Sprites/Items/{textureName}"));
		}
	}
}
