using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace ludum_dare_49
{
    class Arrows
    {
        public int upFilled = 0;
        public int leftFilled = 0;
        public int downFilled = 0;
        public int rightFilled = 0;

        public Vector2 hpPos;

        public Vector2 upPos;
        public Vector2 leftPos;
        public Vector2 downPos;
        public Vector2 rightPos;

        public Vector2 upShake;
        public Vector2 leftShake;
        public Vector2 downShake;
        public Vector2 rightShake;

        private float animTime = 0f;

        public Arrows() 
        {
            var width = Program.level.width;
            var height = Program.level.height;

            hpPos = new Vector2(16 * (width - 4), 16 * (height - 2));

            upPos = new Vector2(16 * (width - 2), 16 * (height - 2));
            leftPos = new Vector2(16 * (width - 3), 16 * (height - 1));
            downPos = new Vector2(16 * (width - 2), 16 * (height - 1));
            rightPos = new Vector2(16 * (width - 1), 16 * (height - 1));

            upShake = new Vector2(0, 0);
            leftShake = new Vector2(0, 0);
            downShake = new Vector2(0, 0);
            rightShake = new Vector2(0, 0);
        }

        public void Update(float dt) 
        {
            animTime += dt;

            if (upFilled == 4) {
                // TODO: drive particle animations
            }

            if (leftFilled == 4) {
                // ditto
            }

            if (downFilled == 4) {
                // ditto
            }

            if (rightFilled == 4) {
                // ditto
            }

        }

        // returns true if it's time to do a dash.
        public bool ConfirmKeyPressed(string key) {
            if (key == "Up") {
                upFilled++;
                bool dash = upFilled >= 5;
                if (dash) upFilled = 0;
                return dash;
            } else if (key == "Left") {
                leftFilled++;
                bool dash = leftFilled >= 5;
                if (dash) leftFilled = 0;
                return dash;
            } else if (key == "Down") {
                downFilled++;
                bool dash = downFilled >= 5;
                if (dash) downFilled = 0;
                return dash;
            } else if (key == "Right") {
                rightFilled++;
                bool dash = rightFilled >= 5;
                if (dash) rightFilled = 0;
                return dash;
            } else {
                Console.WriteLine("What have you done?!");
                return false;
            }
        }

        public void Draw()
        {
            Program.renderer.DrawImage("Health" + Program.player.hp.ToString(), hpPos);

            // draw under
            Program.renderer.DrawImage("Filled" + upFilled.ToString(), upPos + upShake);
            Program.renderer.DrawImage("Filled" + leftFilled.ToString(), leftPos + leftShake);
            Program.renderer.DrawImage("Filled" + downFilled.ToString(), downPos + downShake);
            Program.renderer.DrawImage("Filled" + rightFilled.ToString(), rightPos + rightShake);
            
            // draw overlays
            Program.renderer.DrawImage("Up", upPos + upShake);
            Program.renderer.DrawImage("Left", leftPos + leftShake);
            Program.renderer.DrawImage("Down", downPos + downShake);
            Program.renderer.DrawImage("Right", rightPos + rightShake);
        }

    }
}
