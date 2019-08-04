using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstMonoGameProject
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		const int DISPLAY_W = 1920;
		const int DISPLAY_H = 1080;
		List<Ball> Balls = new List<Ball>();

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = DISPLAY_W;
			graphics.PreferredBackBufferHeight = DISPLAY_H;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			//// TODO: Add your initialization logic here

			Random rand = new Random();
			var ballCount = 100;
			for (int i = 0; i < ballCount; i++)
			{
				Balls.Add(
					new Ball(
						position:
							new Vector2(
								rand.Next(50, graphics.PreferredBackBufferWidth - 50),
								rand.Next(50, graphics.PreferredBackBufferHeight - 50)
							),
						acceleration: rand.Next(50, 200),
						maxSpeed: rand.Next(50, 100),
						texture: rand.Next(2) == 1 ? "ball" : "ball2",
						bounceRate: 0.9f
						)
					);
			}

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			Balls.ForEach(x => x.LoadTexture(Content));
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			var kState = Keyboard.GetState();

			// Acceleration
			if (kState.IsKeyDown(Keys.Up))
				Balls.ForEach(x => x.Up(gameTime));
			if (kState.IsKeyDown(Keys.Down))
				Balls.ForEach(x => x.Down(gameTime));
			if (kState.IsKeyDown(Keys.Left))
				Balls.ForEach(x => x.Left(gameTime));
			if (kState.IsKeyDown(Keys.Right))
				Balls.ForEach(x => x.Right(gameTime));

			Balls.ForEach(x => x.Update(gameTime, graphics));

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			spriteBatch.Begin();
			Balls.ForEach(x => x.Draw(spriteBatch));
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
