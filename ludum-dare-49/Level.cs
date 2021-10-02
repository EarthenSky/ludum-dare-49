using System;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ludum_dare_49
{
    // This class manages the enemies in the level & the ground + walls.
    class Level
    {
        // TODO: implement the little story blurb at the beginning

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
                terrain[x + (height-1) * width] = "Wall1";
            }

            terrain[width - 3 + (height - 2) * width] = "Wall1";
            terrain[width - 2 + (height - 2) * width] = "Up";
            terrain[width - 1 + (height - 2) * width] = "Wall1";
            
            terrain[width - 3 + (height-1) * width] = "Left";
            terrain[width - 2 + (height-1) * width] = "Down";
            terrain[width - 1 + (height-1) * width] = "Right";

        }

        void Update() { 
        
        }
        
        public void Draw() {
            // TODO: add walls, but also create this as a single image, then load to the gpu.
            int i = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Program.renderer.DrawImage(terrain[i], new Vector2(16 * x, 16 * y));
                    i++;
                }
            }
        }
    }
}
