using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecretProject.Library.Objects
{
	public class AssetLoader
	{
		public List<Item> items;

		public AssetLoader()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));

			Console.WriteLine("AssetLoader:Item(s) output:");

			// Load XML file of Items
			using (FileStream stream = File.OpenRead("AssetLoader/Items.xml"))
			{
				var loadedItems = (List<Item>)serializer.Deserialize(stream);
				foreach(var loadedItem in loadedItems)
				{
					loadedItem.Initialize();
					items.Add(loadedItem);
				}
			}
		}
	}
}
