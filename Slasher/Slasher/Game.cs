using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Slasher
{
    class Game
    {
        int gold = 0;
        int score = 0;
        bool combat = false;
        int bossesdefeated = 0;
        List<Player> players = new List<Player>();
        List<Tuple<int, int>> indexes = new List<Tuple<int, int>>() {};
        List<Tuple<int, int>> visible = new List<Tuple<int, int>>() {};
        static int difficulty = 1;
        Room[,] floor;
        int bossCount = 0;
        Tuple<int, int> current;
        List<Projectile> projectiles = new List<Projectile>();


        public int Gold { get => gold; set => gold = value; }
        public int Score { get => score; set => score = value; }
        public bool Combat { get => combat; set => combat = value; }
        public int Bossesdefeated { get => bossesdefeated; set => bossesdefeated = value; }
        public static int Difficulty { get => difficulty; set => difficulty = value; }
        public Room[,] Floor { get => floor; set => floor = value; }
        public int BossCount { get => bossCount; set => bossCount = value; }
        public List<Tuple<int, int>> Indexes { get => indexes; set => indexes = value; }
        public List<Tuple<int, int>> Visible { get => visible; set => visible = value; }
        public List<Player> Players { get => players; set => players = value; }
        public Tuple<int, int> Current { get => current; set => current = value; }
        public List<Projectile> Projectiles { get => projectiles; set => projectiles = value; }

        public Game()
        {
            GenerateFloor();
            current = indexes[Form1.Random.Next(indexes.Count)];
            visible.Add(current);
            floor[current.Item1, current.Item2] = new Room("rooms/r0.txt");

            //prida sipky na miesta kde sa da presjt do vedlajsej miestnosti
            for (int i = 0; i < indexes.Count; i++)
            {
                int v = indexes[i].Item1;
                int h = indexes[i].Item2;
                if (v + 1 < 9)
                {
                    if (floor[v + 1, h].Field[0,0] != 0)
                    {
                        floor[v, h].Field[16, 8] = 1 + 4 * floor[v + 1, h].Field[0, 0];
                        floor[v + 1, h].Field[0, 8] = 3 + 4 * floor[v, h].Field[0, 0];
                    }
                }
                if (v - 1 >= 0)
                {
                    if (floor[v - 1, h].Field[0, 0] != 0)
                    {
                        floor[v, h].Field[0, 8] = 3 + 4 * floor[v - 1, h].Field[0, 0];
                        floor[v - 1, h].Field[16, 8] = 1 + 4 * floor[v, h].Field[0, 0];
                    }
                }
                if (h + 1 < 9)
                {
                    if (floor[v, h + 1].Field[0, 0] != 0)
                    {
                        floor[v, h].Field[8, 16] = 0 + 4 * floor[v, h + 1].Field[0, 0];
                        floor[v, h + 1].Field[8, 0] = 2 + 4 * floor[v, h].Field[0, 0];
                    }
                }
                if (h - 1 >= 0)
                {
                    if (floor[v, h - 1].Field[0, 0] != 0)
                    {
                        floor[v, h].Field[8, 0] = 2 + 4 * floor[v, h - 1].Field[0, 0];
                        floor[v, h - 1].Field[8, 16] = 0 + 4 * floor[v, h].Field[0, 0];
                    }
                }
            }
        }

        //vygeneruje nahodnu mapu poschodia
        internal void GenerateFloor()
        {
            floor = new Room[9,9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    floor[i, j] = new Room();
                }
            }
            indexes = new List<Tuple<int, int>>();

            int h = 5;
            int v = 5;

            //generovanie normalnych miestnosti
            for (int i = 0; i < 8 + difficulty * 2; i++)
            {
                int dir = Form1.Random.Next(0, 3);
                if (dir == 0)
                {
                    if (h + 1 < 9)
                    {
                        if (floor[v, h + 1].Type == "empty")
                        {
                            floor[v, h + 1] = new Room("rooms/r" + Form1.Random.Next(30).ToString() + ".txt");
                            indexes.Add(new Tuple<int, int>(v, h + 1));
                            h += 1;
                        }
                        else dir += 1;
                    }
                    else dir += 1;
                }
                if (dir == 0)
                {
                    if (v + 1 < 9)
                    {
                        if (floor[v + 1, h].Type == "empty")
                        {
                            floor[v + 1, h] = new Room("rooms/r" + Form1.Random.Next(30).ToString() + ".txt");
                            indexes.Add(new Tuple<int, int>(v + 1, h));
                            v += 1;
                        }
                        else dir += 1;
                    }
                    else dir += 1;
                }
                if (dir == 0)
                {
                    if (h - 1 >= 0)
                    {
                        if (floor[v, h - 1].Type == "empty")
                        {
                            floor[v, h - 1] = new Room("rooms/r" + Form1.Random.Next(30).ToString() + ".txt");
                            indexes.Add(new Tuple<int, int>(v, h - 1));
                            h -= 1;
                        }
                        else dir += 1;
                    }
                    else dir += 1;
                }
                if (dir == 0)
                {
                    if (v - 1 >= 0)
                    {
                        if (floor[v - 1, h].Type == "empty")
                        {
                            floor[v - 1,h] = new Room("rooms/r" + Form1.Random.Next(30).ToString() + ".txt");
                            indexes.Add(new Tuple<int, int>(v - 1, h));
                            v -= 1;
                        }
                    }
                }
            }

            //generovanie boss miestnosti
            bossCount = 0;
            Tuple<int, int> target;
            while (bossCount < 4)
            {
                target = new Tuple<int, int>(-1,-1);
                Tuple<int, int> i = indexes[Form1.Random.Next(indexes.Count - 1)];
                v = i.Item1;
                h = i.Item2;
                int dir = Form1.Random.Next(0, 3);
                if (dir == 0)
                {
                    if (h + 1 < 9)
                    {
                        if (floor[v, h + 1].Type == "empty")
                        {
                            target = new Tuple<int, int>(v, h + 1);
                        }
                        else dir += 1;
                    }
                    else dir += 1;
                }

                if (dir == 1)
                {
                    if (v + 1 < 9)
                    {
                        if (floor[v + 1, h].Type == "empty")
                        {
                            target = new Tuple<int, int>(v + 1, h);
                        }
                        else dir += 1;
                    }
                    else dir += 1;
                }
                if (dir == 2)
                {
                    if (h - 1 >= 0)
                    {
                        if (floor[v, h - 1].Type == "empty")
                        {
                            target = new Tuple<int, int>(v, h - 1);
                        }
                        else dir += 1;
                    }
                    else dir += 1;
                }
                if (dir == 3)
                {
                    if (v - 1 >= 0)
                    {
                        if (floor[v - 1, h].Type == "empty")
                        {
                            target = new Tuple<int, int>(v - 1, h);
                        }
                    }
                }
                if (target.ToString() != "(-1, -1)")
                {
                    floor[target.Item1, target.Item2] = new Room("rooms/b" + Form1.Random.Next(10).ToString() + ".txt");
                    bossCount += 1;
                }
            }
        }

        //vsetky cinnosti pohyblivych veci
        public void Move()
        {
            foreach (Player p in players)
            {
                if (p.Direction.ToString() != "(0, 0)")
                {
                    p.Move();
                }
                if (p.Shotdirection.ToString() != "(0, 0)")
                {
                    p.Shoot();
                }               
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Move();
            }
            foreach (Creature c in floor[current.Item1, current.Item2].Enemies)
            {
                c.Move();
            }
        }

        //zmaze obrazovku a zavola vsetky kresliace funkcie
        public void Draw()
        {
            Form1.G.Clear(Color.Black);
            floor[current.Item1, current.Item2].Draw();
            foreach (Item i in floor[current.Item1, current.Item2].Items)
            {
                i.Draw();
            }
            foreach (Player p in players)
            {
                p.Draw();
            }
            foreach (Projectile p in projectiles)
            {
                p.Draw();
            }
            DrawingFunctions.DrawMinimap();
            DrawingFunctions.DrawStats();
        }
    }

    
}
