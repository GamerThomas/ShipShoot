using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShipShoot
{
    class Missile : Sprite
    {

        float missileSpeed = 5;
        public bool dead = false;
        public Missile(Texture2D newTxr, Vector2 newPos) : base(newTxr, newPos)
        {

        }

        public override void Update(GameTime gameTime, Point screenSize)
        {
            spritePos.X -= missileSpeed;

            if (spritePos.X < -spriteTexture.Width) dead = true;
        }
    }
}
