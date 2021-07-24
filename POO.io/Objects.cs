using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    class Objects : BasicAnimatedSprite
    {
        int Radio = 1500, timer = 0, timer2 = 0;
        protected Rectangle OriginPos;
        public bool Devil = false;
        Point Velocity;

        public bool Damage = false;
        public Objects()
        {

        }

        public void Direction(int x, int y)
        {
            Velocity.X = x;
            Velocity.Y = y;
        }

        public override void Update(GameTime gameTime)
        {
            Pos.X += Velocity.X;
            Pos.Y += Velocity.Y;

            base.Update(gameTime);
        }

        public Rectangle GetOriginPos()
        {
            return OriginPos;
        }

        public void Follow(Rectangle HeroPos, GameTime gameTime)
        {
            timer += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            timer2 += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            if ((HeroPos.X - Pos.X < Radio && HeroPos.X - Pos.X > -Radio) && (HeroPos.Y - Pos.Y < Radio && HeroPos.Y - Pos.Y > -Radio))
            {
                Velocity.Y = 0;
                Velocity.X = 0;
                if (HeroPos.X - Pos.X < Radio && HeroPos.X - Pos.X > -Radio)
                {
                    if (HeroPos.X - Pos.X > 0)
                    {
                        Velocity.X = 3;
                    }
                    if (HeroPos.X - Pos.X < 0)
                    {
                        Velocity.X = -3;
                    }
                }
                if (HeroPos.Y - Pos.Y < Radio && HeroPos.Y - Pos.Y > -Radio)
                {

                    if (HeroPos.Y - Pos.Y > 0)
                    {
                        Velocity.Y = 3;
                    }
                    if (HeroPos.Y - Pos.Y < 0)
                    {
                        Velocity.Y = -3;
                    }
                }
            }

            if (timer >= 10)
            {
                Pos.X += Velocity.X;
                Pos.Y += Velocity.Y;
                timer = 0;
            }
            base.Update(gameTime);
        }

        public bool Checkcollision<T>(T Object) where T : IBasicCollision
        {
            if(Intersects(Object.GetRect()))
            {
                if(Object.GetType() == typeof(Shot))
                {
                    SetColor(Color.Red);
                    return true;
                }
            }
            return false;
        }
    }
}
