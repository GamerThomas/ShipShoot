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
        SpriteFont bigFont;

        List<particleSprite> particleList = new List<particleSprite>();

        Point screenSize = new Point(800,450);
        Texture2D backgroundTxr, missileTxr, saucerTxr,particleTxr;

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
            bigFont = Content.Load<SpriteFont>("bigFont");
            particleTxr = Content.Load<Texture2D>("particle");

            backgroundSprite = new Sprite(backgroundTxr, new Vector2(0, 0));
            playerSprite = new PlayerSprite(saucerTxr, new Vector2(screenSize.X/6,screenSize.Y/2));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Random rng = new Random();

            if (playerSprite.playerLives >0 && missileTime > 0)
            {
                missileTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;


            }
            else if (playerSprite.playerLives > 0 && missileSprite.Count < 5)
            {
                missileSprite.Add(new Missile(missileTxr, new Vector2(screenSize.X, rng.Next(0, screenSize.Y - missileTxr.Height))));
                missileTime = (float)(rng.NextDouble() + 0.5f);
            }

            playerSprite.Update(gameTime, screenSize);

            foreach (Missile missile in missileSprite)
            {
                 missile.Update(gameTime, screenSize);
                if (playerSprite.playerLives > 0 && playerSprite.IsColliding(missile))
                {
                    for(int i = 0; i < 16; i++)particleList.Add(new particleSprite(particleTxr, new Vector2(missile.spritePos.X + (missileTxr.Width / 2) - (particleTxr.Width / 2),missile.spritePos.Y + (missileTxr.Height /2 )- (particleTxr.Height / 2 ))));
                    missile.dead = true;
                    playerSprite.playerLives--;
                }
            }

            foreach (particleSprite particle in particleList) particle.Update(gameTime, screenSize);

            missileSprite.RemoveAll(missile => missile.dead);
            particleList.RemoveAll(particle => particle.currentLife <= 0);

            base.Update(gameTime);

            Debug.WriteLine(missileSprite.Count);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();

            backgroundSprite.Draw(_spriteBatch);
            if (playerSprite.playerLives > 0) playerSprite.Draw(_spriteBatch);

            foreach (Missile missile in missileSprite) missile.Draw(_spriteBatch);
            foreach (particleSprite particle in particleList) particle.Draw(_spriteBatch);

            _spriteBatch.DrawString(uiFont, "Lives: "+playerSprite.playerLives, new Vector2(12, 12), Color.Black);

            _spriteBatch.DrawString(uiFont, "Lives: " + playerSprite.playerLives, new Vector2(10, 10), Color.White);

            if (playerSprite.playerLives <= 0)
            {
                Vector2 textSize = bigFont.MeasureString("Game OVER");
                _spriteBatch.DrawString(bigFont, "GAME OVER", new Vector2((screenSize.X/2)- (textSize.X/2) +4,(screenSize.Y/2) -(textSize.Y/2) + 4), Color.Black);
                _spriteBatch.DrawString(bigFont, "GAME OVER", new Vector2((screenSize.X / 2) - (textSize.X / 2), (screenSize.Y / 2) - (textSize.Y / 2)), Color.Red);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
