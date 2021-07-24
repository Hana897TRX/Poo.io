using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace POO.io
{
    abstract class AbstractCollision : IBasicCollision, IMonogame
    {
        public abstract bool Intersects(Rectangle Object);

        public abstract Rectangle GetRect();

        public abstract void LoadContent(ContentManager Content, string fileName);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual bool CheckCollision<T>(T Object) where T : IBasicCollision
        {
            return this.Intersects(Object.GetRect());
        }
    }
}
