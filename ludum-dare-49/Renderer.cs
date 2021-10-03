using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace ludum_dare_49
{
    class Renderer
    {
        private Texture2D atlas;
        private Image imageAtlas;

        private int scaling;

        private Dictionary<string, Rectangle> TextureMap = new Dictionary<string, Rectangle> {
            { "Logo", new Rectangle(0, 0, 16, 16) },
            { "Player1", new Rectangle(16 * 3, 16 * 1, 16, 16) },
            { "Player2", new Rectangle(16 * 4, 16 * 1, 16, 16) },
            { "GnomeThing", new Rectangle(16 * 3, 16 * 0, 16, 16) },
            { "TinyUndead", new Rectangle(16 * 4, 16 * 0, 16, 16) },
            { "Ground1", new Rectangle(16 * 0, 16 * 1, 16, 16) },
            { "Ground2", new Rectangle(16 * 1, 16 * 1, 16, 16) },
            { "Ground3", new Rectangle(16 * 2, 16 * 1, 16, 16) },
            { "Ground4", new Rectangle(16 * 0, 16 * 3, 16, 16) },
            { "Wall1", new Rectangle(16 * 2, 16 * 0, 16, 16) },
            { "Wall2", new Rectangle(16 * 5, 16 * 0, 16, 16) },
            { "WallInvisible", new Rectangle(16 * 5, 16 * 1, 16, 16) },
            { "Left", new Rectangle(16 * 0, 16 * 2, 16, 16) },
            { "Right", new Rectangle(16 * 1, 16 * 2, 16, 16) },
            { "Up", new Rectangle(16 * 2, 16 * 2, 16, 16) },
            { "Down", new Rectangle(16 * 3, 16 * 2, 16, 16) },
            { "Filled0", new Rectangle(16 * 4, 16 * 2, 16, 16) },
            { "Filled1", new Rectangle(16 * 4, 16 * 3, 16, 16) },
            { "Filled2", new Rectangle(16 * 4, 16 * 4, 16, 16) },
            { "Filled3", new Rectangle(16 * 4, 16 * 5, 16, 16) },
            { "Filled4", new Rectangle(16 * 4, 16 * 6, 16, 16) },
            { "Health0", new Rectangle(16 * 6, 16 * 6, 16, 16*2) },
            { "Health1", new Rectangle(16 * 7, 16 * 4, 16, 16*2) },
            { "Health2", new Rectangle(16 * 6, 16 * 4, 16, 16*2) },
            { "Health3", new Rectangle(16 * 7, 16 * 2, 16, 16*2) },
            { "Health4", new Rectangle(16 * 6, 16 * 2, 16, 16*2) },
        };

        public Renderer(int scaling) {
            this.scaling = scaling;
            imageAtlas = Raylib.LoadImage("assets.png");
            Raylib.ImageResizeNN(ref imageAtlas, imageAtlas.width * scaling, imageAtlas.height * scaling);

            // This is a GPU atlas... hopefully this doesn't cause problems?
            atlas = Raylib.LoadTextureFromImage(imageAtlas);  // accessing bad memory...?

            foreach (var pair in TextureMap) {
                var val = pair.Value;
                TextureMap[pair.Key] = new Rectangle(val.x * scaling, val.y * scaling, val.width * scaling, val.height * scaling);
            }
        }

        public Texture2D LoadImage(string file) {
            Image img = Raylib.LoadImage(file);
            Raylib.ImageResizeNN(ref img, img.width * scaling, img.height * scaling);
            return Raylib.LoadTextureFromImage(img);
        }

        public void DrawImage(string imageName, Vector2 position) {
            var rect = TextureMap[imageName];
            Raylib.DrawTextureRec(atlas, rect, position * scaling, Color.WHITE);
        }
    }
}
