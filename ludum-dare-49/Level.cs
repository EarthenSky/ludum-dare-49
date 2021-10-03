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
        public float trueTime = 0f;
        public int time = 0;

        public int width = 640 / 4 / 16;
        public int height = 640 / 4 / 16;

        private List<string> terrain = new List<string>();
        private List<IEnemy> enemies = new List<IEnemy>();

        private Queue<IEnemy> spawnQueue = new Queue<IEnemy>();
        private float queueTimer = 0f;

        public Level() {
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    int choice = Program.rand.Next(8);
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
                terrain[x + (height - 1) * width] = "Wall2";
            }

            terrain[width - 4 + (height - 2) * width] = "Wall1";
            terrain[width - 3 + (height - 2) * width] = "Wall2";
            terrain[width - 2 + (height - 2) * width] = "WallInvisible";
            terrain[width - 1 + (height - 2) * width] = "Wall2";

            terrain[width - 4 + (height - 1) * width] = "Wall2";
            terrain[width - 3 + (height - 1) * width] = "WallInvisible";
            terrain[width - 2 + (height - 1) * width] = "WallInvisible";
            terrain[width - 1 + (height - 1) * width] = "WallInvisible";
        }

        public bool CanStep(Vector2 pos, Vector2 transform) {
            // check for left & right walls
            if ((pos.X / 16 == 0 && transform.X / 16 < 0) ||
                (pos.X / 16 == width - 1 && transform.X / 16 > 0))
                return false;

            // check for actual wall tiles.
            return !HasWall(pos + transform);
        }

        public bool HasWall(Vector2 pos) {
            int index = (int)(pos.X) / 16 + (int)(pos.Y) / 16 * width;
            if (index < terrain.Count && index >= 0)
                return terrain[index].StartsWith("Wall");
            else
                return true;
        }

        // pass a divisible by 16 location
        public IEnemy GetEnemy(Vector2 position) {
            foreach (var enemy in enemies) {
                Vector2 enemyPos = enemy.GetPosition();
                int x = (int)Math.Round(enemyPos.X / 16f);
                int y = (int)Math.Round(enemyPos.Y / 16f);

                if (position.X / 16 == x && position.Y / 16 == y)
                    return enemy;
            }

            // no enemy was found.
            return null;
        }

        // TODO: only spawn an enemy is there is fewer than 16 on screen. Slow down spawning after 8 are on screen.
        private void SpawnEnemy(string enemy) {
            while (true) {
                int x = Program.rand.Next(width);
                int y = Program.rand.Next(height - 2) + 1;
                Vector2 loc = new Vector2(x * 16, y * 16);

                // TODO: spawn enemy 2 away from the player & only at 2 away max from edges.

                var index = x + y * width;
                if (index > 0 && index < terrain.Count && !HasWall(loc) && GetEnemy(loc) == null) {
                    spawnQueue.Enqueue(new GnomeThing(loc)); // space has no wall or enemy, place enemy
                    return;
                }
            }
        }

        private void WinGame() {

        }

        // make the first few enemies "random" so it doesn't feel the same.
        private void TrySpawning(int time) {
            // spawn a wave
            if (time % 60 == 0) {
                WaveN(time / 60);
            }

            //Console.WriteLine(time.ToString());

            switch (time) {
                case 1:
                    break;
                case 2:
                    SpawnEnemy("GnomeThing"); // rand
                    break;
                case 6:
                    SpawnEnemy("GnomeThing"); // rand
                    break;
                case 10:
                    SpawnEnemy("GnomeThing"); // rand
                    break;
                case 11:
                    SpawnEnemy("GnomeThing"); // rand
                    break;
                default:
                    break;
            }

            // spawn extra enemies if there are none.
            if (enemies.Count == 0 && spawnQueue.Count == 0) {
                SpawnEnemy("GnomeThing"); // rand
            }

        }
        
        private void WaveN(int n) {
            if (n == 1) {
                for (int i = 0; i < 3; i++) {
                    SpawnEnemy("GnomeThing");
                }
            } else if (n == 2) {
                for (int i = 0; i < 3; i++) {
                    SpawnEnemy("GnomeThing");
                }
            } else if (n == 3) {
                for (int i = 0; i < 4; i++) {
                    SpawnEnemy("GnomeThing");
                }
            } else if (n == 4) {
                for (int i = 0; i < 4; i++) {
                    SpawnEnemy("GnomeThing");
                }
            } else if (n == 5) {
                for (int i = 0; i < 5; i++) {
                    SpawnEnemy("GnomeThing");
                }
            } else if (n == 6) {
                for (int i = 0; i < 5; i++) {
                    SpawnEnemy("GnomeThing");
                }
            } else if (n == 7) {
                for (int i = 0; i < 6; i++) {
                    SpawnEnemy("GnomeThing");
                }
            } 
        }

        public void Update(float dt) {
            // Enemeis spawn at pre-defined times, but random locations.

            trueTime += dt;
            if (trueTime >= 1f) {
                time += 1;
                trueTime -= 1f;
            
                // Do something every time the time changes.

                TrySpawning(time);

                if (time >= 7 * 60 + 15) {
                    WinGame();
                }
            }

            // pop the queue 1 time per second
            queueTimer += dt;
            float limit = enemies.Count > 8 ? 4f : 1f; // slows spawn rate after 8.
            if (queueTimer > limit) {
                // We can never have more than 16 enemies on screen.
                if (enemies.Count < 16 && spawnQueue.Count != 0) {
                    IEnemy e = spawnQueue.Dequeue();
                    enemies.Add(e);
                    Console.WriteLine(e.GetPosition().ToString());
                    queueTimer = 0; // fully resets after each spawn
                }
            }

            foreach (var enemy in enemies) {
                enemy.Update(dt);
            }

            // Cleanup dead enemies
            enemies.RemoveAll(e => e.IsDead());
        }

        // game ends at 7:15am -> ~6-7 mins total

        public void Draw() {
            // TODO: create this as a single image, then load to the gpu.

            int i = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Program.renderer.DrawImage(terrain[i], new Vector2(16 * x, 16 * y));
                    i++;
                }
            }

            string timeStr = (1 + (time / 60)).ToString() + ":" + (time % 60 < 10 ? "0" : "") + (time % 60).ToString() + " AM";
            Raylib.DrawText(timeStr, 16 * 4 * (6) + 4, 0, 16 * 4, Color.WHITE);

            foreach (var enemy in enemies) {
                enemy.Draw();
            }
        }
    }
}
