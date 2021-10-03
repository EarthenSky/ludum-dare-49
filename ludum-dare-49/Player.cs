using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace ludum_dare_49
{
    class Player
    {
        public Vector2 pos = new Vector2(16 * 2, 16 * 2);
        public string current_frame = "Player1";

        private float anim_time = 0f;

        public Player() 
        {

        }

        public void Update(float dt) 
        {
            anim_time += dt;
            if (anim_time > 0.25f) {
                if (current_frame == "Player1")
                    current_frame = "Player2";
                else
                    current_frame = "Player1";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
            {
                Console.WriteLine(anim_time.ToString());
            }
        }

        public void Draw()
        {
            Program.renderer.DrawImage(current_frame, pos);
        }

    }
}
