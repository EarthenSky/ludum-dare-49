using System;
using System.Numerics;

using Raylib_cs;

namespace ludum_dare_49
{
    static class Program
    {
        public static Renderer renderer = new Renderer(4);
        public static Level level = new Level();

        public static void Main()
        {
            Raylib.InitWindow(640, 640, "Moss Moon");

            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKGRAY);

                level.Draw();
                renderer.DrawImage("Player1", new Vector2(0, 0));

                //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
