using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    class Background : BasicSprite
    {
        public Background(GraphicsDeviceManager graphics)
        {
            Pos = new Rectangle(0,0,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Origin = new Rectangle(0,0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public override void Update(GameTime gameTime)
        {
            Timer += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);

            if(Timer > 1)
            {
                Origin.X += 3;
                Timer = 0;
            }

            if (Origin.X > image.Width - 300)
                Origin.X = 0;
        }
    }
}
