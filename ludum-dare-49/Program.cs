using System;
using System.Numerics;

using Raylib_cs;

namespace ludum_dare_49
{
    static class Program
    {
        public static Renderer renderer = new Renderer(4);

        public static Level level = new Level();
        public static Player player = new Player();
        public static Arrows arrows = new Arrows();

        public static void Main()
        {
            Start();

            while (!Raylib.WindowShouldClose())
            {
                Update(Raylib.GetFrameTime());

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKGRAY);

                Draw();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        private static void Start() {
            Raylib.InitWindow(640, 640, "Moss Moon");
            Raylib.SetTargetFPS(60);
        }

        private static void Update(float dt) {
            level.Update(dt);
            player.Update(dt);
            arrows.Update(dt);
        }

        private static void Draw() {
            level.Draw();
            player.Draw();
            arrows.Draw();
            
            //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);
        }
    }
}
