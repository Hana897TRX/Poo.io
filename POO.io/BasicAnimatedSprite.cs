using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    class BasicAnimatedSprite : BasicSprite
    {
        int FrameCount = 0, FrameWidth, FrameHeight, CurrentFrame;
        protected double TimerPerFrame;
        ArrayList textures = new ArrayList();
        int IsSpriteSheet = 2;
        Rectangle SpriteSheetRec = new Rectangle();

        public BasicAnimatedSprite()
        {

        }

        public BasicAnimatedSprite(Rectangle Pos, Color color, double TimePerFrame)
        {
            this.Pos = Pos;
            this.color = color;
            this.TimerPerFrame = TimePerFrame;
        }

        public void LoadContent(ContentManager Content, string folderName, int frameCount) // MultiplesFiles
        {
            FrameCount = frameCount;

            for (int x = 0; x < frameCount; x++)
            {
                try
                {
                    Texture2D temTex;
                    temTex = Content.Load<Texture2D>(folderName + x.ToString("00"));
                    textures.Add(temTex);
                }
                catch
                {
                    throw new Exception("One or more files don't exist or have an error");
                }

            }
            IsSpriteSheet = 0;
        }

        public void LoadContent(ContentManager Content, string file, int frameWidth, int frameHeight) //For SpriteSheet
        {
            try
            {
                image = Content.Load<Texture2D>(file);
                FrameWidth = frameWidth;
                FrameHeight = frameHeight;
                IsSpriteSheet = 1;
            }
            catch
            {
                System.Console.WriteLine("Error to load texture");
            }

            if(FrameHeight < 0 || FrameWidth < 0)
            {
                throw new Exception("The values of FrameHeight or FrameWidth are negative, these are forbidden");
            }
        }

        public override void Update(GameTime gameTime)
        {
            Timer += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            if (IsSpriteSheet == 1)
            {
                if (Timer >= TimerPerFrame)
                {
                    if ((SpriteSheetRec.Height + FrameHeight) >= image.Height)
                    {
                        SpriteSheetRec.Height = 0;
                    }

                    if ((SpriteSheetRec.Width + FrameWidth) < image.Width)
                    {
                        SpriteSheetRec.Width += FrameWidth;
                    }
                    else
                    {
                        SpriteSheetRec.Width = 0;
                        SpriteSheetRec.Height += FrameHeight;
                    }

                    Timer = 0;
                }
            }
            else if(IsSpriteSheet == 0)
            {
                if (Timer >= TimerPerFrame)
                {
                    try
                    {
                        CurrentFrame = (CurrentFrame + 1) % FrameCount;
                        Timer = 0;
                    }
                    catch(Exception exeption)
                    {
                        if(FrameCount == 0)
                        {
                            System.Console.WriteLine("Error, FrameCount was 0, " + exeption);
                        }
                        else
                        {
                            System.Console.WriteLine("Error, " + exeption);
                        }
                    }
                    
                }
            }
            else
            {

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsSpriteSheet == 1)
            {
                Origin = new Rectangle(SpriteSheetRec.Width, SpriteSheetRec.Height, FrameWidth, FrameHeight);
                base.Draw(spriteBatch);
            }

            else if (IsSpriteSheet == 0)
            {
                    //spriteBatch.Begin();
                    spriteBatch.Draw((Texture2D)textures[CurrentFrame], Pos, color);
                    //spriteBatch.End();
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
