using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO.io
{
    class BlackHole : Objects
    {
        public BlackHole()
        {
            TimerPerFrame = 2;
        }

        public bool CheckCollision<T>(T Object) where T : IBasicCollision
        {
            if (Intersects(Object.GetRect()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
