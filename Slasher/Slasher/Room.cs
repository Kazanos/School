using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Slasher
{
    class Room
    {
        bool visited = false;
        int[,] field = new int[17,17];
        List<Item> items = new List<Item>();
        List<Creature> enemies = new List<Creature>();
        string name;
        string type = "";

        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int[,] Field { get => field; set => field = value; }
        public bool Visited { get => visited; set => visited = value; }
        internal List<Item> Items { get => items; set => items = value; }
        internal List<Creature> Enemies { get => enemies; set => enemies = value; }

        public Room()
        {
        }

        public Room(string n, int diff)
        {
            name = n;
            if (name[7] == 'b')
            {
                type = "special";
            }
            else if (name[7] == 'r')
            {
                type = "room";
            }
            //nacita mapu miestnosti zo suboru
            string[] numbers = new string[] { };
            string[] info = new string[] { };
            using (StreamReader file = new StreamReader(name))
            {
                for (int i = 0; i < 17; i++)
                {
                    numbers = file.ReadLine().Split();
                    for (int j = 0; j < 17; j++)
                    {
                        field[i, j] = int.Parse(numbers[j]);
                    }
                }

                string line = file.ReadLine();

                if (!file.EndOfStream)
                {
                    info = file.ReadLine().Split();

                    for (int i = 0; i < info.Length / 3; i++)
                    {
                        if (info[i * 3] == "0")
                        {
                            enemies.Add(new Creature(int.Parse(info[i * 3 + 1]) * (Form1.Resx / 1920), int.Parse(info[i * 3 + 2]) * (Form1.Resy / 1080), diff));
                        }
                        else if (info[i * 3] == "1")
                        {
                            enemies.Add(new Boss(int.Parse(info[i * 3 + 1]) * (Form1.Resx / 1920), int.Parse(info[i * 3 + 2]) * (Form1.Resy / 1080), diff));
                        }
                    }
                }
            }
        }
    }
}
