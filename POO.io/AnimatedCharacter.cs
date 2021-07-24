using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    class AnimatedCharacter : AbstractCollision
    {
        public int[] STATUS = new int[9];

        //      0      1       2       3       4       5       6       7        8
        //      UP    UPS    DOWN    DOWNS    LEFT   LEFTS   RIGHT   RIGHTS   STAND

        protected Bounds bounds = Bounds.NULL;
        protected Estado Direction;

        public BasicAnimatedSprite Left;
        public BasicAnimatedSprite Right;
        public BasicAnimatedSprite Up;
        public BasicAnimatedSprite Down;

        public BasicSprite StandUp;
        public BasicSprite StandDown;
        public BasicSprite StandRight;
        public BasicSprite StandLeft;
        public BasicSprite Stand;

        public Rectangle GlobalPosition;
        protected Point Velocity;

        protected double timeFrame;

        public AnimatedCharacter(Rectangle Pos, Color color, double tiempoFrame)
        {
            this.timeFrame = tiempoFrame;
            Left = new BasicAnimatedSprite(Pos, color, timeFrame);
            Right = new BasicAnimatedSprite(Pos, color, timeFrame);
            Up = new BasicAnimatedSprite(Pos, color, timeFrame);
            Down = new BasicAnimatedSprite(Pos, color, timeFrame);

            StandUp = new BasicSprite(Pos, color);
            StandDown = new BasicSprite(Pos, color);
            StandRight = new BasicSprite(Pos, color);
            StandLeft = new BasicSprite(Pos, color);
            Stand = new BasicSprite(Pos, color);

            GlobalPosition = Pos;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void LoadContent(ContentManager Content, string fileName)
        {
            throw new Exception("No pudo cargarse los datos correctos");
        }
        public void LoadContent(Estado Direction, ContentManager Content, string FolderName, string FileName, int frames)
        {
            switch (Direction)

            {
                case (Estado.LEFT):
                    {
                        Left.LoadContent(Content, FolderName + "/" + FileName, frames);
                        STATUS[4] = 1;
                        break;
                    }
                case (Estado.RIGHT):
                    {
                        Right.LoadContent(Content, FolderName + "/" + FileName, frames);
                        STATUS[6] = 1;
                        break;
                    }
                case (Estado.UP):
                    {
                        Up.LoadContent(Content, FolderName + "/" + FileName, frames);
                        STATUS[0] = 1;
                        break;
                    }
                case (Estado.DOWN):
                    {
                        Down.LoadContent(Content, FolderName + "/" + FileName, frames);
                        STATUS[2] = 1;
                        break;
                    }
                case (Estado.LEFTS):
                    {
                        StandLeft.LoadContent(Content, FolderName + "/" + FileName);
                        STATUS[5] = 1;
                        break;
                    }
                case (Estado.RIGHTS):
                    {
                        StandRight.LoadContent(Content, FolderName + "/" + FileName);
                        STATUS[7] = 1;
                        break;
                    }
                case (Estado.UPS):
                    {
                        StandUp.LoadContent(Content, FolderName + "/" + FileName);
                        STATUS[1] = 1;
                        break;
                    }
                case (Estado.DOWNS):
                    {
                        StandDown.LoadContent(Content, FolderName + "/" + FileName);
                        STATUS[3] = 1;
                        break;
                    }
                    case (Estado.STAND):
                    {
                        Stand.LoadContent(Content, FolderName + "/" + FileName);
                        STATUS[8] = 1;
                        break;
                    }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Direction == Estado.UP /*&& STATUS[0] == 1*/)
            {
                Up.Draw(spriteBatch);
            }
            else if (Direction == Estado.UPS && STATUS[1] == 1)
            {
                StandUp.Draw(spriteBatch);
            }

            if (Direction == Estado.DOWN /*&& STATUS[2] == 1*/)
            {
                Down.Draw(spriteBatch);
            }
            else if (Direction == Estado.DOWNS && STATUS[3] == 1)
            {
                StandDown.Draw(spriteBatch);
            }

            if (Direction == Estado.RIGHT && STATUS[6] == 1)
            {
                Right.Draw(spriteBatch);
            }
            else if (Direction == Estado.RIGHTS && STATUS[7] == 1)
            {
                StandRight.Draw(spriteBatch);
            }

            if (Direction == Estado.LEFT && STATUS[4] == 1)
            {
                Left.Draw(spriteBatch);
            }
            else if (Direction == Estado.LEFTS && STATUS[5] == 1)
            {
                StandLeft.Draw(spriteBatch);
            }
            if (Direction == Estado.STAND /*&& STATUS[8] == 1*/)
            {
                Stand.Draw(spriteBatch);
            }
        }

        public override Rectangle GetRect()
        {
            return GlobalPosition;
        }

        public override bool Intersects(Rectangle Object)
        {
            return GlobalPosition.Intersects(Object);
        }

        public void UpdatePos(Rectangle GlobalPos)
        {
            Up.SetPos(GlobalPosition);
            Down.SetPos(GlobalPosition);
            Right.SetPos(GlobalPosition);
            Left.SetPos(GlobalPosition);

            StandUp.SetPos(GlobalPosition);
            StandDown.SetPos(GlobalPosition);
            StandRight.SetPos(GlobalPosition);
            StandLeft.SetPos(GlobalPosition);

            Stand.SetPos(GlobalPosition);
        }

        public void SetColor(Color color)
        {
            Up.SetColor(color);
            Down.SetColor(color);
            Right.SetColor(color);
            Left.SetColor(color);

            StandUp.SetColor(color);
            StandDown.SetColor(color);
            StandRight.SetColor(color);
            StandLeft.SetColor(color);
            Stand.SetColor(color);

        }
    }
}
