using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ludum_dare_49
{
    interface IEnemy
    {
        // usually kills
        void RecieveAttack();
        bool IsDead();

        void Update(float dt);
        void Draw();

        Vector2 GetPosition();
        Vector2 GetTargetPosition();
    }
}
