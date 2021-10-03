using System;
using System.Numerics;

using Raylib_cs;

namespace ludum_dare_49
{
    static class Program
    {
        public static Renderer renderer;

        public static Intro intro;

        public static Level level ;
        public static Player player;
        public static Arrows arrows ;

        public static Random rand;

        public static void Main()
        {
            Raylib.InitWindow(640, 640, "Moss Moon");
            Raylib.SetTargetFPS(60);
            rand = new Random(Guid.NewGuid().GetHashCode());

            // --------------------------------------------------- //
            // Creating Objects:
            renderer = new Renderer(4);

            intro = new Intro();

            level = new Level();
            player = new Player();
            arrows = new Arrows();

            // --------------------------------------------------- //

            while (!intro.IsDone() && !Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) && !Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                intro.Update(Raylib.GetFrameTime());

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKGRAY);

                intro.Draw();

                Raylib.EndDrawing();

                if (Raylib.WindowShouldClose()) {
                    Raylib.CloseWindow();
                    return;
                }
            }

            GameStart();

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

        private static void GameStart() {
            
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
