using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMonoGameProject
{
	public class Ball
	{
		private Vector2 Position { get; set; }
		private Vector2 Velocity { get; set; }
		private string TextureName { get; set; }
		private Texture2D Texture { get; set; }
		private float MaxSpeed { get; set; }
		private float Acceleration { get; set; }
		private float JumpHeight { get; set; }
		private float BounceRate { get; set; }

		public Ball(
			Vector2? position = null,
			Vector2? velocity = null,
			string texture = "ball",
			float? maxSpeed = null,
			float? acceleration = null,
			float? jumpHeight = null,
			float? bounceRate = null,
			GraphicsDeviceManager graphics = null
		)
		{
			this.Position = position ?? new Vector2(0, 0);
			this.Velocity = velocity ?? new Vector2(0, 0);
			this.TextureName = texture;
			this.MaxSpeed = maxSpeed ?? 50f;
			this.Acceleration = acceleration ?? 50f;
			this.JumpHeight = jumpHeight ?? 50f;
			this.BounceRate = bounceRate ?? 1.3f;
			if (graphics != null)
				this.Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
		}

		public void Up(GameTime gameTime)
		{
			Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - (this.Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds));
		}

		public void Down(GameTime gameTime)
		{
			Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + (this.Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds));
		}

		public void Left(GameTime gameTime)
		{
			Velocity = new Vector2(this.Velocity.X - (this.Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds), this.Velocity.Y);
		}

		public void Right(GameTime gameTime)
		{
			Velocity = new Vector2(this.Velocity.X + (this.Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds), this.Velocity.Y);
		}

		public void LoadTexture(ContentManager content)
		{
			this.Texture = content.Load<Texture2D>(this.TextureName);
		}

		public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
		{
			// Update Velocity
			if (Velocity.Y > 0)
				Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - ((Acceleration / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds));
			if (Velocity.Y < 0)
				Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + ((Acceleration / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds));
			if (Velocity.X > 0)
				Velocity = new Vector2(this.Velocity.X - ((Acceleration / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds), this.Velocity.Y);
			if (Velocity.X < 0)
				Velocity = new Vector2(this.Velocity.X + ((Acceleration / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds), this.Velocity.Y);


			// Update Position
			this.Position = new Vector2(this.Position.X + this.Velocity.X, this.Position.Y + this.Velocity.Y);

			// Restrict Velocity
			if (this.Position.X <= this.Texture.Width / 2 || this.Position.X >= graphics.PreferredBackBufferWidth - (this.Texture.Width / 2))
				this.Velocity = new Vector2(-this.Velocity.X / this.BounceRate, this.Velocity.Y);
			if (this.Position.Y <= this.Texture.Height / 2 || this.Position.Y >= graphics.PreferredBackBufferHeight - (this.Texture.Height / 2))
				this.Velocity = new Vector2(this.Velocity.X, -this.Velocity.Y / this.BounceRate);

			// Restrict Position
			this.Position =
				new Vector2(
					Math.Min(
						Math.Max(this.Texture.Width / 2, this.Position.X),
						graphics.PreferredBackBufferWidth - this.Texture.Width / 2
					),
					Math.Min(
						Math.Max(this.Texture.Height / 2, this.Position.Y),
						graphics.PreferredBackBufferHeight - this.Texture.Height / 2
					)
				);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this.Texture, this.Position, null, Color.White, 0f, new Vector2(this.Texture.Width / 2, this.Texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
		}
	}
}
