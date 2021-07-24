using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    interface IMonogame
    {
        void LoadContent(ContentManager Content, string fileName);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
