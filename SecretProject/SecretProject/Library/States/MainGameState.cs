using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended.ViewportAdapters;

using SecretProject.Library.Objects;

namespace SecretProject.Library.States
{
	public class MainGameState : State
	{
		protected List<TiledMap> gameMaps = new List<TiledMap>();

		protected TiledMapRenderer gameMapRenderer;

		protected Camera2D mainCamera;

		protected List<Entity> entities = new List<Entity>();

		/* TEMPORARY & DEBUG FIELDS */

		protected float fps = 0;
		protected float totalTime;
		protected float displayedFps;

		protected DefaultViewportAdapter viewportAdapter;

		protected bool firstTake = true;

		public MainGameState(GraphicsDevice graphicsDevice, ContentManager contentManager) : base(graphicsDevice, contentManager)
		{
			gameMaps.Add(MapBuilder.LoadTiledMap("TestMap"));

			gameMapRenderer = new TiledMapRenderer(graphicsDevice);

			var viewportAdapter = new BoxingViewportAdapter(GameInstance.Instance.Window, graphicsDevice, 1440, 810);

			mainCamera = new Camera2D(viewportAdapter);

			var player = new Player();
			var playerStartPos = new Vector2();

			foreach(var obj in gameMaps[0].ObjectLayers[0].Objects)
			{
				if (obj.Name.Equals("PlayerStart"))
				{
					playerStartPos = obj.Position;
					break;
				}
			}

			player.position = playerStartPos;

			entities.Add(player);

			mainCamera.LookAt(player.position);
		}

		public override void Update(GameTime gameTime)
		{
			if (!gameMaps.Any()) return;

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
			
			gameMapRenderer.Update(gameMaps[0], gameTime);

			foreach(var e in entities) e.Update(gameTime);
		}

		public override void PostUpdate(GameTime gameTime)
		{
			if (!gameMaps.Any()) return;

			foreach(var e in entities) e.PostUpdate(gameTime);

			mainCamera.LookAt(entities[0].position);
		}

		public override void Draw(GameTime gameTime)
		{
			if (!gameMaps.Any()) return;

			GameInstance.Instance.spriteBatch.Begin(transformMatrix: mainCamera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

			gameMapRenderer.Draw(gameMaps[0], mainCamera.GetViewMatrix());

			foreach(var e in entities) e.Draw(gameTime);

			GameInstance.Instance.spriteBatch.End();

			// DEBUGGING

			GameInstance.Instance.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"{displayedFps} FPS", new Vector2(10, 10), Color.OrangeRed);

			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"Player Debugging:", new Vector2(10, 50), Color.OrangeRed);
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"X: {(int)entities[0].position.X} || Y: {(int)entities[0].position.Y}", new Vector2(10, 70), Color.NavajoWhite);
			
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"Input Debugging:", new Vector2(10, 100), Color.OrangeRed);
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"Cursor (Screen) X: {(int)Mouse.GetState().Position.X} || Y: {(int)Mouse.GetState().Position.Y}", new Vector2(10, 120), Color.NavajoWhite);
			GameInstance.Instance.spriteBatch.DrawString(GameInstance.Instance.mainFont, $"Cursor (World) X: {(int)mainCamera.ScreenToWorld(Mouse.GetState().Position.X, Mouse.GetState().Position.Y).X} || Y: {(int)mainCamera.ScreenToWorld(Mouse.GetState().Position.X, Mouse.GetState().Position.Y).Y}", new Vector2(10, 140), Color.NavajoWhite);

			GameInstance.Instance.spriteBatch.End();
		}
	}
}
