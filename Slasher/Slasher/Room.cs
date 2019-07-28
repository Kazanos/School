using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Slasher
{
    class Room
    {
        bool visited = false;
        int[,] field = new int[17,17];
        List<Item> items = new List<Item>();
        List<Creature> enemies = new List<Creature>();
        string name;
        string type = "empty";

        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int[,] Field { get => field; set => field = value; }
        public bool Visited { get => visited; set => visited = value; }
        internal List<Item> Items { get => items; set => items = value; }
        internal List<Creature> Enemies { get => enemies; set => enemies = value; }

        public Room()
        {
        }

        public Room(string n)
        {
            name = n;
            if (name[6] == 'b')
            {
                type = "special";
            }
            else if (name[6] == 'r')
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

                if (!file.EndOfStream)
                {
                    info = file.ReadLine().Split();

                    for (int i = 0; i < info.Length / 3; i++)
                    {
                        if (info[i * 3] == "0")
                        {
                            enemies.Add(new Creature(int.Parse(info[i * 3 + 1]) * (Form1.Resx / 1920), int.Parse(info[i * 3 + 2]) * (Form1.Resy / 1080)));
                        }
                        else if (info[i * 3] == "1")
                        {
                            enemies.Add(new Boss(int.Parse(info[i * 3 + 1]) * (Form1.Resx / 1920), int.Parse(info[i * 3 + 2]) * (Form1.Resy / 1080)));
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (field[i,j] != 0)
                    {
                        switch (field[i,j])
                        {
                            case 1: DrawingFunctions.DrawBlock(Form1.Xoffset + j * Form1.TileWidth, Form1.Yoffset + i * Form1.TileHeight, Color.Lime); break;
                            case 2: DrawingFunctions.DrawBlock(Form1.Xoffset + j * Form1.TileWidth, Form1.Yoffset + i * Form1.TileHeight, Color.Yellow); break;
                            case 3: DrawingFunctions.DrawBlock(Form1.Xoffset + j * Form1.TileWidth, Form1.Yoffset + i * Form1.TileHeight, Color.Red); break;
                            case 4: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 1, 0, Color.Lime); break;
                            case 5: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 0, 1, Color.Lime); break;
                            case 6: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, -1, 0, Color.Lime); break;
                            case 7: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 0, -1, Color.Lime); break;
                            case 8: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 1, 0, Color.Yellow); break;
                            case 9: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 0, 1, Color.Yellow); break;
                            case 10: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, -1, 0, Color.Yellow); break;
                            case 11: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 0, -1, Color.Yellow); break;
                            case 12: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 1, 0, Color.Red); break;
                            case 13: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 0, 1, Color.Red); break;
                            case 14: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, -1, 0, Color.Red); break;
                            case 15: DrawingFunctions.DrawDoor(Form1.Xoffset + j * Form1.TileWidth + Form1.TileWidth / 2, Form1.Yoffset + i * Form1.TileHeight + Form1.TileHeight / 2, 0, -1, Color.Red); break;
                            default:
                                break;
                        }
                    }
                }
            }
            foreach (Creature c in enemies)
            {
                c.Draw();
            }
        }
    }
}
