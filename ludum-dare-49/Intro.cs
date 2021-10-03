using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace ludum_dare_49
{
    class Intro
    {
        private Texture2D bg;
        private Texture2D majin;
        private Texture2D text1;
        private Texture2D text2;

        private int majinX = -640 *2 /4;
        private int majinY = 640 *2 /4;

        private int majinTargetX = -640 * 2 / 4;
        private int majinTargetY = 640 * 2 / 4;

        private Color text1Color = new Color(0, 0, 0, 0);
        private Color text1TargetColor = new Color(0, 0, 0, 0);
        private Color text2Color = new Color(0, 0, 0, 0);
        private Color text2TargetColor = new Color(0, 0, 0, 0);

        private float currentTime = 0f;
        private int currentStage = 0;

        private bool isDone = false;

        public Intro() {
            // TODO: load images here.
            bg = Program.renderer.LoadImage("bg.png");
            majin = Program.renderer.LoadImage("witch.png");
            text1 = Program.renderer.LoadImage("text1.png");
            text2 = Program.renderer.LoadImage("text2.png");
        }

        public bool IsDone() {
            return isDone;
        }

        public void Update(float dt) {
            currentTime += dt;

            if (currentTime > 1f) {
                currentTime -= 1f;
                
                // do action when time
                switch (currentStage)
                {
                    case 0:
                        majinTargetX = 0;
                        majinTargetY = 0;
                        break;
                    case 1:
                        text1TargetColor = Color.WHITE;
                        break;
                    case 3:
                        text2TargetColor = Color.WHITE;
                        break;
                    case 5:
                        // TODO: fade to black
                        break;
                    case 6:
                        isDone = true;
                        break;
                    default:
                        break;
                }

                currentStage++;
            }

            // moves the majin to it's target, always
            majinX = (majinX*6 + majinTargetX) / 7;
            majinY = (majinY*6 + majinTargetY) / 7;

            text1Color = new Color(
                (text1Color.r*2 + text1TargetColor.r) / 3,
                (text1Color.g*2 + text1TargetColor.g) / 3,
                (text1Color.b*2 + text1TargetColor.b) / 3,
                (text1Color.a*2 + text1TargetColor.a) / 3);
            text2Color = new Color(
                (text2Color.r*3 + text2TargetColor.r) / 4,
                (text2Color.g*3 + text2TargetColor.g) / 4,
                (text2Color.b*3 + text2TargetColor.b) / 4, (text2Color.a*3 + text2TargetColor.a) / 4);
        }

        public void Draw() {
            Raylib.DrawTexture(bg, 0, 0, Color.WHITE);

            Raylib.DrawTexture(majin, majinX, majinY, Color.WHITE);
            
            Raylib.DrawTexture(text1, 0, 0, text1Color);
            Raylib.DrawTexture(text2, 0, 0, text2Color);
        }
    }
}
