using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace ludum_dare_49
{
    class Ending
    {
        private Texture2D bgTex;

        private bool didWin = false;

        public Ending(bool didWin)
        {
            if (didWin) { 
                bgTex = Program.renderer.LoadImage("sunset.png");
            } else { 
                bgTex = Program.renderer.LoadImage("ded.png");
            }
        }

        public void Update(float dt)
        {
            // TODO: keypresses or soemthing?
        }
        public void Draw() { 
            Raylib.DrawTexture(bgTex, 0, 0, Color.WHITE);
        }

    }
}
