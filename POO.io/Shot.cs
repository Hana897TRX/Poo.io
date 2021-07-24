using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    class Shot : Objects
    {
        public Shot()
        {

        }

        public void SetHeroPos(Rectangle Heropos)
        {
            OriginPos = Heropos;
            Pos = Heropos;
            Pos.X += 20;
            Pos.Y += 20;
        }
    }
}
