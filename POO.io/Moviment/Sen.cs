using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io.Moviment
{
    class Sen
    {
        public Sen()
        {

        }

        public Vector2 GetMoviment(Vector2 Pos)
        {
            Vector2 FinalPos = new Vector2();

            FinalPos.Y = (int)(3 * Math.Sin(Pos.X));
            FinalPos.X++;
            return FinalPos;
        }
    }
}
