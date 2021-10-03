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
        const int FRAME_STEP_SIZE = 4;
        
        public Vector2 pos = new Vector2(16 * 2, 16 * 2);

        private string current_frame = "Player1";
        private float animTime = 0f;

        private float stepAnimTime = 0f;
        private string direction = "None";
        private int stepFrame = 0;

        public Player() 
        {

        }

        public void Update(float dt) 
        {
            animTime += dt;
            if (animTime > 0.25f) {
                if (current_frame == "Player1")
                    current_frame = "Player2";
                else
                    current_frame = "Player1";

                animTime -= 0.25f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)) {
                Console.WriteLine(animTime.ToString());
            }

            // Check if we should start a movement
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP) && direction == "None") {
                direction = "Up";
                stepFrame = 0;
                Program.arrows.ConfirmKeyPressed(direction);
            } 
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT) && direction == "None") {
                direction = "Left";
                stepFrame = 0;
                Program.arrows.ConfirmKeyPressed(direction);
            } 
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN) && direction == "None") {
                direction = "Down";
                stepFrame = 0;
                Program.arrows.ConfirmKeyPressed(direction);
            } 
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) && direction == "None") {
                direction = "Right";
                stepFrame = 0;
                Program.arrows.ConfirmKeyPressed(direction);
            }

            // This loop runs every 1 frames (assuming 60 fps)
            // TODO: use lerp instead
            stepAnimTime += dt;
            if (stepAnimTime > 0.016666f) {
                stepAnimTime -= 0.016666f;

                // Do actual movement
                if (direction == "Up") {
                    pos += new Vector2(16 * 0, 16 * -1) / 4;
                    stepFrame++;
                } else if (direction == "Left") {
                    pos += new Vector2(16 * -1, 16 * 0) / 4;
                    stepFrame++;
                } else if (direction == "Down") {
                    pos += new Vector2(16 * 0, 16 * 1) / 4;
                    stepFrame++;
                } else if (direction == "Right") {
                    pos += new Vector2(16 * 1, 16 * 0) / 4;
                    stepFrame++;
                }

                // check for end of action
                if (direction != "None" && stepFrame >= FRAME_STEP_SIZE) {
                    direction = "None";
                }
            }
            
        }

        public void Draw()
        {
            Program.renderer.DrawImage(current_frame, pos);
        }

    }
}
