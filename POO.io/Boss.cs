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

    enum BossStates { SWIRL, MOVE, ATTACK };

    class Boss : AnimatedCharacter
    {
        ContentManager Content;

        int Timer = 0, Timer2 = 0, Timer3 = 0;
        Random rand = new Random();
        public GraphicsDeviceManager graphics;
        int NumRand;

        public int bossLives = 0;

        protected BossStates estado;

        public ArrayList EnemyArrows = new ArrayList();

        EnemyShot Shot;

        public BasicAnimatedSprite Move;
        public BasicAnimatedSprite Swirl;
        public BasicAnimatedSprite Attack;
        double diferenciaframe;

        public bool Devil = false;

        public Boss(Rectangle Pos, Color color, double diframe, GraphicsDeviceManager graphics) :base(Pos, color, diframe)
        {
            this.diferenciaframe = diframe;


            estado = BossStates.MOVE;
            GlobalPosition = Pos;
            this.graphics = graphics;

            switch(rand.Next(0,2))
            {
                case 0:
                    {
                        Devil = false;
                        color = Color.White;
                        bossLives = 120;
                        break;
                    }
                case 1:
                    {
                        Devil = true;
                        bossLives = 250;
                        Pos.Width += 50;
                        Pos.Height += 50;
                        color = Color.ForestGreen;
                        break;
                    }
            }

            Move = new BasicAnimatedSprite(Pos, color, diferenciaframe);
            Swirl = new BasicAnimatedSprite(Pos, color, diferenciaframe);
            Attack = new BasicAnimatedSprite(Pos, color, diferenciaframe);

        }

        public void LoadContent(ContentManager Content, string filename, string filename2, string filename3)
        {
            Move.LoadContent(Content, filename, 4);
            Swirl.LoadContent(Content, filename2, 4);
            Attack.LoadContent(Content, filename3, 3);
            this.Content = Content;
        }

        public override void Update(GameTime gameTime)
        {

            if(Devil == true)
            {
                Timer3 += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);

                if(Timer3 > 120 && estado == BossStates.SWIRL)
                {
                    Shot = new EnemyShot();
                    Shot.LoadContent(Content, "EnemyArrow/EnemyArrow", 9);
                    Shot.SetPos(new Rectangle(GlobalPosition.X, GlobalPosition.Y + 20, 28, 19));
                    Shot.Direction(-7, 0);
                    EnemyArrows.Add(Shot);
                    Timer3 = 0;
                }
            }

            for(int x = 0; x < EnemyArrows.Count; x++)
            {
                ((EnemyShot)EnemyArrows[x]).Update(gameTime);

                if(((EnemyShot)EnemyArrows[x]).GetRect().X < 0)
                {
                    EnemyArrows.RemoveAt(x);
                }
            }

            Timer += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
            Timer2 += (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);

            if (Timer >= 200)
            {
                NumRand = rand.Next(0, 100);

                Timer = 0;
            }

            Move.Update(gameTime);

            if(GlobalPosition.X < 600)
            {
                Velocity.X = 1;
            }

            if(GlobalPosition.X > graphics.PreferredBackBufferWidth)
            {
                Velocity.X = -1;
            }

            if(GlobalPosition.Y < 0)
            {
                Velocity.Y = 1;
            }

            if(GlobalPosition.Y > graphics.PreferredBackBufferHeight)
            {
                Velocity.Y = -1;
            }

            if(Velocity.Y == 0)
            {
                Velocity.Y = 1;
            }

            GlobalPosition.X += Velocity.X;
            GlobalPosition.Y += Velocity.Y;


            if (NumRand % 2==0)
            {
                estado = BossStates.SWIRL;
                Swirl.Update(gameTime);
            }
            else
            {
                estado = BossStates.MOVE;
                Move.Update(gameTime);
            }
            SetPos(GlobalPosition);
        }

        public void SetPos(Rectangle Pos)
        {
            Move.SetPos(Pos);
            Attack.SetPos(Pos);
            Swirl.SetPos(Pos);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < EnemyArrows.Count; x++)
            {
                ((EnemyShot)EnemyArrows[x]).Draw(spriteBatch);
            }

            if (estado == BossStates.MOVE)
            {
                Move.Draw(spriteBatch);
            }
            if (estado == BossStates.ATTACK)
            {
                Attack.Draw(spriteBatch);
            }
            if (estado == BossStates.SWIRL)
            {
                Swirl.Draw(spriteBatch);
            }
        }

        public bool CheckCollision<T>(T Object) where T : IBasicCollision
        {
            if(Intersects(Object.GetRect()))
            {
                if(Timer2 > 60)
                {
                    if (Object.GetType() == typeof(Shot))
                    {
                        if (estado == BossStates.SWIRL)
                        {
                            return true;
                        }
                        else if (estado == BossStates.MOVE)
                        {
                            estado = BossStates.ATTACK;
                            bossLives--;
                            return false;
                        }
                    }
                    if (Object.GetType() == typeof(Gordo))
                    {
                        estado = BossStates.SWIRL;
                        return true;
                    }
                    Timer2 = 0;
                }

            }
            return false;
        }
    }
}

