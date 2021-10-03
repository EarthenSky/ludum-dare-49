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
        
        public static Vector2 UP = new Vector2(16 * 0, 16 * -1);
        public static Vector2 LEFT = new Vector2(16 * -1, 16 * 0);
        public static Vector2 DOWN = new Vector2(16 * 0, 16 * 1);
        public static Vector2 RIGHT = new Vector2(16 * 1, 16 * 0);

        public Vector2 pos = new Vector2(16 * 2, 16 * 2);

        private string current_frame = "Player1";
        private float animTime = 0f;

        private float stepAnimTime = 0f;
        private string direction = "None";
        private int stepFrame = 0;
        private bool stopMovement = false;
        private int dashSize = 1;

        public int hp = 4;

        public Player() 
        {

        }

        public void TakeDamage() {
            hp -= 1;
            // do red particles when taking damage

            if (hp == 0) {
                Program.level.gameComplete = true;
                Program.level.didWin = false;
            }
        }

        private void PerformDash(Vector2 transform) {
            Vector2 currentLocation = pos;
            for (int i = 1; i <= 3; i++) {
                currentLocation += transform;
                var enemy = Program.level.GetEnemy(currentLocation);
                if (enemy == null) continue;

                enemy.RecieveAttack();
            }

        }

        // Take damage if there is an enemy there
        private void StepInto(Vector2 targetLocation) {
            var enemy = Program.level.GetEnemy(targetLocation);
            if (enemy == null) return;

            enemy.RecieveAttack();
            this.TakeDamage();
        }

        private void ProcessKey(string key, Vector2 transform) {
            direction = key;
            stepFrame = 0;

            bool isDash = Program.arrows.ConfirmKeyPressed(direction);
            if (isDash)
            {
                stopMovement = false;

                if (!Program.level.CanStep(pos, transform)) {
                    dashSize = 0;
                    return;
                } else if (!Program.level.CanStep(pos + transform, transform)) {
                    dashSize = 1;
                } else if (!Program.level.CanStep(pos + transform * 2, transform)) {
                    dashSize = 2;
                } else {
                    dashSize = 3;
                }
                
                PerformDash(transform);
            }
            else
            {
                stopMovement = !Program.level.CanStep(pos, transform); // technically we can replace stopMovement with dashSize.
                if (!stopMovement) StepInto(pos + transform);
            }
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
                ProcessKey("Up", UP);
            } 
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT) && direction == "None") {
                ProcessKey("Left", LEFT);
            } 
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN) && direction == "None") {
                ProcessKey("Down", DOWN);
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) && direction == "None") {
                ProcessKey("Right", RIGHT);
            }

            // This loop runs every 1 frames (assuming 60 fps)
            // TODO: use lerp instead
            stepAnimTime += dt;
            if (stepAnimTime > 0.016666f) {
                stepAnimTime -= 0.016666f;

                // Do actual movement
                if (direction == "Up") {
                    if (!stopMovement) pos += UP * dashSize / 4;
                    stepFrame++;
                } else if (direction == "Left") {
                    if (!stopMovement) pos += LEFT * dashSize / 4;
                    stepFrame++;
                } else if (direction == "Down") {
                    if (!stopMovement) pos += DOWN * dashSize / 4;
                    stepFrame++;
                } else if (direction == "Right") {
                    if (!stopMovement) pos += RIGHT * dashSize / 4;
                    stepFrame++;
                }

                // check for end of action
                if (direction != "None" && stepFrame >= FRAME_STEP_SIZE) {
                    direction = "None";
                    stopMovement = false;
                    dashSize = 1;
                }
            }
            
        }

        public void Draw()
        {
            Program.renderer.DrawImage(current_frame, pos);
        }

    }
}
