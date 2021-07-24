using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    class Manual : AnimatedCharacter
    {
        public Keys Upk, Downk, Leftk, Rightk;
        public GraphicsDeviceManager graphics;
        double tiempoFrame;
        public Manual(Rectangle Pos, Color color, Keys Up, Keys Down, Keys Left, Keys Right, GraphicsDeviceManager graphics, double timeframe) : base(Pos, color,timeframe)
        {
            this.tiempoFrame = timeframe;
            Upk = Up;
            Downk = Down;
            Leftk = Left;
            Rightk = Right;
            this.graphics = graphics;

        }

        public override void Update(GameTime gameTime)
        {
            Velocity.X = 0;
            Velocity.Y = 0;
            if (Direction == Estado.DOWN)
            {
                Direction = Estado.DOWNS;
            }
            else if (Direction == Estado.RIGHT)
            {
                Direction = Estado.RIGHTS;
            }
            else if (Direction == Estado.LEFT)
            {
                Direction = Estado.LEFTS;
            }
            else if (Direction == Estado.UP)
            {
                Direction = Estado.UPS;
            }
            else
            {
                Direction = Estado.STAND;
            }

            if (Keyboard.GetState().IsKeyDown(Upk))
            {
                Up.Update(gameTime);
                //if (bounds != Bounds.UP)
                {
                    Velocity.Y = -4;
                }
                if (GlobalPosition.Y - Velocity.Y > 0)
                    GlobalPosition.Y += Velocity.Y;
                if (STATUS[0] == 1)
                    Direction = Estado.UP;

            }

            if (Keyboard.GetState().IsKeyDown(Downk))
            {
                Down.Update(gameTime);
                //if (bounds != Bounds.DOWN)
                {
                    Velocity.Y = 4;
                }
                if (GlobalPosition.Y + Velocity.Y < graphics.PreferredBackBufferHeight - GlobalPosition.Height)
                    GlobalPosition.Y += Velocity.Y;

                if (STATUS[2] == 1)
                    Direction = Estado.DOWN;
            }

            if (Keyboard.GetState().IsKeyDown(Leftk))
            {
                Left.Update(gameTime);
                //if (bounds != Bounds.LEFT)
                {
                    Velocity.X = -2;
                }
                if (GlobalPosition.X - Velocity.X > 0)
                    GlobalPosition.X += Velocity.X;

                if (STATUS[4] == 1)
                    Direction = Estado.LEFT;
            }

            if (Keyboard.GetState().IsKeyDown(Rightk))
            {
                Right.Update(gameTime);
                //if (bounds != Bounds.RIGHT)
                {
                    Velocity.X = 4;
                }
                if (GlobalPosition.X + Velocity.X < graphics.PreferredBackBufferWidth - GlobalPosition.Width)
                    GlobalPosition.X += Velocity.X;
                if (STATUS[6] == 1)
                    Direction = Estado.RIGHT;
            }
            if (Direction == Estado.STAND)
                Stand.Update(gameTime);

            UpdatePos(GlobalPosition);
        }

    }
}
