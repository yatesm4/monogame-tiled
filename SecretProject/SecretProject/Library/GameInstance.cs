using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SecretProject.Library.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretProject.Library
{
	public class GameInstance : Game
	{
		protected GraphicsDeviceManager gdm;
		public GraphicsDeviceManager graphicsDeviceManager => gdm;

		protected SpriteBatch sb;
		public SpriteBatch spriteBatch => sb;

		protected static GameInstance instance = null;
		protected static readonly object instancePadlock = new object();

		protected MouseState currentMs;
		protected MouseState previousMs;

		protected State currentState;
		protected State nextState;

		public SpriteFont mainFont;


		public GameInstance()
		{
			gdm = new GraphicsDeviceManager(this)
			{
				// IsFullScreen = true,
				PreferredBackBufferWidth = 1440,
				PreferredBackBufferHeight = 810,
			};

			Content.RootDirectory = "Content";

			instance = this;
		}

		public static GameInstance Instance
		{
			get
			{
				lock (instancePadlock)
				{
					if (instance != null) return instance;
					else throw new Exception("No GameInstance available!");
				}
			}
		}

		protected override void Initialize()
		{
			// Uncomment for mouse visibility
			IsMouseVisible = true;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			sb = new SpriteBatch(GraphicsDevice);

			currentState = new SplashScreenState(GraphicsDevice, Content);

			mainFont = Content.Load<SpriteFont>("Fonts/Font_01");
		}

		protected override void UnloadContent()
		{
			// TODO Unload non-ContentManager here
		}

		protected override void Update(GameTime gameTime)
		{
			previousMs = currentMs;
			currentMs = Mouse.GetState();

			if (currentMs.LeftButton == ButtonState.Released && previousMs.LeftButton == ButtonState.Pressed)
			{
				// make mouse click sound here
			}

			if (nextState != null)
			{
				currentState = nextState;
				nextState = null;
			}

			currentState.Update(gameTime);
			currentState.PostUpdate(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			currentState.Draw(gameTime);

			base.Draw(gameTime);
		}

		public void ChangeState(State state)
		{
			nextState = state;
		}
	}
}
