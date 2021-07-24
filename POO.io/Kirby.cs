using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    class Kirby : Manual
    {
        BasicSprite HP = new BasicSprite();
        ArrayList Lives = new ArrayList();
        public ArrayList Disparos = new ArrayList();
        Shot Shoot = new Shot();
        ContentManager Content;
        public int lives;


        public int Vidas
        {
            get { return lives; }
            set { lives = value; }
        }


        int Timer = 0, Timer2 = 120;

        int y = -20;
        protected double timeframe;
        public Kirby(Rectangle Pos, Color color, Keys Up, Keys Down, Keys Left, Keys Right, GraphicsDeviceManager graphics, double tiempodelFrame, int livesss) : base(Pos, color, Up, Down, Left, Right, graphics, tiempodelFrame)
        {
            this.lives = livesss;
            this.timeFrame = tiempodelFrame;
            Upk = Up;
            Downk = Down;
            Leftk = Left;
            Rightk = Right;
            this.graphics = graphics;
            UpdatePos(Pos);
        }

        public override void LoadContent(ContentManager Content, string NameOfObject)
        {
            this.Content = Content;
            for (int x = 0; x < lives; x++)
            {
                y += 20;
                HP = new BasicSprite(new Rectangle(y, 10, 16, 16), Color.White);
                HP.LoadContent(Content, "Objects/Hearth");
                Lives.Add(HP);
            }
            try
            {
                LoadContent(Estado.DOWN, Content, NameOfObject, NameOfObject + "Down", 1);
                LoadContent(Estado.UP, Content, NameOfObject, NameOfObject + "Up", 1);
                LoadContent(Estado.STAND, Content, NameOfObject, NameOfObject + "Star", 1);
                //shot.LoadContent(Content, "Shot", 2);
            }
            catch(Exception exeption)
            {
                System.Console.WriteLine("The files couldn't be loaded");
            }
        }

        public override void Update(GameTime gameTime)
        {
            Timer += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            Timer2 += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.X) && Timer > 15)
            {
                try
                {
                    NewShot();
                }
                catch(Exception exeption)
                {
                    System.Console.WriteLine("Error in generate new shot");
                }
                Timer = 0;
            }

            for (int x = 0; x < Disparos.Count; x++)
            {
                ((Shot)Disparos[x]).Update(gameTime);

                if(((Shot)Disparos[x]).GetRect().Intersects(GlobalPosition) && ((Shot)Disparos[x]).Devil == true)
                {
                    if(Timer2 > 30)
                    {
                        lives -= 2;
                        SetColor(Color.DarkRed);
                        GlobalPosition.Y += 5;
                        GlobalPosition.X -= 5;
                        Timer2 = 0;
                    }
                }

                if (((Shot)Disparos[x]).GetRect().X > ((Shot)Disparos[x]).GetOriginPos().X + 250 || ((Shot)Disparos[x]).GetRect().X < 0)
                {
                    Disparos.RemoveAt(x);
                }
            }

            

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

                for (int x = 0; x < lives; x++)
                {
                    ((BasicSprite)Lives[x]).Draw(spriteBatch);
                }


            for (int x = 0; x < Disparos.Count; x++)
            {
                ((Shot)Disparos[x]).Draw(spriteBatch);
            }


            base.Draw(spriteBatch);
        }

        public void NewShot()
        {
            Shoot = new Shot();
            Shoot.SetHeroPos(GlobalPosition);
            Shoot.Direction(4, 0);
            try
            {
                Shoot.LoadContent(Content, "Shot/Shot");
            }
            catch(Exception exeption)
            {
                System.Console.WriteLine("Error, the texture couldn't be loaded" + exeption);
            }
            Disparos.Add(Shoot);
        }

        public ArrayList GetShots()
        {
            return Disparos;
        }

        public bool CheckCollision<T>(T Object) where T : IBasicCollision
        {
            Rectangle CheckBox;
            if(Object.GetType() == typeof(BlackHole))
            {
                CheckBox = new Rectangle((int)(Object.GetRect().X + Object.GetRect().Width / 5.5F),(int)(Object.GetRect().Y + Object.GetRect().Height / 5.5F),(int)(Object.GetRect().Width / 2F),(int)(Object.GetRect().Height / 2F));
            }
            else
            {
                CheckBox = Object.GetRect();
            }


            if (Timer2 > 30)
            {
                if (Intersects(CheckBox))
                {

                    if (Object.GetType() == typeof(Gordo))
                    {
                        lives--;
                        SetColor(Color.Red);

                    }
                    if (Object.GetType() == typeof(BlackHole))
                    {
                        lives = 0;
                        SetColor(Color.Black);
                    }
                    if (Object.GetType() == typeof(Hearth))
                    {
                        lives++;
                    }
                    if (Object.GetType() == typeof(Boss))
                    {
                        SetColor(Color.DarkRed);
                        lives--;
                    }
                    if (Object.GetType() == typeof(Meteor))
                    {
                        GlobalPosition.X -= 7;
                        GlobalPosition.Y += 2;
                        lives--;
                        SetColor(Color.DarkRed);
                    }
                    if(Object.GetType() == typeof(EnemyShot))
                    {
                        lives -= 3;
                        SetColor(Color.ForestGreen);
                    }
                    Timer2 = 0;
                    return true;
                }
                SetColor(Color.White);
            }
            return false;
        }
    }
}
