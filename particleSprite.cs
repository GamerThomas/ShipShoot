﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices;

namespace ShipShoot
{
    class particleSprite : Sprite
    {
        Random rando = new Random();
        Vector2 velocity;
        public float maxLife,currentLife;
        Color particleColor;
        
        public particleSprite(Texture2D newTxr, Vector2 newPos) : base(newTxr, newPos) 
        {

            maxLife = (float)(rando.NextDouble() + 2);
            currentLife = maxLife;
            
            velocity = new Vector2((float)(rando.NextDouble()* 100 + 50),(float)(rando.NextDouble() * 100 + 50));

            if (rando.Next(2) > 0) velocity.X *= -1;
            if (rando.Next(2) > 0) velocity.Y *= -1;

            particleColor = new Color((float)(rando.NextDouble() / 2 + 0.5), (float)(rando.NextDouble() / 2 + 0.5), 0f);
        }

        public override void Update(GameTime gameTime, Point screenSize)
        {
            spritePos += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentLife -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public new void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(spriteTexture, new Rectangle((int)spritePos.X, (int)spritePos.Y, (int)(spriteTexture.Width * (currentLife / maxLife)),(int)(spriteTexture.Height * (currentLife / maxLife))),particleColor);
            
        }
    }
}
