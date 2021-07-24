using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace POO.io
{
    enum Estado { UP, UPS, DOWN, DOWNS, RIGHT, RIGHTS, LEFT, LEFTS, STAND };
    enum Bounds { UP, DOWN, LEFT, RIGHT, NULL };



    public class Game1 : Game
    {
        string OneMoreTry;
        GraphicsDeviceManager graphics;
        Juego game2;
        SpriteBatch spriteBatch;
        BasicSprite Logo = new BasicSprite();
        SpriteFont ScoreFont;
        int HightScore = 0;


        SoundEffect menusong, juego, endm, boss, bossExplosion;
        SoundEffectInstance menusonginstance, juegoinstance, endminstance, bossinstance, bossEinstance;
        int Q = 0;

        enum GameState
        {
            menu, instrucciones, jugar, perder,
        }
        GameState CurrentGameState = GameState.menu;
        Menu jugar; //jugar
        Menu instructions; //instrucciones
        Menu menu;//menu
        Menu Exit;//salir
        Menu boton5; //game over

        bool lose;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            game2 = new Juego();
        }

        
        protected override void Initialize()
        {
            game2.Initialize(graphics);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ScoreFont = Content.Load<SpriteFont>("ScoreFont");

            menusong = Content.Load<SoundEffect>("BADBOY");
            menusonginstance = menusong.CreateInstance();

            boss = Content.Load<SoundEffect>("Boss00");
            bossinstance = boss.CreateInstance();

            bossExplosion = Content.Load<SoundEffect>("Boss01");
            bossEinstance = bossExplosion.CreateInstance();

            juego = Content.Load<SoundEffect>("Space");
            juegoinstance =juego.CreateInstance();

            endm = Content.Load<SoundEffect>("erro");
            endminstance = endm.CreateInstance();

            Logo.LoadContent(Content, "Title");


            IsMouseVisible = true;

            jugar = new Menu(Content.Load<Texture2D>("Play"), graphics.GraphicsDevice);
            jugar.setPosition(new Vector2((graphics.PreferredBackBufferWidth / 2.8F), graphics.PreferredBackBufferHeight / 4.2F));

            instructions = new Menu(Content.Load<Texture2D>("Instructions"), graphics.GraphicsDevice);
            instructions.setPosition(new Vector2(graphics.PreferredBackBufferWidth / 2.8F, graphics.PreferredBackBufferHeight / 2.1F));

            menu = new Menu(Content.Load<Texture2D>("Menu"), graphics.GraphicsDevice);
            menu.setPosition(new Vector2(graphics.PreferredBackBufferWidth / 2.8F, graphics.PreferredBackBufferHeight / 83 -20));

            Exit = new Menu(Content.Load<Texture2D>("Exit"), graphics.GraphicsDevice);
            Exit.setPosition(new Vector2(graphics.PreferredBackBufferWidth / 2.8F, graphics.PreferredBackBufferHeight / 1.4F));
            Logo.SetPos(new Rectangle((int)(graphics.PreferredBackBufferWidth / 4.1F), -70 , 420, 220));

            game2.LoadContent(Content);
           
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                case GameState.menu:

                    menusonginstance.IsLooped= true;
                    menusonginstance.Play();
                    if (jugar.isClicked == true)
                    {
                        CurrentGameState = GameState.jugar;
                        OneMoreTry = "\n\n\nYou have one more Try";
                        jugar.isClicked = false;
                    }
                    jugar.Update(mouse);

                    if (instructions.isClicked == true)
                    {
                        CurrentGameState = GameState.instrucciones;

                        instructions.isClicked = false;
                    }
                    instructions.Update(mouse);

                    if (Exit.isClicked == true) Exit();
                    Exit.Update(mouse);
                    
                    break;

                case GameState.instrucciones:

                    if (menu.isClicked == true)
                    {
                        CurrentGameState = GameState.menu;
                        jugar.isClicked = false;
                        menu.isClicked = false;
                        menu.isClicked = false;
                    }
                    menu.Update(mouse);

                    break;

                case GameState.jugar:

                    menusonginstance.Stop();
                    endminstance.Stop();


                    game2.Update(gameTime);

                    if(game2.BossArray.Count == 0)
                    {
                        bossinstance.Stop();
                        bossEinstance.Stop();
                        juegoinstance.IsLooped = true;
                        juegoinstance.Play();
                    }
                    else if(game2.BossArray.Count == 1)
                    {
                        bossinstance.IsLooped = true;
                        bossinstance.Play();
                        juegoinstance.Stop();
                    }
                    else if(game2.BossArray.Count >= 2)
                    {
                        bossinstance.Stop();
                        bossEinstance.IsLooped = true;
                        bossEinstance.Play();
                    }

                    if (game2.LUS == true)
                    {
                        CurrentGameState = GameState.perder;
                    }
                    break;

                case GameState.perder:

                    juegoinstance.Stop();
                    bossinstance.Stop();
                    bossEinstance.Stop();

                    if(game2.Score > HightScore)
                    {
                        HightScore = game2.Score;
                    }

                    Q++;
                    if (Q < 50)
                    {
                        endminstance.IsLooped = true;
                        endminstance.Play();
                    }
                    else
                    {
                        endminstance.Stop();
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)){

                        Exit();
                    }


                    if(CurrentGameState == GameState.perder && Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        OneMoreTry = "";
                        NewTry();
                    }

                    break;

            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.menu:
                    try
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("fondo"), new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                        Logo.Draw(spriteBatch);
                        
                    }
                    catch
                    {
                        System.Console.WriteLine("Error, couldn't be load texture");
                    }
                    jugar.Draw(spriteBatch);
                    instructions.Draw(spriteBatch);
                    Exit.Draw(spriteBatch);
                    break;

                case GameState.jugar:

                    game2.Draw(spriteBatch);
                    
               
                    break;

                case GameState.instrucciones:
                    try
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("space2"), new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    }
                    catch
                    {
                        System.Console.WriteLine("Error, couldn't be load texture");
                    }
                    
                    menu.Draw(spriteBatch);
                    break;

                case GameState.perder:
                    spriteBatch.Draw(Content.Load<Texture2D>("BlueScreenDeath"), new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    spriteBatch.DrawString(ScoreFont, "Your HighScore is: " + HightScore.ToString() + OneMoreTry, new Vector2(graphics.PreferredBackBufferWidth / 3, graphics.PreferredBackBufferHeight / 80), Color.Azure);
                    break;

            }

            spriteBatch.End();
            

            base.Draw(gameTime);
        }

        public void NewTry()
        {
            CurrentGameState = GameState.menu;
            game2.kirby.lives = 10;
            game2.Gordos.Clear();
            game2.BHA.Clear();
            game2.BossArray.Clear();
            game2.HearthArray.Clear();
            game2.Meteorites.Clear();
            game2.kirby.Disparos.Clear();
            game2.BossDefeat = 0;
            game2.Score = 0;
        }
    }
}
