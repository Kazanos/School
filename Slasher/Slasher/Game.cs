using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slasher
{
    class Game
    {
        private int gold = 0;
        private int score = 0;
        private bool combat = false;
        private int bossesdefeated = 0;
        private List<Player> players = new List<Player>();
        private List<Tuple<int, int>> indexes = new List<Tuple<int, int>>() {};
        private List<Tuple<int, int>> visited = new List<Tuple<int, int>>() {};
        private int difficulty = 1;
        private Room[,] floor;
        private Random rnd = new Random();
        private int bossCount = 0;
        Tuple<int, int> current;


        public int Gold { get => gold; set => gold = value; }
        public int Score { get => score; set => score = value; }
        public bool Combat { get => combat; set => combat = value; }
        public int Bossesdefeated { get => bossesdefeated; set => bossesdefeated = value; }
        public int Difficulty { get => difficulty; set => difficulty = value; }
        public Room[,] Floor { get => floor; set => floor = value; }
        public int BossCount { get => bossCount; set => bossCount = value; }
        public List<Tuple<int, int>> Indexes { get => indexes; set => indexes = value; }
        public List<Tuple<int, int>> Visited1 { get => visited; set => visited = value; }
        internal List<Player> Players { get => players; set => players = value; }
        public Tuple<int, int> Current { get => current; set => current = value; }

        public Game()
        {
            GenerateFloor();
            current = indexes[rnd.Next(indexes.Count)];
            visited.Add(current);
            floor[current.Item1, current.Item2] = new Room("rooms/r0.txt", difficulty);
            for (int i = 0; i < indexes.Count; i++)
            {
                int v = indexes[i].Item1;
                int h = indexes[i].Item2;
                if (v + 1 < 9)
                {
                    if (floor[v+1,h].Type != "")
                    {
                        floor[v, h].Field[16, 8] = 1 + 4 * floor[v + 1, h].Field[0, 0];
                        floor[v + 1, h].Field[0, 8] = 3 + 4 * floor[v, h].Field[0, 0];
                    }
                }
                if (v - 1 >= 0)
                {
                    if (floor[v - 1, h].Type != "")
                    {
                        floor[v, h].Field[0, 8] = 3 + 4 * floor[v - 1, h].Field[0, 0];
                        floor[v - 1, h].Field[16, 8] = 1 + 4 * floor[v, h].Field[0, 0];
                    }
                }
                if (h + 1 < 9)
                {
                    if (floor[h + 1, h].Type != "")
                    {
                        floor[v, h].Field[8, 16] = 0 + 4 * floor[v, h + 1].Field[0, 0];
                        floor[v, h + 1].Field[8, 0] = 2 + 4 * floor[v, h].Field[0, 0];
                    }
                }
                if (h - 1 >= 0)
                {
                    if (floor[v + 1, h].Type != "")
                    {
                        floor[v, h].Field[8, 0] = 2 + 4 * floor[v, h - 1].Field[0, 0];
                        floor[v, h - 1].Field[8, 16] = 0 + 4 * floor[v, h].Field[0, 0];
                    }
                }
            }
        }

        //vygeneruje nahodnu mapu poschodia
        private void GenerateFloor()
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
                int dir = rnd.Next(0, 3);
                if (dir == 0)
                {
                    if (h + 1 < 9)
                    {
                        if (floor[v, h + 1].Type == "")
                        {
                            floor[v, h + 1] = new Room("rooms/r" + rnd.Next(30).ToString() + ".txt", difficulty);
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
                        if (floor[v + 1, h].Type == "")
                        {
                            floor[v + 1, h] = new Room("rooms/r" + rnd.Next(30).ToString() + ".txt", difficulty);
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
                        if (floor[v, h - 1].Type == "")
                        {
                            floor[v, h - 1] = new Room("rooms/r" + rnd.Next(30).ToString() + ".txt", difficulty);
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
                        if (floor[v - 1, h].Type == "")
                        {
                            floor[v - 1,h] = new Room("rooms/r" + rnd.Next(30).ToString() + ".txt", difficulty);
                            indexes.Add(new Tuple<int, int>(v - 1, h));
                            v -= 1;
                        }
                    }
                }
            }

            //generovanie boss miestnosti
            Tuple<int, int> target;
            while (bossCount < 4)
            {
                target = new Tuple<int, int>(-1,-1);
                Tuple<int, int> i = indexes[rnd.Next(indexes.Count)];
                v = i.Item1;
                h = i.Item2;
                int dir = rnd.Next(0, 3);
                if (dir == 0)
                {
                    if (h + 1 < 9)
                    {
                        if (floor[v, h + 1].Type == "")
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
                        if (floor[v + 1, h].Type == "")
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
                        if (floor[v, h - 1].Type == "")
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
                        if (floor[v - 1, h].Type == "")
                        {
                            target = new Tuple<int, int>(v - 1, h);
                        }
                    }
                }
                if (target.ToString() != "(-1,-1)")
                {
                    floor[target.Item1, target.Item2] = new Room("rooms/b" + rnd.Next(10).ToString() + ".txt", difficulty);
                    bossCount += 1;
                }
            }
        }

        public void Move()
        {

        }

        public void Draw()
        {

        }
    }

    
}
