using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace POO.io
{
    class Juego
    {
        ContentManager Content;
        Random rand = new Random();
        public Kirby kirby;
        Background BG;
        GraphicsDeviceManager graphics;
        int Timer = 0, Timer2 = 0, Timer3 = 0, Timer4 = 0, TimerHealth = 0, GordosDif = 40;
        public int Score = 0, BossDefeat = 0;
        BlackHole blackhole = new BlackHole();


        public ArrayList Gordos = new ArrayList();
        public ArrayList BHA = new ArrayList();
        public ArrayList BossArray = new ArrayList();
        public ArrayList HearthArray = new ArrayList();
        public ArrayList Meteorites = new ArrayList();

        Gordo gordo;
        Hearth hearth;
        Meteor Meteorite;
        Boss Leviathan;

        SpriteFont ScoreFont;


        bool OneBoss = true;

        public bool lus;

        public bool LUS
        {
            get { return lus; }
            set { lus = value; }
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            kirby = new Kirby(new Rectangle(32, 32, 32, 32), Color.White, Keys.Up, Keys.Down, Keys.Left, Keys.Right, graphics, 30, 10);
            BG = new Background(graphics);
            BG.SetOrigin(new Rectangle(0, 0, 800, 480));

            this.graphics = graphics;
        }

        public void LoadContent(ContentManager Content)
        {
            BG.LoadContent(Content, "v01");
            kirby.LoadContent(Content, "Kirby");

            ScoreFont = Content.Load<SpriteFont>("ScoreFont");
            this.Content = Content;
        }

        public void Update(GameTime gameTime)
        {
            Score += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            Timer += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            Timer2 += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            Timer4 += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);

            kirby.Update(gameTime);
            BG.Update(gameTime);
            NewBoss();
            NewHearth(gameTime);
            newMeteorites(gameTime);

            for(int x = 0; x < Meteorites.Count; x++)
            {
                ((Meteor)Meteorites[x]).Update(gameTime);

                kirby.CheckCollision<Meteor>((((Meteor)Meteorites[x])));

                if (((Meteor)Meteorites[x]).GetRect().X < 0 || ((Meteor)Meteorites[x]).GetRect().Y > graphics.PreferredBackBufferHeight)
                    Meteorites.RemoveAt(x);
            }

            for (int x = 0; x < HearthArray.Count; x++)
            {
                ((Hearth)HearthArray[x]).Update(gameTime);
                if (((Hearth)HearthArray[x]).GetRect().X < 0)
                {
                    HearthArray.RemoveAt(x);
                }
            }

            for (int x = 0; x < HearthArray.Count; x++)
            {
                ((Hearth)HearthArray[x]).Update(gameTime);
            }

            if (Timer2 > 50 && BHA.Count < 3)
            {
                blackhole = new BlackHole();
                blackhole.SetPos(new Rectangle(rand.Next(0, graphics.PreferredBackBufferWidth + 0), rand.Next(0, graphics.PreferredBackBufferHeight + 0), 100, 100));
                if(blackhole.GetRect().Intersects(kirby.GetRect()))
                {
                    blackhole.SetPos(new Rectangle(rand.Next(0, graphics.PreferredBackBufferWidth + 0), rand.Next(0, graphics.PreferredBackBufferHeight + 0), 100, 100));
                }
                blackhole.LoadContent(Content, "BH/BH", 36);
                BHA.Add(blackhole);
                Timer2 = 0;
            }

            if(BossDefeat >= 2)
            {
                GordosDif = 25;
            }
            else
            {
                GordosDif = 40;
            }

            if (Timer > GordosDif)
            {
                gordo = new Gordo();
                gordo.Direction(-3, 0);
                gordo.LoadContent(Content, "Gordo/Gordo", 18);
                gordo.SetPos(new Rectangle(graphics.PreferredBackBufferWidth + 100, rand.Next(0, graphics.PreferredBackBufferHeight), 32, 32));
                Gordos.Add(gordo);
                Timer = 0;
            }

            for (int x = 0; x < BossArray.Count; x++)
            {
                ((Boss)BossArray[x]).Update(gameTime);
            }

            for (int x = 0; x < Gordos.Count; x++)
            {
                if (((Gordo)Gordos[x]).GetRect().X < 0)
                {
                    Gordos.RemoveAt(x);
                }

                ((Gordo)Gordos[x]).Update(gameTime);
            }

            for (int x = 0; x < Gordos.Count; x++)
            {
                kirby.CheckCollision<Gordo>(((Gordo)Gordos[x]));
            }

            for (int x = 0; x < BHA.Count; x++)
            {
                ((BlackHole)BHA[x]).Follow(kirby.GlobalPosition, gameTime);
                kirby.CheckCollision<BlackHole>(((BlackHole)BHA[x]));

                for (int y = 0; y < BHA.Count; y++)
                {
                    if (((BlackHole)BHA[x]).CheckCollision<BlackHole>(((BlackHole)BHA[y])) && x != y)
                    {
                        BHA.RemoveAt(x);
                        break;
                    }
                }
            }

            for (int x = 0; x < kirby.Disparos.Count; x++)
            {
                for (int y = 0; y < Gordos.Count; y++)
                {
                    if (((Gordo)Gordos[y]).Checkcollision<Shot>((Shot)kirby.Disparos[x]))
                    {
                        Gordos.RemoveAt(y);
                        Score += 50;
                    }
                }
            }

            for(int x = 0; x < BossArray.Count; x++)
            {
                for(int i = 0; i < ((Boss)BossArray[x]).EnemyArrows.Count; i++)
                {
                    kirby.CheckCollision<EnemyShot>((EnemyShot)((Boss)BossArray[x]).EnemyArrows[i]);
                }
                kirby.CheckCollision<Boss>(((Boss)BossArray[x]));
                for(int y = 0; y < kirby.Disparos.Count; y++)
                {
                    if (((Boss)BossArray[x]).CheckCollision<Shot>(((Shot)kirby.Disparos[y])))
                    {
                        ((Shot)kirby.Disparos[y]).Direction(-8, 0);
                        ((Shot)kirby.Disparos[y]).SetColor(Color.PaleVioletRed);
                        ((Shot)kirby.Disparos[y]).Devil = true;
                    }
                }

                if(((Boss)BossArray[x]).bossLives <= 0)
                {
                    BossArray.RemoveAt(x);
                    Score += 500;
                    BossDefeat++;
                }
            }

            for(int x = 0; x < BossArray.Count; x++)
            {
                for(int y = 0; y < Gordos.Count; y++)
                {
                    if(((Boss)BossArray[x]).CheckCollision<Gordo>(((Gordo)Gordos[y])))
                    {
                        ((Gordo)Gordos[y]).Direction(-7,0);
                        ((Gordo)Gordos[y]).SetColor(Color.DarkViolet);
                    }

                }
            }

            for(int x = 0; x < HearthArray.Count; x++)
            {
                if (kirby.CheckCollision<Hearth>(((Hearth)HearthArray[x])))
                {
                    HearthArray.RemoveAt(x);
                }
            }
            
            //System of Lives
            if (kirby.Vidas <= 0 )
            {
                lus = true;
            }           
            else
            {
                lus = false;    
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BG.Draw(spriteBatch);

            for (int x = 0; x < Meteorites.Count; x++)
            {
                ((Meteor)Meteorites[x]).Draw(spriteBatch);
            }

            for (int x = 0; x < HearthArray.Count; x++)
            {
                ((Hearth)HearthArray[x]).Draw(spriteBatch);
            }

            for(int x = 0; x < BossArray.Count; x++)
            {
                ((Boss)BossArray[x]).Draw(spriteBatch);
            }
            kirby.Draw(spriteBatch);

            spriteBatch.DrawString(ScoreFont, "SCORE: " + Score.ToString(), new Vector2(0, 32), Color.White);

            for (int x = 0; x < Gordos.Count; x++)
            {
                ((Gordo)Gordos[x]).Draw(spriteBatch);
            }

            for (int x = 0; x < BHA.Count; x++)
            {
                ((BlackHole)BHA[x]).Draw(spriteBatch);
            }
        }

        public void NewBoss()
        {
            if(Score % 3000 >= 2900 && Timer4 > 3400) 
            {
                if(OneBoss)
                {
                    Leviathan = new Boss(new Rectangle(1000, 300, 50, 75), Color.White, 9, graphics);
                    Leviathan.LoadContent(Content, "LevMove/L", "LevShot/L", "LevDie/L");
                    BossArray.Add(Leviathan);
                    OneBoss = false;
                    Timer4 = 0;
                }

            }
            else
            {
                OneBoss = true;
            }
        }

        public void NewHearth(GameTime gameTime)
        {
            TimerHealth += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            if(Score % 1800 > 1300 && kirby.lives < 10 && TimerHealth > 250 && HearthArray.Count < 1)
            {
                hearth = new Hearth();
                hearth.SetPos(new Rectangle(850, rand.Next(0, graphics.PreferredBackBufferHeight), 32, 32));
                hearth.LoadContent(Content, "Objects/Hearth");
                hearth.Direction(-1,0);
                HearthArray.Add(hearth);
                Timer3 = 0;
            }
        }

        public void newMeteorites(GameTime gameTime)
        {
            Timer3 += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            if (Timer3 > 45 && BossDefeat >= 1)
            {
                Meteorite = new Meteor();
                Meteorite.LoadContent(Content, "Meteorite/Meteorite00");
                Meteorite.Direction(-5,2);
                Meteorite.SetPos(new Rectangle(rand.Next(0,graphics.PreferredBackBufferWidth),0, 32, 32));
                Meteorites.Add(Meteorite);
                Timer3 = 0;
            }
        }
    }
}
