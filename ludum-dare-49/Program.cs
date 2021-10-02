using System;
using System.Numerics;

using Raylib_cs;

namespace ludum_dare_49
{
    static class Program
    {
        static Renderer renderer = new Renderer(4);

        public static void Main()
        {
            Raylib.InitWindow(640, 640, "Hello World");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKGRAY);

                renderer.DrawImage("Player1", new Vector2(0, 0));

                //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
