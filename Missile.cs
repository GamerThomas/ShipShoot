using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShipShoot
{
    class Missile : Sprite
    {

        float currentSpeed = 20f;
        float maxSpeed = 1000f;
        float acceleration = 200f;

        public bool dead = false;
        public Missile(Texture2D newTxr, Vector2 newPos , float newMaxspeed = 1000f) : base(newTxr, newPos)
        {
            maxSpeed = newMaxspeed;
        }

        public override void Update(GameTime gameTime, Point screenSize)
        {

            currentSpeed += acceleration *(float)gameTime.ElapsedGameTime.TotalSeconds;
            currentSpeed = Math.Min(currentSpeed, maxSpeed);
            spritePos.X -= currentSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (spritePos.X < -spriteTexture.Width) dead = true;
        }
    }
}
