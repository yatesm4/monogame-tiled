using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

using SecretProject.Library.Objects;

namespace SecretProject.Library.States
{
	public class MainGameState : State
	{
		protected Map currentMap;

		public Camera2D mainCamera;
		public Camera2D occludeCamera;

		protected List<Entity> entities = new List<Entity>();

		protected Player player;

		/* TEMPORARY & DEBUG FIELDS */

		protected float fps = 0;
		protected float totalTime;
		protected float displayedFps;

		protected bool firstTake = true;

		public MainGameState(GraphicsDevice graphicsDevice, ContentManager contentManager) : base(graphicsDevice, contentManager)
		{
			currentMap = new Map();

			var viewportAdapter = new BoxingViewportAdapter(GameInstance.Instance.Window, graphicsDevice, 1440 / 3, 810 / 3);
			var occludeAdapter = new BoxingViewportAdapter(GameInstance.Instance.Window, graphicsDevice, 1440 / 2, 810 / 2);
			mainCamera = new Camera2D(viewportAdapter);
			occludeCamera = new Camera2D(occludeAdapter);

			player = new Player();
			player.position = currentMap.tiles[currentMap.tiles.GetLength(0) / 2, currentMap.tiles.GetLength(1) / 2].position;
			currentMap.entities.Add(player);
			mainCamera.LookAt(player.position);
		}

		public override void Update(GameTime gameTime)
		{
			var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

			var elapsedMilisecs = (float) gameTime.ElapsedGameTime.TotalMilliseconds;
			totalTime += elapsedMilisecs;
			if (totalTime >= 1000)
			{
				displayedFps = fps;
				fps = 0;
				totalTime = 0;
			}
			fps++;
			
			currentMap.Update(gameTime);
		}

		public override void PostUpdate(GameTime gameTime)
		{
			currentMap.PostUpdate(gameTime);

			mainCamera.LookAt(player.position);
			occludeCamera.LookAt(player.position);
		}

		public override void Draw(GameTime gameTime)
		{
			GameInstance.Instance.spriteBatch.Begin(transformMatrix: mainCamera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

			currentMap.Draw(gameTime, occludeCamera);

			GameInstance.Instance.spriteBatch.End();

			// DEBUGGING

			GameInstance.Instance.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"System Debugging:", new Vector2(10, 10), Color.DarkMagenta);
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"{displayedFps} FPS", new Vector2(10, 30), Color.Magenta);

			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"Player Debugging:", new Vector2(10, 70), Color.DarkMagenta);
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"X: {(int)player.position.X} || Y: {(int)player.position.Y}", new Vector2(10, 90), Color.Magenta);
			
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"Input Debugging:", new Vector2(10, 120), Color.DarkMagenta);
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"Cursor (Screen) X: {(int)Mouse.GetState().Position.X} || Y: {(int)Mouse.GetState().Position.Y}", new Vector2(10, 140), Color.Magenta);
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"Cursor (World) X: {(int)mainCamera.ScreenToWorld(Mouse.GetState().Position.X, Mouse.GetState().Position.Y).X} || Y: {(int)mainCamera.ScreenToWorld(Mouse.GetState().Position.X, Mouse.GetState().Position.Y).Y}", new Vector2(10, 160), Color.Magenta);

			GameInstance.Instance.spriteBatch.DrawString(
				GameInstance.Instance.mainFont,
				$"{(int)player.position.X}, {(int)player.position.Y}",
				mainCamera.WorldToScreen(player.position.X,player.position.Y),
				Color.DarkMagenta
			);

			GameInstance.Instance.spriteBatch.End();
		}
	}
}
