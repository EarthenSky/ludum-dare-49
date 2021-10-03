using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace ludum_dare_49
{
    class GnomeThing : IEnemy
    {
        private Vector2 pos;
        private bool isDead = false;

        public GnomeThing(Vector2 start) {
            pos = start;
        }

        // usually kills
        public void RecieveAttack() {
            isDead = true;
        }

        public bool IsDead() {
            return isDead;
        }

        public void Update(float dt) { 

        }

        public void Draw() {
            Program.renderer.DrawImage("GnomeThing", pos);
        }

        public Vector2 GetPosition() { 
            return pos;
        }
    }
}
