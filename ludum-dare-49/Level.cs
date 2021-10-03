using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace ludum_dare_49
{
    // This class manages the enemies in the level & the ground + walls.
    class Level
    {
        // TODO: implement the little story blurb at the beginning

        public float trueTime = 0;
        public int time = 0;

        public int width = 640 / 4 / 16;
        public int height = 640 / 4 / 16;

        private List<string> terrain = new List<string>();

        public Level() {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    int choice = rand.Next(8);
                    if (choice <= 4) {
                        terrain.Add("Ground1"); // TODO: make sure these are string references
                    } else if (choice == 5) { 
                        terrain.Add("Ground2");
                    } else if (choice == 6) { 
                        terrain.Add("Ground3");
                    } else if (choice == 7) { 
                        terrain.Add("Ground4");
                    }
                }
            }

            for (int x = 0; x < width; x++) {
                terrain[x] = "Wall1";
                terrain[x + (height-1) * width] = "Wall2";
            }

            terrain[width - 4 + (height - 2) * width] = "Wall1";
            terrain[width - 3 + (height - 2) * width] = "Wall2";
            terrain[width - 2 + (height - 2) * width] = "WallInvisible";
            terrain[width - 1 + (height - 2) * width] = "Wall2";

            terrain[width - 4 + (height - 1) * width] = "Wall2";
            terrain[width - 3 + (height-1) * width] = "WallInvisible";
            terrain[width - 2 + (height-1) * width] = "WallInvisible";
            terrain[width - 1 + (height-1) * width] = "WallInvisible";
        }

        public bool CanStep(Vector2 pos, Vector2 transform) {
            // check for left & right walls
            if ((pos.X/16 == 0 && transform.X/16 < 0) || 
                (pos.X/16 == width - 1 && transform.X/16 > 0))
                return false;
            
            // check for actual wall tiles.
            int index = (int)(pos.X+transform.X)/16 + (int)(pos.Y+transform.Y)/16 * width;
            if (index < terrain.Count && index >= 0)
                return !terrain[index].StartsWith("Wall");
            else
                return false;
        }

        public IEnemy GetEnemy(Vector2 position) {
            return null;
        }

        public void Update(float dt) {
            // Enemeis spawn at pre-defined times, but random locations.

            trueTime += dt;
            if (trueTime >= 1f) {
                time += 1;
                trueTime -= 1f;
            }
        }
        
        // game ends at 7:15am -> ~6-7 mins total

        public void Draw() {
            // TODO: add walls, but also create this as a single image, then load to the gpu.
            int i = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Program.renderer.DrawImage(terrain[i], new Vector2(16 * x, 16 * y));
                    i++;
                }
            }

            string timeStr = (1 + (time / 60)).ToString() + ":" + (time % 60 < 10 ? "0" : "") + (time % 60).ToString() + " AM";
            Raylib.DrawText(timeStr, 16 * 4 * (6) + 4, 0, 16 * 4, Color.WHITE);
        }
    }
}
