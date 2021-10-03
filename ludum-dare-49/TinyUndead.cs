using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace ludum_dare_49
{
    class TinyUndead : IEnemy
    {
        public static Vector2 UP = new Vector2(16 * 0, 16 * -1);
        public static Vector2 LEFT = new Vector2(16 * -1, 16 * 0);
        public static Vector2 DOWN = new Vector2(16 * 0, 16 * 1);
        public static Vector2 RIGHT = new Vector2(16 * 1, 16 * 0);

        private Vector2 pos;
        private Vector2 target;
        private bool isDead = false;

        private float moveTimer = 0f;
        private float moveTimeLimit = 0.5f; // take a step every second

        private const int FRAME_STEP_SIZE = 4;

        private float stepAnimTime = 0f;
        private string direction = "None";
        private int stepFrame = 0;

        public TinyUndead(Vector2 start)
        {
            pos = start;
            target = start;
        }

        // usually kills
        public void RecieveAttack()
        {
            isDead = true;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void Update(float dt)
        {
            if (isDead) return;

            moveTimer += dt;
            if (moveTimer > moveTimeLimit)
            {
                moveTimer -= moveTimeLimit;
                MoveToPlayer();
            }

            // This loop runs every 1 frames (assuming 60 fps)
            stepAnimTime += dt;
            while (stepAnimTime > 0.016666f)
            {
                stepAnimTime -= 0.016666f;

                // Do actual movement
                if (direction == "Up")
                {
                    pos += UP * 1 / 4;
                    stepFrame++;
                }
                else if (direction == "Left")
                {
                    pos += LEFT * 1 / 4;
                    stepFrame++;
                }
                else if (direction == "Down")
                {
                    pos += DOWN * 1 / 4;
                    stepFrame++;
                }
                else if (direction == "Right")
                {
                    pos += RIGHT * 1 / 4;
                    stepFrame++;
                }

                // Check distance after each move
                if (Vector2.Distance(Program.player.pos, pos) < 16 / 2)
                {
                    this.RecieveAttack();
                    if (Program.player.hp > 0)
                        Program.player.TakeDamage(); // TODO: hopefully player can't take double damage?
                }

                // check for end of action
                if (direction != "None" && stepFrame >= FRAME_STEP_SIZE)
                {
                    direction = "None";
                    stepFrame = 0;
                }
            }
        }

        public void Draw()
        {
            Program.renderer.DrawImage("TinyUndead", pos);
        }

        public Vector2 GetPosition()
        {
            return pos;
        }

        public Vector2 GetTargetPosition()
        {
            return target;
        }

        // ----------------------------------------- //

        private void MoveToPlayer()
        {
            Vector2 player = new Vector2(MathF.Round(Program.player.pos.X / 16) * 16, MathF.Round(Program.player.pos.Y / 16) * 16); // normalized
            Vector2 difference = player - pos;
            bool moveX = Program.rand.Next(2) == 0;

            // hopefully this gets replaced with a jump table.
            if (moveX)
            {
                if (difference.X > 0)
                {
                    direction = "Right";
                }
                else if (difference.X < 0)
                {
                    direction = "Left";
                }
                else // move Y
                {
                    if (difference.Y > 0)
                    {
                        direction = "Down";
                    }
                    else if (difference.Y < 0)
                    {
                        direction = "Up";
                    }
                    else
                    {
                        direction = "None";
                    }
                }

            }
            else
            {
                if (difference.Y > 0)
                {
                    direction = "Down";
                }
                else if (difference.Y < 0)
                {
                    direction = "Up";
                }
                else // move X
                {
                    if (difference.X > 0)
                    {
                        direction = "Right";
                    }
                    else if (difference.X < 0)
                    {
                        direction = "Left";
                    }
                    else
                    {
                        direction = "None";
                    }
                }
            }

            // Update target position
            if (direction == "Right")
                target = pos + RIGHT;
            else if (direction == "Left")
                target = pos + LEFT;
            else if (direction == "Up")
                target = pos + UP;
            else if (direction == "Down")
                target = pos + DOWN;
            else
                target = pos;

            // Wait if the place you chose to move is targeted.
            if (Program.level.IsTargeted(target, this))
            {
                target = pos;
                direction = "None";
            }

        }
    }
}
