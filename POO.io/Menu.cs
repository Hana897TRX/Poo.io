using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace POO.io
{
    class Menu
    {
        Texture2D textura;
        Vector2 posicion;
        Rectangle rectangulo;
        Color color = new Color(255, 255, 255, 255);
        public Vector2 size;
        public Menu(Texture2D nuevatextura, GraphicsDevice graphics)
        {
            textura = nuevatextura;
            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height/ 3);
        }

        bool down;
        public bool isClicked;
        public void Update(MouseState mouse)
        {
            rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, (int)size.X, (int)size.Y);
            Rectangle minimouse = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (minimouse.Intersects(rectangulo))
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;
                if (down) color.A += 3; else color.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (color.A < 255)
            {
                color.A += 3;
                isClicked = false;
            }

        }

        public void setPosition(Vector2 newPosition)
        {
            posicion = newPosition;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.Draw(textura, rectangulo, color);
            }
            catch
            {
                if (textura == null)
                {
                    System.Console.WriteLine("Texture was null");
                }
                else if (rectangulo == new Rectangle(0, 0, 0, 0))
                {
                    System.Console.WriteLine("Rectangle was null");
                }
                else if (color == null)
                {
                    System.Console.WriteLine("Color was null");
                }
            }

        }
    }
}
