using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    class BasicSprite : AbstractCollision
    {
        protected Rectangle Pos;
        protected Color color = Color.White;
        protected Rectangle Origin;
        protected Texture2D image;
        protected int Timer = 0;

        public BasicSprite()
        {

        }

        public BasicSprite(Rectangle Pos, Color color)
        {
            this.Pos = Pos;
            this.color = color;
        }

        public BasicSprite(Rectangle Pos, Color color, Rectangle Origin)
        {
            this.Pos = Pos;
            this.color = color;
            this.Origin = Origin;

        }

        public override void LoadContent(ContentManager Content, string fileName)
        {
            try
            {
                image = Content.Load<Texture2D>(fileName);
            }
            catch(Exception exeption)
            {
                System.Console.Write("Error, the file not match with the type of texture," + exeption);
            }
            Pos.Width = image.Width;
            Pos.Height = image.Height;

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if (Origin == null)
            //    Origin = Pos;
            try
            {
                if (Origin != new Rectangle(0, 0, 0, 0))
                {
                    //spriteBatch.Begin();
                    spriteBatch.Draw(image, Pos, Origin, color);
                    //spriteBatch.End();
                }
                else
                {
                    //spriteBatch.Begin();
                    spriteBatch.Draw(image, Pos, color);
                    //spriteBatch.End();
                }
            }
            catch(Exception exeption)
            {
                if(Pos == null || Pos == new Rectangle(0,0,0,0))
                {
                    System.Console.WriteLine("Error, the object have no position");
                }
                else if(Origin == null || Origin == new Rectangle(0,0,0,0))
                {
                    System.Console.WriteLine("Error, the object have no origin");
                }
            }
        }

        public override Rectangle GetRect()
        {
            return Pos;
        }

        public override bool Intersects(Rectangle rectangle)
        {
            return Pos.Intersects(rectangle);
        }

        public void SetPos(Rectangle Pos)
        {
            this.Pos = Pos;
        }

        public void SetOrigin(Rectangle Origin)
        {
            this.Origin = Origin;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }
    }
}

