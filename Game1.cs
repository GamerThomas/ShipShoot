using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ShipShoot
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        PlayerSprite playerSprite;
        Sprite backgroundSprite;
        List<Missile> missileSprite = new List<Missile>();
        SpriteFont uiFont;

        Point screenSize = new Point(800,450);
        Texture2D backgroundTxr, missileTxr, saucerTxr;

        float missileTime = 2;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            saucerTxr = Content.Load<Texture2D>("saucer");
            missileTxr = Content.Load<Texture2D>("missile2");
            backgroundTxr = Content.Load<Texture2D>("backG");
            uiFont = Content.Load<SpriteFont>("uifont");

            backgroundSprite = new Sprite(backgroundTxr, new Vector2(0, 0));
            playerSprite = new PlayerSprite(saucerTxr, new Vector2(screenSize.X/6,screenSize.Y/2));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Random rng = new Random();

            if (missileTime > 0)
            {
                missileTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;


            }
            else if (missileSprite.Count < 5)
            {
                 missileSprite.Add(new Missile(missileTxr, new Vector2(screenSize.X, rng.Next(0, screenSize.Y - missileTxr.Height))));
                missileTime = (float)(rng.NextDouble() + 0.5f);
            }

            playerSprite.Update(gameTime, screenSize);

            foreach (Missile missile in missileSprite) missile.Update(gameTime, screenSize);

            missileSprite.RemoveAll(missile => missile.dead);

            base.Update(gameTime);

            Debug.WriteLine(missileSprite.Count);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();

            backgroundSprite.Draw(_spriteBatch);
             playerSprite.Draw(_spriteBatch);

            foreach (Missile missile in missileSprite) missile.Draw(_spriteBatch);

            _spriteBatch.DrawString(uiFont, "Font Test You Big Bitch!!!", new Vector2(10, 10), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
